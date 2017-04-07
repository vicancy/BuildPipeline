namespace Incremental
{
    using LevelDB;

    class DefaultDatabase : IncrementalDatabase
    {
        private readonly DB _db;

        public DefaultDatabase(string dbPath)
            => _db = DB.Open(dbPath, new Options
            {
                CreateIfMissing = true,
                // Most keys and values are hashes, so don't compress
                Compression = CompressionType.NoCompression,
            });

        public override byte[] Lookup(byte[] key)
            => _db.TryGet(ReadOptions.Default, key, out var value) ? value.ToArray() : null;

        public override void Upsert(byte[] key, byte[] value)
            => _db.Put(WriteOptions.Default, key, value);

        public override void Dispose()
            => _db.Dispose();
    }
}
