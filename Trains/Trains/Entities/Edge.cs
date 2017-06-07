namespace Trains.Entities
{
    public class Edge
    {
        public Node Destination { get; }
        public int Distance { get; }

        public Edge(Node destination, int distance)
        {
            Destination = destination;
            Distance = distance;
        }
    }
}