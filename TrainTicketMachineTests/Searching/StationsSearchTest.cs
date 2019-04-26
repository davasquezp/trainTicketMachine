using System.Linq;
using TrainTicketMachine.Searching;
using Xunit;

namespace TrainTicketMachineTests
{
    public class StationsSearchTest
    { 
        [Fact]
        public void ShouldFindStationsAndNextCharacters()
        {
            var givenStations = new []{ "DARTFORD", "DARTMOUTH", "DARTSHIRE", "TOWERHILL" };
            var searchString = "DART";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);

            Assert.Equal(3, stations.Count());
            Assert.Contains("DARTSHIRE", stations);
            Assert.Contains("DARTFORD", stations);
            Assert.Contains("DARTMOUTH", stations);

            Assert.Equal(3, characters.Count());
            Assert.Contains('S', characters);
            Assert.Contains('F',  characters);
            Assert.Contains('M', characters);
        }

        [Fact]
        public void ShouldFindStationsAndNextCharactersWithWholeWord()
        {
            var givenStations = new[] { "LIVERPOOL LIME", "LIVERPOOL LIME STREET", "PADDINGTON" };
            var searchString = "LIVERPOOL LIME";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);

            Assert.Equal(2, stations.Count());
            Assert.Contains("LIVERPOOL LIME", stations);
            Assert.Contains("LIVERPOOL LIME STREET", stations);

            Assert.Single(characters);
            Assert.Contains(' ', characters);
        }

        [Fact]
        public void ShouldReturnEmptyListWhenKeywordNotFound()
        {
            var givenStations = new[] { "LIVERPOOL", "LONDON BRIDGE", "PADDINGTON" };
            var searchString = "LONDONDERRY";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);
            Assert.Empty(stations);
            Assert.Empty(characters);
        }


        [Fact]
        public void ShouldFindStationsIgnoringCase()
        {
            var givenStations = new[] { "LIVERPOOL", "LONDON BRIDGE", "londonderry" };
            var searchString = "LoNDoN";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);

            Assert.Equal(2, stations.Count());
            Assert.Contains("LONDON BRIDGE", stations);
            Assert.Contains("LONDONDERRY", stations);

            Assert.Equal(2, characters.Count());
            Assert.Contains(' ', characters);
            Assert.Contains('D', characters);
        }

        [Fact]
        public void ShouldFindStationsTrimmingSpaces()
        {
            var givenStations = new[] { "LIVERPOOL", "LONDON BRIDGE", "LONDONDERRY"};
            var searchString = "  LoNDoN  ";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);

            Assert.Equal(2, stations.Count());
            Assert.Contains("LONDON BRIDGE", stations);
            Assert.Contains("LONDONDERRY", stations);

            Assert.Equal(2, characters.Count());
            Assert.Contains(' ', characters);
            Assert.Contains('D', characters);
        }

        [Fact]
        public void ShouldReturnEmptyListWhenKeywordIsEmpty()
        {
            var givenStations = new[] { "LIVERPOOL", "LONDON BRIDGE", "LONDONDERRY"};
            var searchString = string.Empty;
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);

            Assert.Empty(stations);
            Assert.Empty(characters);
        }

        [Fact]
        public void ShouldReturnNotDuplicatedChars()
        {
            var givenStations = new[] { "LIVERPOOL", "LONDONDUNE", "LONDONDERRY" };
            var searchString = "LONDON";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);

            Assert.Equal(2, stations.Count());
            Assert.Contains("LONDONDUNE", stations);
            Assert.Contains("LONDONDERRY", stations);

            Assert.Single(characters);
            Assert.Contains('D', characters);
        }
    }
}
