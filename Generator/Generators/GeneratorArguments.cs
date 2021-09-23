using System;

namespace Object.Generators
{
    public class GeneratorArguments
    {
        public Random Random { get; }
        public Type Type { get; }
        public Faker Faker { get; }

        public object MinTypeValue => Type.GetField("MinValue").GetValue(null);
        public object MaxTypeValue => Type.GetField("MaxValue").GetValue(null);

        public GeneratorArguments(Random random, Type type, Faker faker)
        {
            Random = random;
            Type = type;
            Faker = faker;
        }
    }
}
