namespace Incremental
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IncrementalRunner
    {
        private readonly IncrementalDatabase _db;
        private readonly CryptoHashProvider[] _hashProviders;

        public IncrementalRunner(IncrementalDatabase db, IEnumerable<CryptoHashProvider> hashProviders)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _hashProviders = hashProviders.Concat(new CryptoHashProvider[] { new DefaultCryptoHashProvider() }).ToArray();
        }

        public object[] Run(string name, string version, Func<object[], object[]> function, params object[] inputs)
        {
            return null;
        }
    }
}
