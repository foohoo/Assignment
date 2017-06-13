namespace Trains.Entities
{
    public class Edge
    {
        public Node Destination { get; private set; }
        public int Distance { get; private set; }

        public Edge(Node destination, int distance)
        {
            Destination = destination;
            Distance = distance;
        }
    }
}