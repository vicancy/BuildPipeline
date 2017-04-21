namespace Microsoft.BuildPipeline.Incremental
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using LibGit2Sharp;

    class GitIncrementalDatabase : IncrementalDatabase
    {
        const int GitHashSize = 20;

        private readonly KeyValueStore _db;
        private readonly Repository _repo;
        private readonly string _path;

        /// <param name="path">Path including ".incremental"</param>
        public GitIncrementalDatabase(string path)
        {
            if (!Repository.IsValid(path))
            {
                Directory.CreateDirectory(path);
                Repository.Init(path, isBare: true);
            }

            _path = path;
            _repo = new Repository(path);
            _db = new LevelDbStore(Path.Combine(path, "db"));
        }

        public override CryptoHash[] LookupFunction(string name, CryptoHash[] inputHashes)
        {
            var key = EncodeKey(name, inputHashes);
            var value = _db.TryGet(key);
            return value != null ? DecodeHashes(value) : Array.Empty<CryptoHash>();
        }

        public override void PutFunction(string name, CryptoHash[] inputHashes, CryptoHash[] outputHashes)
        {
            var key = EncodeKey(name, inputHashes);
            var value = EncodeHashes(outputHashes);
            _db.Put(key, value);
        }

        private static byte[] EncodeKey(string name, CryptoHash[] inputHashes)
        {
            Debug.Assert(!name.Contains("\0"));

            var nameLength = Encoding.UTF8.GetByteCount(name);
            var length = nameLength + 1;

            foreach (var inputHash in inputHashes)
            {
                length += 1 + inputHash.Bytes.Length;
            }

            var bytes = new byte[length];
            var i = Encoding.UTF8.GetBytes(name, 0, name.Length, bytes, 0);

            bytes[i++] = 0;

            foreach (var inputHash in inputHashes)
            {
                bytes[i++] += (byte)inputHash.Bytes.Length;
                Buffer.BlockCopy(inputHash.Bytes, 0, bytes, i, inputHash.Bytes.Length);
                i += inputHash.Bytes.Length;
            }

            return bytes;
        }

        private static byte[] EncodeHashes(CryptoHash[] hashes)
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

        private static CryptoHash[] DecodeHashes(byte[] bytes)
        {
            var i = 0;
            var result = new List<CryptoHash>((bytes.Length + GitHashSize) / (1 + GitHashSize));

            while (i < bytes.Length)
            {
                var hash = new byte[bytes[i++]];
                Buffer.BlockCopy(bytes, i, hash, 0, hash.Length);
                result.Add(new CryptoHash(hash));
                i += hash.Length;
            }

            return result.ToArray();
        }

        public override Stream OpenReadBlob(CryptoHash hash)
        {
            if (hash.Bytes == null || hash.Bytes.Length != GitHashSize) return null;

            return _repo.Lookup<Blob>(new ObjectId(hash.Bytes))?.GetContentStream();
        }

        public override BlobWriteStream OpenWriteBlob()
        {
            return new GitBlobWriteStream(_repo);
        }

        public override Task Pull()
        {
            throw new NotImplementedException();
        }

        public override Task Push()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            _db.Dispose();
            _repo.Dispose();
        }
    }

    class GitBlobWriteStream : BlobWriteStream
    {
        private readonly Repository _repo;
        private readonly MemoryStream _ms = new MemoryStream();

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;

        public GitBlobWriteStream(Repository repo) => _repo = repo;

        protected override void Dispose(bool disposing) => _ms.Dispose();

        public override void Flush() => _ms.Flush();
        public override void Write(byte[] buffer, int offset, int count) => _ms.Write(buffer, offset, count);

        public override CryptoHash CloseAndGetHash() => _repo.ObjectDatabase.Write<Blob>(_ms.ToArray()).RawId;

        public override long Length => throw new NotSupportedException();
        public override long Position { get => _ms.Position; set => throw new NotSupportedException(); }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();


        private static byte[] s_gitBlobHeader = Encoding.ASCII.GetBytes("blob ");
        private static byte[] s_zeroByte = new byte[] { 0 };

        public static byte[] ComputeGitBlobHash(Stream stream)
        {
            // https://git-scm.com/book/en/v1/Git-Internals-Git-Objects
            using (var sha1 = IncrementalHash.CreateHash(HashAlgorithmName.SHA1))
            {
                sha1.AppendData(s_gitBlobHeader);
                sha1.AppendData(Encoding.ASCII.GetBytes(stream.Length.ToString()));
                sha1.AppendData(s_zeroByte);

                var offset = 0;
                var buffer = new byte[8196];

                while (true)
                {
                    var n = stream.Read(buffer, offset, buffer.Length);
                    if (n <= 0) break;

                    sha1.AppendData(buffer, 0, n);
                }

                return sha1.GetHashAndReset();
            }
        }
    }
}
