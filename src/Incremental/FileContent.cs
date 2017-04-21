namespace Microsoft.BuildPipeline.Incremental
{
    using System;
    using System.IO;

    /// <summary>
    /// Crypto hash is calculated based on raw file content
    /// </summary>
    public abstract class FileContent
    {
        public static FileContent Load(string fullPath) => new PhysicalFileContent(fullPath);

        public static FileContentWriteStream Create(IContentAddresableStore cas) => new FileContentWriteStream(cas);

        public abstract Stream OpenRead();

        public override string ToString()
        {
            using (var reader = new StreamReader(OpenRead()))
            {
                var chars = new char[1024];
                var n = reader.Read(chars, 0, chars.Length);
                return new string(chars, 0, n);
            }
        }
    }

    class PhysicalFileContent : FileContent
    {
        public readonly string FullPath;

        public override Stream OpenRead() => File.OpenRead(FullPath);

        public PhysicalFileContent(string fullPath)
            => FullPath = Path.IsPathRooted(fullPath)
                ? FullPath : throw new ArgumentOutOfRangeException(nameof(fullPath), "Expect a rooted full path");
    }

    public class FileContentWriteStream : Stream
    {
        private readonly IContentAddresableStore _cas;
        private readonly BlobWriteStream _bws;

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;

        public FileContentWriteStream(IContentAddresableStore cas)
        {
            _cas = cas;
            _bws = cas.OpenWriteBlob();
        }

        public FileContent CloseAndGetFileContent() => new CasFileContent(_bws.CloseAndGetHash(), _cas);

        protected override void Dispose(bool disposing) => _bws.Dispose();

        public override void Flush() => _bws.Flush();
        public override void Write(byte[] buffer, int offset, int count) => _bws.Write(buffer, offset, count);

        public override long Length => throw new NotSupportedException();
        public override long Position { get => _bws.Position; set => throw new NotSupportedException(); }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }

    class CasFileContent : FileContent
    {
        private readonly IContentAddresableStore _cas;
        public readonly CryptoHash Hash;

        public override Stream OpenRead() => _cas.OpenReadBlob(Hash);

        public CasFileContent(CryptoHash hash, IContentAddresableStore cas)
        {
            Hash = hash.HasValue ? hash : throw new ArgumentException(nameof(hash));
            _cas = cas;
        }
    }
}
