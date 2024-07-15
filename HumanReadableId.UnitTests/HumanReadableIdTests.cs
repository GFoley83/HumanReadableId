using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HumanReadableId.UnitTests
{
    public class HumanReadableIdTests
    {
        [Test]
        public void ShouldGenerateAnId()
        {
            var id = HumanReadableId.Generate();
            Assert.IsNotEmpty(id);
        }

        [Test]
        public void ShouldHaveWordTypeArraysForAdjectivesVerbsAndAnimals()
        {
            Assert.IsTrue(Words.Adjectives.Length > 0);
            Assert.IsTrue(Words.Verbs.Length > 0);
            Assert.IsTrue(Words.Animals.Length > 0);
            Console.WriteLine($"Adjectives: {Words.Adjectives.Length} Verbs: {Words.Verbs.Length} Animals: {Words.Animals.Length}");
        }

        [Test]
        public void ShouldGenerateAnIdWithExpectedWordCountByDefault()
        {
            var id = HumanReadableId.Generate();
            var words = id.Split('-');
            Assert.AreEqual(words.Length, HumanReadableId.DefaultPattern.Length);
        }

        [Test]
        public void ShouldGenerateAnIdInTheDefaultFormatByDefault()
        {
            var id = HumanReadableId.Generate();
            var words = id.Split('-');

            for (var i = 0; i < HumanReadableId.DefaultPattern.Length; i++)
            {
                Assert.IsTrue(Words.WordIsInWordList(words[i], HumanReadableId.DefaultPattern[i]));
            }
        }

        [Test]
        [TestCase(new[] { WordType.Adjective, WordType.Adjective, WordType.Adjective, WordType.Animal })]
        [TestCase(new[] { WordType.Animal, WordType.Verb, WordType.Adjective })]
        public void ShouldGenerateAnIdWithCustomFormat(WordType[] pattern)
        {
            var id = HumanReadableId.Generate(pattern);
            var words = id.Split('-');

            for (var i = 0; i < pattern.Length; i++)
            {
                Assert.IsTrue(Words.WordIsInWordList(words[i], pattern[i]));
            }
        }

        [Test]
        public void ShouldRespectLowerCasePassphraseConfig()
        {
            var config = new Config { LowerCasePassphrase = true };
            var id = HumanReadableId.Generate(config);

            // Split the ID by the separator to get the individual words
            var words = id.Split(config.SeparationChar);

            // Check that each word is in lowercase
            foreach (var word in words)
            {
                Assert.IsTrue(word.All(char.IsLower), $"Word '{word}' is not in lowercase");
            }
        }

        [Test]
        public void ShouldRespectSeparationCharConfig()
        {
            var config = new Config { SeparationChar = "|" };
            var id = HumanReadableId.Generate(config, WordType.Adjective, WordType.Animal);
            Assert.IsTrue(id.Contains("|"));
            var words = id.Split('|');

            Assert.AreEqual(words.Length, 2); // Adjective and Animal
            Assert.IsTrue(Words.WordIsInWordList(words[0], WordType.Adjective));
            Assert.IsTrue(Words.WordIsInWordList(words[1], WordType.Animal));
        }

        [Test]
        public void ShouldHandleEmptyPattern()
        {
            var id = HumanReadableId.Generate(new WordType[] { });
            var words = id.Split('-');
            Assert.AreEqual(words.Length, HumanReadableId.DefaultPattern.Length);
        }

        [Test]
        public void ShouldGenerateIdWithLargePattern()
        {
            var largePattern = Enumerable.Repeat(WordType.Adjective, 100).ToArray();
            var id = HumanReadableId.Generate(largePattern);
            var words = id.Split('-');
            Assert.AreEqual(words.Length, largePattern.Length);
        }

        [Test]
        public void ShouldHandleNullPattern()
        {
            var id = HumanReadableId.Generate(null, null);
            var words = id.Split('-');
            Assert.AreEqual(words.Length, HumanReadableId.DefaultPattern.Length);
        }

        [Test]
        [TestCase(1000000)]
        public void ShouldGenerateUniqueIds(int numIdsToGenerate)
        {
            static string GenerateIdAndCheckForDupes(ICollection<string> hashSet)
            {
                var id = HumanReadableId.Generate();
                if (hashSet.Contains(id))
                {
                    id = GenerateIdAndCheckForDupes(hashSet);
                }

                return id;
            }

            var ids = new HashSet<string>();
            for (var i = 0; i < numIdsToGenerate; i++)
            {
                ids.Add(GenerateIdAndCheckForDupes(ids));
            }

            Assert.AreEqual(ids.Count, numIdsToGenerate);
        }
    }
}
