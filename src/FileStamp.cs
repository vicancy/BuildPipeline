namespace Incremental
{
    using System;
    using System.IO;

    public class FileStamp
    {
        public readonly string Path;
        public readonly CryptoHash ContentHash;
        public readonly long LastModifiedTime;

        public FileStamp(string path, CryptoHash contentHash, long lastModifiedTime)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            ContentHash = contentHash.HasValue ? contentHash : throw new ArgumentNullException(nameof(contentHash));
            LastModifiedTime = lastModifiedTime;
        }
    }

    class FileStampCryptoHashProvider : CryptoHashProvider
    {
        public override object Deserialize(Type declaredType, Stream input)
        {
            throw new NotImplementedException();
        }

        public override CryptoHash GetCryptoHash(object value)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(object value, Type declaredType, Stream output)
        {
            throw new NotImplementedException();
        }

        public override bool SupportsType(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
