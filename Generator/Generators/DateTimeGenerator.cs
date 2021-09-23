using System;

namespace Object.Generators
{
    public class DateTimeGenerator : IGenerator
    {
        public object Generate(GeneratorArguments arguments)
        {
            long lowWord = -arguments.Random.Next((int)(DateTime.MaxValue.Ticks), (int)(DateTime.MinValue.Ticks));
            long highWord = arguments.Random.Next((int)(DateTime.MinValue.Ticks >> 32), (int)(DateTime.MaxValue.Ticks >> 32));

            long tick = (highWord << 32) | lowWord;
            
            return new DateTime(tick);
        }

        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(DateTime));
        }
    }
}
