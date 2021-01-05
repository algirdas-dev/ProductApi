using NUnit.Framework;
using Product.Helpers.Generators;

namespace Product.Tests.Helpers.Generators
{
    public class CodeGeneratorTest
    {
        private CodeGeneratorHelper generator;

        [SetUp]
        public void Setup()
        {
            generator = new CodeGeneratorHelper();
        }

        [Test]
        public void GenerateCode()
        {
            byte length = 6; 
            Assert.AreEqual(length, generator.Generate(length).Length);
        }
    }
}