using TrainTicketMachine.Searching;
using Xunit;

namespace TrainTicketMachineTests
{
    public class StationsSearchTest
    { 
        [Fact]
        public void ShouldFindStationsAndNextCharacters()
        {
            var givenStations = new []{ "DARTFORD", "DARTMOUTH", "TOWERHILL" };
            var searchString = "DART";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);
            Assert.Contains("DARTFORD", stations);
            Assert.Contains("DARTMOUTH", stations);
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
            Assert.Contains("LIVERPOOL LIME", stations);
            Assert.Contains("LIVERPOOL LIME STREET", stations);
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
            Assert.Contains("LONDON BRIDGE", stations);
            Assert.Contains("LONDONDERRY", stations);
            Assert.Contains(' ', characters);
            Assert.Contains('D', characters);
        }

        [Fact]
        public void ShouldFindStationsTrimmingSpaces()
        {
            var givenStations = new[] { "LIVERPOOL", "LONDON BRIDGE", "LONDONDERRY" };
            var searchString = "  LoNDoN  ";
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);
            Assert.Contains("LONDON BRIDGE", stations);
            Assert.Contains("LONDONDERRY", stations);
            Assert.Contains(' ', characters);
            Assert.Contains('D', characters);
        }

        [Fact]
        public void ShouldReturnEmptyListWhenKeywordIsEmpty()
        {
            var givenStations = new[] { "LIVERPOOL", "LONDON BRIDGE", "LONDONDERRY" };
            var searchString = string.Empty;
            var stationsSearch = StationsSearch.BuildFromStations(givenStations);
            var (stations, characters) = stationsSearch.Search(searchString);
            Assert.Empty(stations);
            Assert.Empty(characters);
        }
    }
}
