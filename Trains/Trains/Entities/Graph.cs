using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Trains.Entities
{
    public class Graph
    {
        public Dictionary<char, Node> NodeMap;

        public List<Node> Nodes { get; set; }

        public Graph(string inputGraphString)
        {         
            NodeMap = new Dictionary<char, Node>();
            Nodes = new List<Node>();

            var inputNodes = inputGraphString.Split(new[] {", "}, StringSplitOptions.None);

            foreach (var node in inputNodes)
            {
                var fromTown = node[0];
                var toTown = node[1];
                int distance;
                var containsDistance = int.TryParse(node.Substring(2), out distance);

                if(!containsDistance) throw new ArgumentException("One of the nodes is not formatted correctly, correct format example: 'AB1'");

                //Add new "from" node if needed
                //Add new "to" node if needed
                //create edge from "from" node to "to" node with correct distance

                var fromNode = NodeMap.ContainsKey(fromTown) ? NodeMap[fromTown] : AddNewNodeToGraphAndMap(fromTown);
                var toNode = NodeMap.ContainsKey(toTown) ? NodeMap[toTown] : AddNewNodeToGraphAndMap(toTown);

                fromNode.AddDestination(toNode, distance);
            }
        }

        private Node AddNewNodeToGraphAndMap(char town)
        {
            var node = new Node(town);
            Nodes.Add(node);
            NodeMap[town] = node;

            return node;
        }

        public int FindDistanceForJourney(char[] journey)
        {
            var totalDistance = 0;

            var currentTown = NodeMap.ContainsKey(journey[0]) ? NodeMap[journey[0]] : null;

            if (currentTown == null) return -1;

            for (var i = 1; i < journey.Length; i++)
            {
                var destinationTown = journey[i];
                var outGoingEdge = currentTown.OutgoingEdges.FirstOrDefault(e => e.Destination.Name == destinationTown);

                if (outGoingEdge == null) return -1;

                totalDistance += outGoingEdge.Distance;
                currentTown = NodeMap[destinationTown];
            }

            return totalDistance;
        }

        public int FindJourneysWithMaxStopsFor(char startingTown, char finishingTown, int maxStops)
        {
            var startingNode = NodeMap.ContainsKey(startingTown) ? NodeMap[startingTown] : null;
            var finishingNode = NodeMap.ContainsKey(finishingTown) ? NodeMap[finishingTown] : null;

            if (startingNode == null || finishingNode == null) return -1;

            var numberOfStops = 0;

            return startingNode.OutgoingEdges.Sum(edge => TravelEdges(edge.Destination, finishingTown, maxStops, numberOfStops, false));
        }


        public int FindJourneysWithExactStopsFor(char startingTown, char finishingTown, int stops)
        {
            var startingNode = NodeMap.ContainsKey(startingTown) ? NodeMap[startingTown] : null;
            var finishingNode = NodeMap.ContainsKey(finishingTown) ? NodeMap[finishingTown] : null;

            if (startingNode == null || finishingNode == null) return -1;

            var numberOfStops = 0;

            return startingNode.OutgoingEdges.Sum(edge => TravelEdges(edge.Destination, finishingTown, stops, numberOfStops, true));
        }


        private int TravelEdges(Node town, char destination, int maxStops, int numberOfStops, bool needExactStops)
        {
            numberOfStops++;

            if (numberOfStops > maxStops)
                return 0;

            if (needExactStops)
            {
                if (town.Name == destination && numberOfStops == maxStops)
                    return 1;
            }
            else
            {
                if (town.Name == destination)
                    return 1;
            }

            return town.OutgoingEdges.Sum(edge => TravelEdges(edge.Destination, destination, maxStops, numberOfStops, needExactStops));
        }

        
    }
}