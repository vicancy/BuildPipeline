namespace Incremental
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IncrementalRunner
    {
        private static readonly CryptoHashProvider[] s_defaultHashProviders = new CryptoHashProvider[]
        {
            new FileStampCryptoHashProvider(),
            new DefaultCryptoHashProvider(),
        };

        private readonly IncrementalDatabase _db;
        private readonly CryptoHashProvider[] _hashProviders;

        public IncrementalRunner(IncrementalDatabase db, IEnumerable<CryptoHashProvider> hashProviders)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _hashProviders = hashProviders.Concat(s_defaultHashProviders).ToArray();
        }

        public object[] Run(string function, string version, Func<object[], object[]> invoke, object[] inputs, Type[] outputTypes)
        {
            var inputHashes = new CryptoHash[inputs.Length];
            for (var i = 0; i < inputs.Length; i++)
            {
                inputHashes[i] = GetCryptoHash(inputs[i]);
            }

            var outputHashes = _db.LookupFunction(function, version, inputHashes);
            if (outputHashes.Length > 0)
            {
                if (outputHashes.Length != outputTypes.Length)
                {
                    throw new InvalidOperationException(
                        $"Output parameter count for '{function}' '{version}' " +
                        $"has changed from '{outputHashes.Length}' to '{outputTypes.Length}'");
                }

                // Return result from the incremental cache
                var outputs = new object[outputHashes.Length];

                for (var i = 0; i < outputs.Length; i++)
                {
                    using (var input = _db.OpenReadBlob(outputHashes[i]))
                    {
                        var provider = GetProvider(outputTypes[i]);
                        outputs[i] = provider.Deserialize(outputTypes[i], input);
                    }
                }

                return outputs;
            }
            else
            {
                // Not found in the cache, invoke and fill the cache
                var outputs = invoke(inputs);
                if (outputs == null || outputs.Length <= 0)
                    throw new InvalidOperationException($"Invoke returns empty outputs");

                for (var i = 0; i < outputs.Length; i++)
                {
                    var provider = GetProvider(outputs[i].GetType());
                    using (var output = _db.OpenWriteBlob())
                    {
                        provider.Serialize(outputs[i], outputTypes[i], output);
                        outputHashes[i] = GetCryptoHash(output.CloseAndGetHash());
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
    }
}
