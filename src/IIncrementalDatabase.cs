namespace Incremental
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// An incremental database stores the mapping between function inputs to function outputs.
    /// </summary>
    public interface IIncrementalDatabase : IDisposable
    {
        /// <summary>Looks up function result from the database</summary>
        Hash[] LookupFunction(string function, string version, Hash[] inputHashes);

        /// <summary>Updates or insert a function result to the database</summary>
        void PutFunction(string function, string version, Hash[] inputHashes, Hash[] outputHashes);

        /// <summary>Opens a stream with the crypto hash</summary>
        Stream OpenReadBlob(Hash hash);

        /// <summary>Opens a blob for writing, returns the hash code after writing is done</summary>
        HashWriteStream OpenWriteBlob();

        Task Pull();
        Task Push();
    }

    public abstract class HashWriteStream : Stream
    {
        /// <summary>Closes the write stream and returns the hash code</summary>
        public abstract Hash CloseAndGetHash();
    }
}
