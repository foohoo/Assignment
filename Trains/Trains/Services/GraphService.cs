using System;
using Trains.Entities;
using Trains.Interfaces;

namespace Trains.Services
{
    public class GraphService : IGraphService
    {
        public Graph CreateGraph(string inputGraphText)
        {
            var nodeArray = inputGraphText.Split(new []{", "}, StringSplitOptions.None);

            var graph = new Graph(nodeArray);

            return graph;
        }
    }
}
