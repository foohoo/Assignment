using System.Collections.Generic;
using System.Linq;
using Trains.Entities;

namespace Trains.Helpers
{
    public static class DijkstraAlgorithm
    {
        public static Dictionary<char, int> Dijkstra(Graph graph, Node start, Node finish)
        {
            var towns = new List<Vertex>();
            var journeyDistances = new Dictionary<char, int>();

            foreach (var node in graph.Nodes)
            {
                var initialDistance = node.Name == start.Name ? 0 : int.MaxValue;
                journeyDistances[node.Name] = initialDistance;
                towns.Add(new Vertex(node));
            }

            while (towns.Count(n => !n.Visited) > 0)
            {
                var stop = towns[GetNextSmallestDistance(towns, journeyDistances)];

                if (stop.Town.Name == finish.Name && stop.Visited)
                    return journeyDistances;

                stop.Visited = true;

                foreach (var node in stop.Town.OutgoingEdges)
                {
                    var currDistance = journeyDistances[stop.Town.Name] + node.Distance;

                    if (currDistance > 0 && (currDistance < journeyDistances[node.Destination.Name] || journeyDistances[node.Destination.Name] == 0))
                    {
                        journeyDistances[node.Destination.Name] = currDistance;
                    }
                }
            }

            return journeyDistances;
        }

        private static int GetNextSmallestDistance(IList<Vertex> vertices, IDictionary<char, int> distance)
        {
            var currentMinimum = int.MaxValue;
            var minIndex = 0;

            for (var i = 0; i < vertices.Count(); i++)
            {
                if (distance[vertices[i].Town.Name] <= currentMinimum && !vertices[i].Visited)
                {
                    currentMinimum = distance[vertices[i].Town.Name];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}
