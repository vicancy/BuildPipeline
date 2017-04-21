namespace Microsoft.BuildPipeline.Incremental
{
    using System;

    /// <summary>
    /// A simple key value pair store on disk with binary keys and binary values.
    /// Keys and values cannot be null.
    /// </summary>
    public abstract class KeyValueStore : IDisposable
    {
        public abstract byte[] TryGet(byte[] key);

        public abstract void Put(byte[] key, byte[] value);

        public virtual void Dispose() { }
    }
}
