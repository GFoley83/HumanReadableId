using System;
using System.Linq;
using HumanReadableIdCreator;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Default: {HumanReadableId.Create()}");

            var id = HumanReadableId.Create(WordType.Adjective, WordType.Verb, WordType.Verb, WordType.Animal);

            Console.WriteLine($"Custom pattern: {id}");

            id = HumanReadableId.Generate(new Config
            {
                SeparationChar = string.Empty,
                LowerCasePassphrase = false
            }, WordType.Verb, WordType.Adjective, WordType.Animal);

            Console.WriteLine($"Custom pattern w/ config: {id}");

            // Adjectives: 1501 Verbs: 633 Animals: 1749
            Console.WriteLine($"Adjectives: {Words.Adjectives.Count()} Verbs: {Words.Verbs.Count()} Animals: {Words.Animals.Count()} ");

            Console.ReadLine();
        }
    }
}
