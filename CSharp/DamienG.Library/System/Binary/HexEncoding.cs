using System;

namespace DamienG.System.Binary
{
    public class HexEncoding : BinaryTextEncoding
    {
        public override string Encode(byte[] bytes)
        {
            var output = new char[bytes.Length * 2];
            var outputIndex = 0;
            for (var byteIndex = 0; byteIndex < bytes.Length; byteIndex++)
            {
                var hex = bytes[byteIndex].ToString("X2");
                output[outputIndex++] = hex[0];
                output[outputIndex++] = hex[1];
            }
            return new string(output);
        }

        public override byte[] Decode(string text)
        {
            if (text.Length % 2 == 1)
                throw new ArgumentOutOfRangeException("text", "Must be an even number of hex digits");

            var output = new byte[text.Length / 2];
            var textIndex = 0;
            for (var outputIndex = 0; outputIndex < output.Length; outputIndex++)
            {
                var b = (byte)((Hex(text[textIndex++]) << 4) + Hex(text[textIndex++]));
                output[outputIndex] = b;
            }
            return output;
        }

        private int Hex(char a)
        {
            if (a >= '0' && a <= '9')
                return (int)a - (int)'0';

            if (a >= 'a' && a <= 'f')
                return (int)a - (int)'a' + 10;

            if (a >= 'A' && a <= 'F')
                return (int)a - (int)'A' + 10;

            throw new ArgumentOutOfRangeException("text", String.Format("Character {0} is not hexadecimal", a));
        }
    }
}