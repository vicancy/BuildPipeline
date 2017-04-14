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
        const int GitHashSize = 20;

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
            var result = new List<Hash>((bytes.Length + GitHashSize) / (1 + GitHashSize));

            while (i < bytes.Length)
            {
                var hash = new byte[bytes[i++]];
                Buffer.BlockCopy(bytes, i, hash, 0, hash.Length);
                result.Add(new Hash(hash));
                i += hash.Length;
            }

            return result.ToArray();
        }

        public Stream OpenReadBlob(Hash hash)
        {
            if (hash.Bytes == null || hash.Bytes.Length != GitHashSize) return null;

            return _repo.Lookup<Blob>(new ObjectId(hash.Bytes))?.GetContentStream();
        }

        public HashWriteStream OpenWriteBlob()
        {
            return new GitHashWriteStream(_repo);
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

        class GitHashWriteStream : HashWriteStream
        {
            private readonly Repository _repo;
            private readonly MemoryStream _ms = new MemoryStream();

            public GitHashWriteStream(Repository repo)
            {
                _repo = repo;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _ms.Write(buffer, offset, count);
            }

            public override bool CanRead => false;
            public override bool CanSeek => false;
            public override bool CanWrite => true;
            public override long Length => throw new NotSupportedException();
            public override long Position { get => _ms.Position; set => throw new NotSupportedException(); }

            public override void Flush() => _ms.Flush();
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

            protected override void Dispose(bool disposing) => _ms.Dispose();

            public override Hash CloseAndGetHash()
            {
                return new Hash(_repo.ObjectDatabase.Write<Blob>(_ms.ToArray()).RawId);
            }
        }
    }
}
