using System;
using NUnit.Framework;
using Trains.Entities;
using Trains.Services;

namespace Trains.Tests
{
    [TestFixture]
    public class GraphServiceUnitTests
    {
        GraphService _graphService;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            const string testDataInput = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            _graphService = new GraphService(new Graph(testDataInput));
        }

        [TestCase("ABC", "Distance for route options: A-B-C is 9")]
        [TestCase("AZ", "Distance for route options: A-Z is NO SUCH ROUTE")]
        public void Should_Output_Correct_Result_For_Finding_The_Distance_For_A_Journey(string journeyText, string expectedOutput)
        {
            var output = _graphService.FindDistanceForJourney(journeyText.ToCharArray());
            Assert.That(output, Is.EqualTo(expectedOutput));
        }

        [TestCase('A', 'C', 3, "Number of trips from A to C with max stops of 3 is 3")]
        [TestCase('A', 'Z', 3, "Number of trips from A to Z with max stops of 3 is 0")]
        public void Should_Output_Correct_Result_For_Finding_Journeys_With_Max_Stops(char start, char finish, int maxStops, string expectedOutput)
        {
            var output = _graphService.FindJourneysWithMaxStopsFor(start, finish, maxStops);
            Assert.That(output, Is.EqualTo(expectedOutput));
        }

        [TestCase('A', 'C', 4, "Number of trips from A to C with exactly 4 stops is 3")]
        [TestCase('A', 'Z', 3, "Number of trips from A to Z with exactly 3 stops is 0")]
        public void Should_Output_Correct_Result_For_Finding_Journeys_With_Exact_Stops(char start, char finish, int stops, string expectedOutput)
        {
            var output = _graphService.FindJourneysWithExactStopsFor(start, finish, stops);
            Assert.That(output, Is.EqualTo(expectedOutput));
        }

        [TestCase('A', 'C', "The shortest distance between A and C is 9")]
        [TestCase('A', 'Z', "The shortest distance between A and Z is 0 - no possible route")]
        public void Should_Output_Correct_Result_For_Finding_Shortest_Distance(char start, char finish, string expectedOutput)
        {
            var output = _graphService.FindShortestDistanceBetween(start, finish);
            Assert.That(output, Is.EqualTo(expectedOutput));
        }

        [TestCase('C', 'C', 30, "Number of trips from C to C with max distance of 30 stops is 7")]
        [TestCase('A', 'Z', 30, "Number of trips from A to Z with max distance of 30 stops is 0")]
        public void Should_Output_Correct_Result_For_Finding_Journeys_Below_A_Maximum_Distance(char start, char finish, int maxDistance, string expectedOutput)
        {
            var output = _graphService.FindNumberOfRoutesForJourneyWithMaxDistance(start, finish, maxDistance);
            Assert.That(output, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Should_Print_Node_Names_In_A_Graph()
        {
            Assert.That(_graphService.PrintNodes(), Is.EqualTo("A B C D E "));
        }
    }
}
