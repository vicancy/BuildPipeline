namespace Microsoft.BuildPipeline.Incremental
{
    using LevelDB;

    class LevelDbStore : KeyValueStore
    {
        private readonly DB _db;

        public LevelDbStore(string path)
        {
            _db = DB.Open(path, new Options
            {
                CreateIfMissing = true,
                // Most keys and values are hashes, so don't compress
                Compression = CompressionType.NoCompression,
            });
        }

        public override byte[] TryGet(byte[] key)
            => _db.TryGet(ReadOptions.Default, key, out var value) ? value.ToArray() : null;

        public override void Put(byte[] key, byte[] value)
            => _db.Put(WriteOptions.Default, key, value);

        public override void Dispose() => _db.Dispose();
    }
}
