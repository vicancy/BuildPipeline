namespace Microsoft.BuildPipeline.Incremental
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using Jil;

    class DefaultCryptoHashProvider : CryptoHashProvider
    {
        private static CryptoHash s_true = BitConverter.GetBytes(true);
        private static CryptoHash s_false = BitConverter.GetBytes(false);

        public override bool SupportsType(Type type) => true;

        public DefaultCryptoHashProvider()
        {
            if (!BitConverter.IsLittleEndian) throw new NotSupportedException("Does not support big endian");
        }

        public override CryptoHash GetCryptoHash(object value)
        {
            if (value is string str) return GetCryptoHash(str);
            else if (value is int i) return BitConverter.GetBytes(i);
            else if (value is bool b) return b ? s_true : s_false;
            else if (value.GetType().IsEnum) return GetCryptoHash(value.ToString());
            else if (value is byte[] bytes) return GetCryptoHash(bytes);
            else if (value is long l) return BitConverter.GetBytes(l);
            else if (value is float f) return BitConverter.GetBytes(f);
            else if (value is double d) return BitConverter.GetBytes(d);
            else if (value is ulong ul) return BitConverter.GetBytes(ul);
            else if (value is char c) return BitConverter.GetBytes(c);
            else if (value is short s) return BitConverter.GetBytes(s);
            else if (value is ushort us) return BitConverter.GetBytes(us);
            else if (value is uint ui) return BitConverter.GetBytes(ui);
            else return GetCryptoHash(JSON.Serialize(value));
        }

        private static CryptoHash GetCryptoHash(string str)
        {
            return GetCryptoHash(Encoding.UTF8.GetBytes(str));
        }

        private static CryptoHash GetCryptoHash(byte[] bytes)
        {
            if (bytes.Length <= CryptoHash.MaxSize) return bytes;
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(bytes);
            }
        }

        public override void Serialize(object value, Type declaredType, Stream output)
        {
            using (var writer = new StreamWriter(output))
            {
                JSON.Serialize(value, writer);
            }
        }

        public override object Deserialize(Type declaredType, Stream input)
        {
            using (var reader = new StreamReader(input))
            {
                return JSON.Deserialize(reader, declaredType);
            }
        }
    }
}
