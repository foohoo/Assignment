namespace Trains.Entities
{
    public class Vertex
    {
        public Node Town { get; private set; }
        public bool Visited { get; set; }

        public Vertex(Node town, bool visited = false)
        {
            Town = town;
            Visited = visited;
        }
    }
}