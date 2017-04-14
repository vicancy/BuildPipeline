namespace Incremental
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// An incremental database stores the mapping between function inputs to function outputs.
    /// </summary>
    public abstract class IncrementalDatabase : IDisposable
    {
        /// <summary>
        /// Looks up function result from the database
        /// </summary>
        public abstract CryptoHash[] LookupFunction(string function, string version, CryptoHash[] inputHashes);

        /// <summary>
        /// Updates or insert a function result to the database
        /// </summary>
        public abstract void PutFunction(string function, string version, CryptoHash[] inputHashes, CryptoHash[] outputHashes);

        /// <summary>
        /// Opens a stream with the crypto hash
        /// </summary>
        public abstract Stream OpenReadBlob(CryptoHash hash);

        /// <summary>
        /// Opens a blob for writing, returns the hash code after writing is done
        /// </summary>
        public abstract HashWriteStream OpenWriteBlob();

        public virtual Task Pull() => Task.CompletedTask;
        public virtual Task Push() => Task.CompletedTask;

        public virtual void Dispose() { }
    }

    public abstract class HashWriteStream : Stream
    {
        /// <summary>
        /// Closes the write stream and returns the hash code
        /// </summary>
        public abstract CryptoHash CloseAndGetHash();
    }
}
