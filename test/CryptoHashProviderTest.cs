namespace Microsoft.BuildPipeline.Incremental
{
    using System.IO;
    using Xunit;

    public class CryptoHashProviderTest
    {
        [Fact]
        public void default_crypto_hash_provider()
        {
            var provider = new DefaultCryptoHashProvider();
            Test(provider, true, "01");
            Test(provider, 1, "01000000");
            Test(provider, 1UL, "0100000000000000");
            Test(provider, "", "");
            Test(provider, "asdf", "61736466");
            Test(provider, new string('0', 1000), "88eaec963a0368e8854562e824b7fe2fdd3db8d1");
        }

        public static void Test(CryptoHashProvider provider, object value, string hash = null)
        {
            Assert.True(provider.SupportsType(value.GetType()));

            if (hash != null)
            {
                Assert.Equal(hash, provider.GetCryptoHash(value).ToString());
            }

            using (var output = new MemoryStream())
            {
                provider.Serialize(value, value.GetType(), output);

                using (var input = new MemoryStream(output.ToArray()))
                {
                    Assert.Equal(value, provider.Deserialize(value.GetType(), input));
                }
            }
        }
    }
}
