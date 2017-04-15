namespace Incremental
{
    using System.IO;

    /// <summary>
    /// Stores blobs that can be retrieved based on its content, not its location
    /// </summary>
    public interface IContentAddresableStore
    {
        /// <summary>
        /// Opens a stream with the crypto hash
        /// </summary>
        Stream OpenReadBlob(CryptoHash hash);

        /// <summary>
        /// Opens a blob for writing, returns the hash code after writing is done
        /// </summary>
        BlobWriteStream OpenWriteBlob();
    }

    public abstract class BlobWriteStream : Stream
    {
        /// <summary>
        /// Closes the write stream and returns the hash code
        /// </summary>
        public abstract CryptoHash CloseAndGetHash();
    }
}
