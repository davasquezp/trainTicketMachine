using System.Collections.Generic;
using System.Linq;

namespace TrainTicketMachine.Tries
{
    public class TrieNode
    {
        public const char EMPTY_CHAR = '\0';
        private readonly char key; 
        private readonly Dictionary<char, TrieNode> children = new Dictionary<char, TrieNode>();

        private TrieNode()
        {
            key = EMPTY_CHAR;
        }

        private TrieNode(char key)
        {
            this.key = key;
        }

        public static TrieNode WithKey(char key)
        {
            return new TrieNode(key);
        }

        public static TrieNode Empty()
        {
            return new TrieNode();
        }

        public Dictionary<char, TrieNode> getChildren()
        {
            return children;
        }

        public IEnumerable<string> GetWords()
        {
            return children.SelectMany(children => GetWords(children.Value)
                                                    .Select(word => word.Remove(word.Length -1)));
        }

        private static IEnumerable<string> GetWords(TrieNode node)
        {
            if (!node.children.Any())
            {
                return new[] { node.key.ToString() };
            }

            return node.getChildren()
                .SelectMany(children => GetWords(children.Value)
                                            .Select(word => node.key + word));
        }

    }
}