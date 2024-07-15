using System;
using System.Text;
using System.Threading;

namespace HumanReadableIdCreator
{
    public enum WordType
    {
        Adjective,
        Verb,
        Animal
    }

    public class Config
    {
        public bool LowerCasePassphrase { get; set; } = true;
        public string SeparationChar { get; set; } = "-";
    }

    /// <summary>
    /// Generate human readable IDs.
    /// </summary>
    public static class HumanReadableId
    {
        // Thread-safe random number generator
        private static readonly ThreadLocal<Random> _rnd = new ThreadLocal<Random>(() => new Random());

        /// <summary>
        /// Default pattern is:
        /// <code>adjective-adjective-animal-verb-adjective</code>
        /// E.g. <code>WordType[] DefaultPattern = { WordType.Adjective, WordType.Adjective, WordType.Animal, WordType.Verb, WordType.Adjective };</code>
        /// </summary>
        public static readonly WordType[] DefaultPattern = { WordType.Adjective, WordType.Adjective, WordType.Animal, WordType.Verb, WordType.Adjective };

        /// <summary>
        /// Overload method for Generate to use default configuration.
        /// Default behavior returns an ID in the format: <code>adjective-adjective-animal-verb-adjective</code>
        /// </summary>
        /// <param name="pattern">Array of WordType</param>
        /// <returns>Generated passphrase string</returns>
        public static string Create(params WordType[] pattern) => Generate(null, pattern);

        /// <summary>
        /// Default behavior returns an ID in the format: <code>adjective-adjective-animal-verb-adjective</code>
        /// <para>E.g.
        /// <code>happy-energetic-dog-runs-quickly</code></para>
        /// <para>Override "pattern" param to customize order and/or number of words to generate.</para>
        /// </summary>
        /// <param name="config">Configuration options for passphrase generation</param>
        /// <param name="pattern">Defaults to "DefaultPattern" which is: <code>adjective-adjective-animal-verb-adjective</code></param>
        /// <returns>Generated passphrase string</returns>
        public static string Generate(Config config = null, params WordType[] pattern)
        {
            pattern = GetPatternOrDefault(pattern);
            config = config ?? new Config();

            var idBuilder = new StringBuilder();

            for (int i = 0; i < pattern.Length; i++)
            {
                AppendRandomWord(idBuilder, pattern[i]);
                if (i + 1 < pattern.Length)
                {
                    idBuilder.Append(config.SeparationChar);
                }
            }

            var result = idBuilder.ToString();
            return config.LowerCasePassphrase ? result.ToLowerInvariant() : result;
        }

        /// <summary>
        /// Gets the default pattern if the provided pattern is null or empty.
        /// </summary>
        /// <param name="pattern">Array of WordType</param>
        /// <returns>Default pattern if input is null or empty, otherwise the input pattern</returns>
        private static WordType[] GetPatternOrDefault(WordType[] pattern)
        {
            return (pattern == null || pattern.Length == 0) ? DefaultPattern : pattern;
        }

        /// <summary>
        /// Appends a random word of the specified WordType to the StringBuilder.
        /// </summary>
        /// <param name="idBuilder">StringBuilder to append the word to</param>
        /// <param name="wordType">Type of word to append</param>
        private static void AppendRandomWord(StringBuilder idBuilder, WordType wordType)
        {
            switch (wordType)
            {
                case WordType.Adjective:
                    idBuilder.Append(Words.Adjectives[_rnd.Value.Next(Words.Adjectives.Length)]);
                    break;
                case WordType.Verb:
                    idBuilder.Append(Words.Verbs[_rnd.Value.Next(Words.Verbs.Length)]);
                    break;
                case WordType.Animal:
                    idBuilder.Append(Words.Animals[_rnd.Value.Next(Words.Animals.Length)]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(wordType), wordType, null);
            }
        }
    }
}
