using System;
using System.Runtime.InteropServices;

namespace Object.Generators
{
    public class PrimitiveGenerator : IGenerator
    {
        public object Generate(GeneratorArguments arguments)
        {
            double randValue1 = arguments.Random.NextDouble();
            double randValue2 = arguments.Random.NextDouble();
            double minValue = 0.0;

            if (!(arguments.Type.Equals(typeof(bool)) || arguments.Type.Equals(typeof(char))))
            {
                minValue = (double)Convert.ChangeType(arguments.MinTypeValue, typeof(double));
            }

            long generatedValue1 = 0;
            long generatedValue2 = 0;

            if (minValue == 0)
            {
                generatedValue1 = (long)(randValue1 * Math.Pow(byte.MaxValue, Marshal.SizeOf(arguments.Type)));
                generatedValue2 = (long)(randValue2 * Math.Pow(byte.MaxValue, Marshal.SizeOf(arguments.Type)));
            }
            else
            {
                generatedValue1 = (long)(randValue1 * Math.Pow(byte.MaxValue, Marshal.SizeOf(arguments.Type)) / 2);
                generatedValue2 = (long)(randValue2 * -Math.Pow(byte.MaxValue, Marshal.SizeOf(arguments.Type)) / 2);
            }

            return (generatedValue1 < Math.Abs(generatedValue2)) ? Convert.ChangeType(generatedValue1, arguments.Type) : Convert.ChangeType(generatedValue2, arguments.Type);
        }

        public bool CanGenerate(Type type)
        {
            return type.IsPrimitive;
        }
    }
}
