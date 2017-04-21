namespace Microsoft.BuildPipeline.Incremental
{
    using System;
    using System.IO;

    public abstract class CryptoHashProvider
    {
        public abstract bool SupportsType(Type type);

        public abstract CryptoHash GetCryptoHash(object value);

        public abstract void Serialize(object value, Type declaredType, Stream output);

        public abstract object Deserialize(Type declaredType, Stream input);
    }
}
