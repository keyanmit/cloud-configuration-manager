using System;

namespace SigningAndEncryption.SigningAndExcryption
{
    static class ByteConvertHelper
    {
        public static byte[] GetBytes(string st)
        {
            var bytes = new byte[st.Length * sizeof(char)];
            System.Buffer.BlockCopy(st.ToCharArray(), 0, bytes, 0, st.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            var stringLen = bytes.Length / sizeof(char);
            var chars = new char[stringLen];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
