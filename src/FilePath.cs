namespace Incremental
{
    using System;

    public class FilePath
    {
        public readonly string Path;
        public readonly CryptoHash ContentHash;
        public readonly long LastModifiedTime;

        public FilePath(string path, CryptoHash contentHash, long lastModifiedTime)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            ContentHash = contentHash.HasValue ? contentHash : throw new ArgumentNullException(nameof(contentHash));
            LastModifiedTime = lastModifiedTime;
        }
    }
}
