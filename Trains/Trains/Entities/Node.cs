using System.Collections.Generic;

namespace Trains.Entities
{
    public class Node
    {
        public char Name { get; private set; }
        public List<Edge> OutgoingEdges { get; set; }

        public Node(char name)
        {
            OutgoingEdges = new List<Edge>();
            Name = name;
        }

        public void AddDestination(Node destinationNode, int distance)
        {
            OutgoingEdges.Add(new Edge(destinationNode, distance));
        }
    }
}