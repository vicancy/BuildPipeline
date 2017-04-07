namespace Incremental
{
    using System;
    using System.Security.Cryptography;
    using System.IO;

    class DefaultFileCache : IncrementalFileCache
    {
        private readonly string _blobsPath;

        public DefaultFileCache(string blobsPath)
        {
            _blobsPath = Path.GetFullPath(blobsPath);
        }

        public override FilePath LookupFileByHash(CryptoHash hash)
        {
            var filename = hash.ToString();
            var path = Path.Combine(_blobsPath, filename.Substring(2), filename);

            var fi = new FileInfo(path);
            if (!fi.Exists) return null;

            return new FilePath(path, hash, fi.LastWriteTimeUtc.Ticks);
        }

        public override Stream BeginWriteFile()
        {
            return new IncrementalHashWriteStream(Path.Combine(_blobsPath, "." + Guid.NewGuid().ToString("N")));
        }

        public override CryptoHash EndWriteFile(Stream stream)
        {
            var ihws = stream as IncrementalHashWriteStream ?? throw new ArgumentException(nameof(stream));

            var hash = new CryptoHash(ihws.Hash.GetHashAndReset());
            var filename = hash.ToString();
            var directory = Path.Combine(_blobsPath, filename.Substring(2));
            var path = Path.Combine(directory, filename);

            Directory.CreateDirectory(directory);

            try
            {
                File.Move(ihws.FileStream.Name, path);
                File.SetAttributes(path, FileAttributes.ReadOnly);
                return hash;
            }
            catch (IOException) when (File.Exists(path))
            {
                return hash;
            }
        }

        class IncrementalHashWriteStream : Stream
        {
            public readonly FileStream FileStream;
            public readonly IncrementalHash Hash = IncrementalHash.CreateHash(HashAlgorithmName.SHA1);

            public IncrementalHashWriteStream(string path)
            {
                FileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                FileStream.Write(buffer, offset, count);
                Hash.AppendData(buffer, offset, count);
            }

            public override bool CanRead => false;
            public override bool CanSeek => false;
            public override bool CanWrite => true;
            public override long Length => throw new NotSupportedException();
            public override long Position { get => FileStream.Position; set => throw new NotSupportedException(); }

            public override void Flush() => FileStream.Flush();
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

            protected override void Dispose(bool disposing) => FileStream.Dispose();
        }
    }
}
