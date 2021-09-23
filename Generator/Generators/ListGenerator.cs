using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Generators
{
    public class ListGenerator : IGenerator
    {
        public object Generate(GeneratorArguments arguments)
        {
            Type[] genericType = arguments.Type.GetGenericArguments();
            Type listType = typeof(List<>).MakeGenericType(genericType);

            MethodInfo fakerCreateMethod = typeof(Faker).GetMethod("Create");
            MethodInfo listAddMethod = listType.GetMethod("Add");
            MethodInfo genericFakerCreateMethod = fakerCreateMethod.MakeGenericMethod(genericType);

            object faker = new Faker();
            object list = Activator.CreateInstance(listType);

            int listLength = arguments.Random.Next(1, byte.MaxValue);
            for (int i = 0; i < listLength; i++)
            {
                object item = genericFakerCreateMethod.Invoke(faker, null);
                listAddMethod.Invoke(list, new[] { item });
            }

            return list;
        }

        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && !type.IsGenericTypeDefinition && type.GetGenericTypeDefinition().Equals(typeof(List<>));
        }
    }
}
