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
        Stream Read(Hash hash);

        /// <summary>Writes some content to the database</summary>
        Hash Write(byte[] content);

        Task Pull();
        Task Push();
    }
}
