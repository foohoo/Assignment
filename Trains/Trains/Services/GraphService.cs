using System;
using System.Linq;
using Trains.Entities;

namespace Trains.Services
{
    public class GraphService
    {
        public Graph Graph;

        public GraphService(Graph graph)
        {
            Graph = graph;
        }

        public string FindDistanceForJourney(char[] journey)
        {
            var result = Graph.FindDistanceForJourney(journey);
            return string.Format("Distance for route options: {0} is {1}", string.Join("-", journey), result < 0 ? "NO SUCH ROUTE" : result.ToString());
        }

        public string FindJourneysWithMaxStopsFor(char start, char finish, int maxStops)
        {
            var result = Graph.FindJourneysWithMaxStopsFor(start, finish, maxStops);

            var outputString = "Number of trips from {0} to {1} with max stops of {2} is {3}";
            return string.Format(outputString, start, finish, maxStops, result < 0 ? "0" : result.ToString());
        }

        public string FindJourneysWithExactStopsFor(char start, char finish, int stops)
        {
            var outputString = "Number of trips from {0} to {1} with exactly {2} stops is {3}";
            var result = Graph.FindJourneysWithExactStopsFor(start, finish, stops);
            return string.Format(outputString, start, finish, stops, result < 0 ? "0" : result.ToString());
        }

        public string FindShortestDistanceBetween(char start, char finish)
        {
            var result = Graph.FindShortestDistanceBetween(start, finish);
            var outputString = "The shortest distance between {0} and {1} is {2}";
            return string.Format(outputString, start, finish, (result < 0 || result == int.MaxValue) ? "0 - no possible route" : result.ToString());
        }

        public string FindNumberOfRoutesForJourneyWithMaxDistance(char start, char finish, int maxDistance)
        {
            var result = Graph.FindNumberOfRoutesForJourneyWithMaxDistance(start, finish, maxDistance);
            var outputString = "Number of trips from {0} to {1} with max distance of {2} stops is {3}";
            return string.Format(outputString, start, finish, maxDistance, result < 0 ? "0" : result.ToString());
        }

        public string PrintNodes()
        {
            var nodes = string.Empty;
            Graph.Nodes.ForEach(n => nodes += n.Name + " ");
            return nodes;
        }
    }
}
