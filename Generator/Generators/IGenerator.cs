using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Generators
{
    public interface IGenerator
    {
        object Generate(GeneratorArguments arguments);
        bool CanGenerate(Type type);
    }
}
