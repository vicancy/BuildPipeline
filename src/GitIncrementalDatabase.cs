namespace Incremental
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using LibGit2Sharp;
    using LevelDB;

    class GitIncrementalDatabase : IIncrementalDatabase
    {
        private readonly DB _db;
        private readonly Repository _repo;
        private readonly string _path;

        /// <param name="path">Path including ".incremental"</param>
        public GitIncrementalDatabase(string path)
        {
            Directory.CreateDirectory(path);

            if (!Repository.IsValid(path))
            {
                Repository.Init(path, isBare: true);
            }

            _path = path;
            _repo = new Repository(path);
            _db = DB.Open(Path.Combine(path, "db"), new Options
            {
                CreateIfMissing = true,
                // Most keys and values are hashes, so don't compress
                Compression = CompressionType.NoCompression,
            });
        }

        public Hash[] LookupFunction(string function, string version, Hash[] inputHashes)
        {
            var key = EncodeKey(function, version, inputHashes);
            if (_db.TryGet(ReadOptions.Default, key, out var value))
                return DecodeHashes(value.ToArray());
            return Array.Empty<Hash>();
        }

        public void PutFunction(string function, string version, Hash[] inputHashes, Hash[] outputHashes)
        {
            var key = EncodeKey(function, version, inputHashes);
            var value = EncodeHashes(outputHashes);
            _db.Put(WriteOptions.Default, key, value);
        }

        private static byte[] EncodeKey(string function, string version, Hash[] inputHashes)
        {
            var functionLength = Encoding.UTF8.GetByteCount(function);
            if (functionLength > byte.MaxValue) throw new ArgumentOutOfRangeException(nameof(function), "Cannot be bigger than 255");

            var versionLength = Encoding.UTF8.GetByteCount(version);
            if (functionLength > byte.MaxValue) throw new ArgumentOutOfRangeException(nameof(version), "Cannot be bigger than 255");

            var length = 2 + functionLength + versionLength;

            foreach (var inputHash in inputHashes)
            {
                length += 1 + inputHash.Bytes.Length;
            }

            var bytes = new byte[length];
            var i = 1;

            bytes[i] = (byte)functionLength;
            i += Encoding.UTF8.GetBytes(function, 0, function.Length, bytes, i);
            bytes[i++] = (byte)versionLength;
            i += Encoding.UTF8.GetBytes(version, 0, version.Length, bytes, i);

            foreach (var inputHash in inputHashes)
            {
                bytes[i++] += (byte)inputHash.Bytes.Length;
                Buffer.BlockCopy(inputHash.Bytes, 0, bytes, i, inputHash.Bytes.Length);
                i += inputHash.Bytes.Length;
            }

            return bytes;
        }

        private static byte[] EncodeHashes(Hash[] hashes)
        {
            var length = 0;
            foreach (var hash in hashes)
            {
                length += 1 + hash.Bytes.Length;
            }

            var i = 0;
            var bytes = new byte[length];

            foreach (var hash in hashes)
            {
                bytes[i++] += (byte)hash.Bytes.Length;
                Buffer.BlockCopy(hash.Bytes, 0, bytes, i, hash.Bytes.Length);
                i += hash.Bytes.Length;
            }

            return bytes;
        }

        private static Hash[] DecodeHashes(byte[] bytes)
        {
            var i = 0;
            var result = new List<Hash>((bytes.Length + byte.MaxValue) / (1 + byte.MaxValue));

            while (i < bytes.Length)
            {
                var hash = new byte[bytes[i++]];
                Buffer.BlockCopy(bytes, i, hash, 0, hash.Length);
                result.Add(new Hash(hash));
                i += hash.Length;
            }

            return result.ToArray();
        }

        public Stream Read(Hash hash)
        {
            return _repo.Lookup<Blob>(new ObjectId(hash.Bytes))?.GetContentStream();
        }

        public Hash Write(byte[] data)
        {
            var hash = _repo.ObjectDatabase.Write<Blob>(data);
            
            // TODO: Need to put this hash to a tree to enable push/pull

            return new Hash(hash.RawId);
        }

        public Task Pull()
        {
            throw new NotImplementedException();
        }

        public Task Push()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _db.Dispose();
            _repo.Dispose();
        }
    }
}
