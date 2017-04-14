namespace Incremental
{
    using System;

    public struct CryptoHash : IEquatable<CryptoHash>
    {
        public static readonly CryptoHash Empty = new CryptoHash();

        public const int MaxSize = byte.MaxValue;

        public readonly byte[] Bytes;
        public bool HasValue => Bytes != null;

        public CryptoHash(byte[] bytes)
        {
            Bytes = bytes != null && bytes.Length > MaxSize
                ? throw new ArgumentOutOfRangeException(nameof(bytes))
                : bytes;
        }

        public static implicit operator byte[](CryptoHash hash) => hash.Bytes;
        public static implicit operator CryptoHash(byte[] bytes) => new CryptoHash(bytes);

        public unsafe override string ToString()
        {
            if (Bytes == null) return "";

            var b = 0;
            var c = stackalloc char[MaxSize * 2];
            for (int i = 0; i < Bytes.Length; i++)
            {
                b = Bytes[i] >> 4;
                c[i * 2] = (char)(87 + b + (((b - 10) >> 31) & -39));
                b = Bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(87 + b + (((b - 10) >> 31) & -39));
            }
            return new string(c, 0, Bytes.Length * 2);
        }

        public override bool Equals(object obj)
        {
            return obj is CryptoHash ch ? Equals(ch) : false;
        }

        public unsafe bool Equals(CryptoHash other)
        {
            var data1 = Bytes;
            var data2 = other.Bytes;

            // http://stackoverflow.com/questions/43289/comparing-two-byte-arrays-in-net
            if (data1 == data2)
                return true;
            if (data1 == null || data2 == null)
                return false;
            if (data1.Length != data2.Length)
                return false;

            fixed (byte* bytes1 = data1, bytes2 = data2)
            {
                int len = data1.Length;
                int rem = len % (sizeof(long) * 16);
                long* b1 = (long*)bytes1;
                long* b2 = (long*)bytes2;
                long* e1 = (long*)(bytes1 + len - rem);

                while (b1 < e1)
                {
                    if (*(b1) != *(b2) || *(b1 + 1) != *(b2 + 1) ||
                        *(b1 + 2) != *(b2 + 2) || *(b1 + 3) != *(b2 + 3) ||
                        *(b1 + 4) != *(b2 + 4) || *(b1 + 5) != *(b2 + 5) ||
                        *(b1 + 6) != *(b2 + 6) || *(b1 + 7) != *(b2 + 7) ||
                        *(b1 + 8) != *(b2 + 8) || *(b1 + 9) != *(b2 + 9) ||
                        *(b1 + 10) != *(b2 + 10) || *(b1 + 11) != *(b2 + 11) ||
                        *(b1 + 12) != *(b2 + 12) || *(b1 + 13) != *(b2 + 13) ||
                        *(b1 + 14) != *(b2 + 14) || *(b1 + 15) != *(b2 + 15))
                        return false;
                    b1 += 16;
                    b2 += 16;
                }

                for (int i = 0; i < rem; i++)
                    if (data1[len - 1 - i] != data2[len - 1 - i])
                        return false;

                return true;
            }
        }

        public unsafe override int GetHashCode()
        {
            fixed (byte* b = Bytes)
            {
                // Since we are already a crypto hash, just return the first 4 bytes.
                return *(int*)b;
            }
        }
    }
}
