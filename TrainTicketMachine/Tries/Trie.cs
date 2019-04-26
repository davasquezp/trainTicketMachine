namespace TrainTicketMachine.Tries
{
    public class Trie
    {
        private TrieNode root;

        public Trie()
        {
            root = TrieNode.Empty();
        }

        public void Insert(string word)
        {
            word = word
                .ToUpper()
                .Trim() + TrieNode.EMPTY_CHAR;

            var children = root.getChildren();
            for (int i = 0; i < word.Length; i++)
            {
                char nextChar = word[i];
                TrieNode node;
                if (children.ContainsKey(nextChar))
                {
                    node = children[nextChar];
                }
                else
                {
                    node = TrieNode.WithKey(nextChar);
                    children.Add(nextChar, node);
                }

                children = node.getChildren();
            }
        }

        public TrieNode Search(string word)
        {
            return string.IsNullOrEmpty(word) ? 
                TrieNode.Empty() :
                Search(root, word[0], word.Remove(0, 1));
        }

        private static TrieNode Search(TrieNode root, char key, string remaining)
        {
            var children = root.getChildren();
            if (children.ContainsKey(key))
            {
                var node = children[key];
                return string.IsNullOrEmpty(remaining) ? node : Search(node, remaining[0], remaining.Remove(0, 1));
            }

            return TrieNode.Empty();
        }
    }
}
