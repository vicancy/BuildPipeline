namespace Incremental
{
    using System;

    public abstract class IncrementalDatabase : IDisposable
    {
        public abstract byte[] Lookup(byte[] key);

        public abstract void Upsert(byte[] key, byte[] value);

        public abstract void Dispose();
    }
}
