using System;

namespace HumanReadableId.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Default: {HumanReadableId.Generate()}");

            var id = HumanReadableId.Generate(new[]
            {
                WordType.Adjective, WordType.Verb, WordType.Verb, WordType.Animal
            });

            Console.WriteLine($"Custom pattern: {id}");

            id = HumanReadableId.Generate(new Config
            {
                SeparationChar = "",
                LowerCasePassphrase = false
            }, WordType.Verb, WordType.Adjective, WordType.Animal);

            Console.WriteLine($"Custom pattern w/ config: {id}");

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(HumanReadableId.Generate(new Config
                {
                    SeparationChar = string.Empty,
                    LowerCasePassphrase = false
                }, WordType.Verb, WordType.Adjective, WordType.Animal));
            }


            Console.ReadLine();
        }
    }
}
