using System;
using System.Collections.Generic;

namespace Trains.Entities
{
    public class Graph
    {
        public Dictionary<char, Node> NodeMap;

        public List<Node> Nodes { get; set; }

        public Graph(string[] inputNodes)
        {
            NodeMap = new Dictionary<char, Node>();
            Nodes = new List<Node>();

            foreach(var node in inputNodes)
            {
                var fromTown = node[0];
                var toTown = node[1];
                var distance = int.Parse(node.Substring(2));

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
    }
}