using System;

namespace Product.Helpers.Generators
{
    public class CodeGeneratorHelper
    {
        public string Generate(byte length)
        {
            //length = length < 0 ? length * -1 : length;
            var str = "";

            do
            {
                str += Guid.NewGuid().ToString().Replace("-", "");
            }

            while (length > str.Length);

            return str.Substring(0, length);
        }
    }
}
