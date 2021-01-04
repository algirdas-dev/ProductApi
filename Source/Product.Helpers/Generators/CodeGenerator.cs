using System;
using System.Security.Cryptography;

namespace Product.Helpers.Generators
{
    public class CodeGenerator
    {
        public string Generate(byte length) {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bit_count = (length * 6);
                var byte_count = ((bit_count + 7) / 8); // rounded up
                var bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
