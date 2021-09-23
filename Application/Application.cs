using System;
using System.Text;
using System.Collections.Generic;
using Object;

namespace Application
{
    public class Application
    {
        public class ClassInClassInClass
        {
            private List<List<char>> _list;

            public JustAClass
             Property1 { get; set; }

            public ClassInClassInClass(List<List<char>> list)
            {
                _list = list;
            }

            public override string ToString()
            {
                return $"\n\t\tList: {OutputList()}";
            }

            private string OutputList()
            {
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < _list.Count; i++)
                {
                    for (int j = 0; j < _list[i].Count; j++)
                    {
                        builder.Append($" {_list[i][j]}");
                    }
                }
                return builder.ToString();
            }
        }

        public class ClassInClass
        {
            public char Property1 { get; set; }
            public bool Property2 { get; set; }
            public List<int> Property3 { get; set; }
            public ClassInClassInClass Property4 { get; set; }

            public override string ToString()
            {
                return $"\tProperty1: {Property1}\n\tProperty2: {Property2}\n\tProperty3: {OutputList()}\n\tProperty4: {Property4}";
            }

            private string OutputList()
            {
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < Property3.Count; i++)
                {
                    builder.Append($" {Property3[i]}");
                }
                return builder.ToString();
            }
        }

        public class JustAClass
        
        {
            public int A { get; }
            public int B { get; }
            public int C { get; }

            public string Property1 { get; set; }
            public double Property2 { get; set; }
            public float Property3 { get; set; }
            public long Property4 { get; set; }
            public int Property5 { get; set; }
            public ClassInClass Property6 { get; set; }

            public JustAClass
            (int a, int b, int c)
            {
                A = a;
                B = b;
                C = c;
            }

            public JustAClass
            ()
            {

            }

            public override string ToString()
            {
                return $"A: {A}\nB: {B}\nC: {C}\nProperty1: {Property1}\nProperty2: {Property2}\nProperty3: {Property3}\nProperty4: {Property4}\nProperty5: {Property5}\nProperty6: {Property6}";
            }
        }

        public static void Main(string[] argv)
        {
            Faker faker = new Faker();
            JustAClass test = faker.Create<JustAClass>();
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}
