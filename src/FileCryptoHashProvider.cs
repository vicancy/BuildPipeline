namespace Incremental
{
    using System;
    using System.IO;
    using System.Text;

    class FileCryptoHashProvider : CryptoHashProvider
    {
        private readonly string _basePath;
        private readonly KeyValueStore _db;

        public override bool SupportsType(Type type) => typeof(FileContent).IsAssignableFrom(type);

        public FileCryptoHashProvider(string basePath = null, string cachePath = null)
        {
            cachePath = cachePath ?? Path.Combine(Path.GetTempPath(), ".incremental", "filestamp");
            _basePath = basePath ?? Directory.GetCurrentDirectory();

            _db = new LevelDbStore(cachePath);
        }

        public override CryptoHash GetCryptoHash(object value)
        {
            if (value is CasFileContent cfc) return cfc.Hash;
            if (value is PhysicalFileContent pfc) return GetCryptoHash(pfc.FullPath);
            if (value is PhysicalFilePath pfp) return GetCryptoHash(pfp.FullPath);

            throw new NotSupportedException("Unknown file type: " + value.GetType().FullName);
        }

        private CryptoHash GetCryptoHash(string fullPath)
        {
            var key = Encoding.UTF8.GetBytes(fullPath);
            var lastWriteTicks = new FileInfo(fullPath).LastWriteTimeUtc.Ticks;
            var bytes = _db.TryGet(key);
            if (bytes != null && bytes.Length == 8 /* last write ticks*/ + 20 /* git sha1 */)
            {
                var cachedLastWriteTicks = BitConverter.ToInt64(bytes, 0);
                if (lastWriteTicks == cachedLastWriteTicks)
                    return new CryptoHash(bytes, 8, bytes.Length - 8);
            }

            using (var stream = File.OpenRead(fullPath))
            {
                var hash = GitBlobWriteStream.ComputeGitBlobHash(stream);
                bytes = new byte[8 + hash.Length];
                Buffer.BlockCopy(BitConverter.GetBytes(lastWriteTicks), 0, bytes, 0, 8);
                Buffer.BlockCopy(hash, 0, bytes, 8, bytes.Length);
                _db.Put(key, bytes);
                return hash;
            }
        }

        public override object Deserialize(Type declaredType, Stream input)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(object value, Type declaredType, Stream output)
        {
            throw new NotImplementedException();
        }
    }
}
