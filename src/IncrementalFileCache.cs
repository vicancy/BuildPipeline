namespace Incremental
{
    using System.IO;

    public abstract class IncrementalFileCache
    {
        public abstract FilePath LookupFileByHash(CryptoHash hash);

        public abstract Stream BeginWriteFile();

        public abstract CryptoHash EndWriteFile(Stream stream);
    }
}
