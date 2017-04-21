namespace Microsoft.BuildPipeline.Incremental
{
    using System;

    public struct Hashed<T> : IEquatable<Hashed<T>>
    {
        public readonly T Value;
        public readonly CryptoHash Hash;

        public Hashed(T value, CryptoHash hash)
        {
            Value = value;
            Hash = hash;
        }

        public static implicit operator T(Hashed<T> obj) => obj.Value;
        public static implicit operator Hashed<T>(T obj) => new Hashed<T>(obj, CryptoHash.Empty);

        public override string ToString() => string.Concat(Value, " ", Hash);
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => obj is Hashed<T> h ? Equals(h) : false;
        public bool Equals(Hashed<T> other) => Equals(Value, other.Value) && Hash.Equals(other.Hash);
    }
}
