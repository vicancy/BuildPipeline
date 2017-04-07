namespace Incremental
{
    using System;

    public struct CryptoHash
    {
        public const int MaxLength = byte.MaxValue;

        public readonly byte[] Bytes;
        public bool HasValue => Bytes != null;

        public CryptoHash(byte[] bytes)
        {
            if (bytes != null && bytes.Length > MaxLength)
                throw new ArgumentOutOfRangeException(nameof(bytes));

            Bytes = bytes;
        }

        public unsafe override string ToString()
        {
            if (Bytes == null) return "";

            var b = 0;
            var c = stackalloc char[MaxLength * 2];
            for (int i = 0; i < Bytes.Length; i++)
            {
                b = Bytes[i] >> 4;
                c[i * 2] = (char)(87 + b + (((b - 10) >> 31) & -39));
                b = Bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(87 + b + (((b - 10) >> 31) & -39));
            }
            return new string(c, 0, Bytes.Length * 2);
        }
    }
}
