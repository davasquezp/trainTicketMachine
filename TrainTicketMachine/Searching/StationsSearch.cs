using System.Collections.Generic;
using System.Linq;
using TrainTicketMachine.Tries;

namespace TrainTicketMachine.Searching
{
    public class StationsSearch
    {
        private Trie stationsLexicalTree;

        private StationsSearch(Trie stationsLexicalTree)
        {
            this.stationsLexicalTree = stationsLexicalTree;
        }

        public static StationsSearch BuildFromStations(IEnumerable<string> stations)
        {
            var stationsLexicalTree = new Trie();

            foreach (var station in stations)
            {
                stationsLexicalTree.Insert(station);
            }

            return new StationsSearch(stationsLexicalTree);
        }

        public (IEnumerable<string>, IEnumerable<char>) Search(string searchString)
        {
            searchString = searchString.Trim()
                .ToUpper();

            var lexicalTreeNode = stationsLexicalTree.Search(searchString);

            if (lexicalTreeNode.IsLeaf())
            {
                return (Enumerable.Empty<string>(), Enumerable.Empty<char>());
            }
         
            return (lexicalTreeNode.GetWords()
                    .Select(word => searchString + word),
                lexicalTreeNode.getChildren()
                    .Select(x => x.Key));
        }
    }
}
