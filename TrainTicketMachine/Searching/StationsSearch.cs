using System.Collections.Generic;
using System.Linq;
using TrainTicketMachine.Tries;

namespace TrainTicketMachine.Searching
{
    public class StationsSearch
    {
        private Trie stationsTrie;

        private StationsSearch(Trie stationsTrie)
        {
            this.stationsTrie = stationsTrie;
        }

        public static StationsSearch BuildFromStations(IEnumerable<string> stations)
        {
            var stationsTrie = new Trie();

            foreach (var station in stations)
            {
                stationsTrie.Insert(station);
            }

            return new StationsSearch(stationsTrie);
        }

        public (IEnumerable<string>, IEnumerable<char>) Search(string searchString)
        {
            var sanitizedSearchString = searchString.Trim()
                .ToUpper();

            var trieNode = stationsTrie.Search(sanitizedSearchString);

            return (trieNode.GetWords()
                    .Select(word => sanitizedSearchString + word),
                    trieNode.GetNextCharacters());
        }
    }
}
