using System;
using System.Text;

namespace Object.Generators
{
    public class StringGenerator : IGenerator
    {
        public object Generate(GeneratorArguments arguments)
        {
            int stringLength = arguments.Random.Next(1, byte.MaxValue);
            byte[] stringBytes = new byte[stringLength];

            arguments.Random.NextBytes(stringBytes);
            return Encoding.UTF8.GetString(stringBytes);
        }

        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(string));
        }
    }
}
