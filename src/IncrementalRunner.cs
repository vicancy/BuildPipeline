namespace Incremental
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class IncrementalRunner : IDisposable
    {
        private static readonly CryptoHashProvider[] s_defaultHashProviders = new CryptoHashProvider[]
        {
            new FileCryptoHashProvider(),
            new DefaultCryptoHashProvider(),
        };

        private readonly IncrementalDatabase _db;
        private readonly CryptoHashProvider[] _hashProviders;

        public IncrementalRunner() : this(new GitIncrementalDatabase(".incremental")) { }
        public IncrementalRunner(IncrementalDatabase db) : this(db, Enumerable.Empty<CryptoHashProvider>()) { }
        public IncrementalRunner(IncrementalDatabase db, IEnumerable<CryptoHashProvider> hashProviders)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _hashProviders = hashProviders.Concat(s_defaultHashProviders).ToArray();
        }

        /// <summary>
        /// Runs a function using incremental cache.
        /// </summary>
        /// <param name="name">
        /// Functions with the same name should always
        /// produce the exact same outputs when inputs are same
        /// </param>
        /// <param name="invoke">Doing actual work when the function is invoked</param>
        /// <param name="inputs">
        /// Inputs to the function with corresponding crypto hash, 
        /// crypto hash is optional for inputs and will be recalculated using
        /// CryptoHashProvider when missing.
        /// </param>
        /// <param name="outputTypes">Types of function outputs</param>
        /// <returns>Function outputs with corresponding crypto hash</returns>
        public Hashed<object>[] Run(string name, Func<object[], object[]> invoke, Hashed<object>[] inputs, Type[] outputTypes)
        {
            var inputHashes = new CryptoHash[inputs.Length];
            for (var i = 0; i < inputs.Length; i++)
            {
                inputHashes[i] = inputs[i].Hash.HasValue ? inputs[i].Hash : GetCryptoHash(inputs[i].Value);
            }

            var outputHashes = _db.LookupFunction(name, inputHashes);
            if (outputHashes.Length > 0)
            {
                if (outputHashes.Length != outputTypes.Length)
                {
                    throw new InvalidOperationException(
                        $"Output parameter count for '{name}' " +
                        $"has changed from '{outputHashes.Length}' to '{outputTypes.Length}'");
                }

                // Return result from the incremental cache
                var outputs = new Hashed<object>[outputHashes.Length];

                for (var i = 0; i < outputs.Length; i++)
                {
                    using (var input = _db.OpenReadBlob(outputHashes[i]))
                    {
                        var provider = GetProvider(outputTypes[i]);
                        outputs[i] = new Hashed<object>(provider.Deserialize(outputTypes[i], input), outputHashes[i]);
                    }
                }

                return outputs;
            }
            else
            {
                // Not found in the cache, invoke and fill the cache
                var inputObjects = new object[inputs.Length];
                for (var i = 0; i < inputs.Length; i++)
                {
                    inputObjects[i] = inputs[i].Value;
                }
                var outputObjects = invoke(inputObjects);
                if (outputObjects == null || outputObjects.Length <= 0)
                    throw new InvalidOperationException($"Invoke returns empty outputs");

                var outputs = new Hashed<object>[outputObjects.Length];
                for (var i = 0; i < outputObjects.Length; i++)
                {
                    var provider = GetProvider(outputObjects[i].GetType());
                    using (var output = _db.OpenWriteBlob())
                    {
                        provider.Serialize(outputObjects[i], outputTypes[i], output);
                        outputs[i] = new Hashed<object>(outputObjects[i], GetCryptoHash(output.CloseAndGetHash()));
                    }
                }
                return outputs;
            }
        }

        private CryptoHashProvider GetProvider(Type type)
        {
            var provider = default(CryptoHashProvider);

            foreach (var hashProvider in _hashProviders)
            {
                if (hashProvider.SupportsType(type))
                {
                    provider = hashProvider;
                    break;
                }
            }

            return provider ?? throw new InvalidOperationException($"Don't know how to serialize '{type}'");
        }

        private CryptoHash GetCryptoHash(object input)
        {
            var type = input.GetType();
            var hash = default(CryptoHash);

            foreach (var hashProvider in _hashProviders)
            {
                if (hashProvider.SupportsType(type))
                {
                    hash = hashProvider.GetCryptoHash(input);
                    break;
                }
            }

            return hash.HasValue ? hash : throw new InvalidOperationException($"Cannot get hash value for '{input}'");
        }

        public void Dispose() => _db.Dispose();
    }
}
