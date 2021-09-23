using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Object.Generators;

namespace Object
{
    public class Faker
    {
        private static readonly Random random = new Random();
        private static readonly string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        private static readonly List<IGenerator> generators = new List<IGenerator>()
        {
            new PrimitiveGenerator(),
            new StringGenerator(),
        };

        private List<Type> _generatedTypes = new List<Type>();

        static Faker()
        {
            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();

            string[] pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (string file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);
                IEnumerable<Type> types = asm.GetTypes().
                                   Where(t => t.GetInterfaces().
                                   Where(i => i.FullName == typeof(IGenerator).FullName).Any());

                foreach (Type type in types)
                {
                    IGenerator plugin = asm.CreateInstance(type.FullName) as IGenerator;
                    generators.Add(plugin);
                }
            }
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type type)
        {
            IGenerator generator = GetGenerator(type);
            if (generator != null)
            {
                GeneratorArguments arguments = new GeneratorArguments(random, type, this);
                return generator.Generate(arguments);
            }
            return CreateObject(type);
        }

        private ConstructorInfo[] GetSortedConstructors(Type type)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();

            for (int i = 0; i < (constructorInfos.Length - 1); i++)
            {
                for (int j = 1; j < constructorInfos.Length; j++)
                {
                    int constructorParametersLength1 = constructorInfos[i].GetParameters().Length;
                    int constructorParametersLength2 = constructorInfos[j].GetParameters().Length;

                    if (constructorParametersLength1 < constructorParametersLength2)
                    {
                        ConstructorInfo temp = constructorInfos[i];
                        constructorInfos[i] = constructorInfos[j];
                        constructorInfos[j] = temp;
                    }
                }
            }

            return constructorInfos;
        }

        private object[] CreateConstructorArguments(ParameterInfo[] parameterInfos)
        {
            List<object> arguments = new List<object>();

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                object generatedArgument = Create(parameterInfo.ParameterType);
                arguments.Add(generatedArgument);
            }

            return arguments.ToArray();
        }

        private object CreateObject(Type type)
        {
            if (IsСyclicalВependence(type))
            {
                return null;
            }

            ConstructorInfo[] constructorInfos = GetSortedConstructors(type);
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                object createdObject = null;
                try
                {
                    object[] constructorArguments = CreateConstructorArguments(constructorInfo.GetParameters());
                    createdObject = Activator.CreateInstance(type, constructorArguments);
                }
                catch (NotSupportedException)
                {
                    break;
                }
                CreatePropertyValues(createdObject, type);
                return createdObject;
            }
            return null;
        }

        private void CreatePropertyValues(object obj, Type type)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite)
                {
                    object value = Create(propertyInfo.PropertyType);
                    propertyInfo.SetValue(obj, value);
                }
            }
        }

        private IGenerator GetGenerator(Type type)
        {
            foreach (IGenerator generator in generators)
            {
                if (generator.CanGenerate(type))
                {
                    return generator;
                }
            }
            return null;
        }

        private bool IsСyclicalВependence(Type type)
        {
            foreach (Type generatedType in _generatedTypes)
            {
                if (generatedType.Equals(type))
                {
                    return true;
                }
            }
            _generatedTypes.Add(type);
            return false;
        }
    }
}
