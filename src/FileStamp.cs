namespace Incremental
{
    using System;

    public class FileStamp
    {
        public readonly string Path;
        public readonly Hash ContentHash;
        public readonly long LastModifiedTime;

        public FileStamp(string path, Hash contentHash, long lastModifiedTime)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            ContentHash = contentHash.HasValue ? contentHash : throw new ArgumentNullException(nameof(contentHash));
            LastModifiedTime = lastModifiedTime;
        }
    }
}
