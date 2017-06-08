using System;
using Trains.Entities;
using Trains.Interfaces;

namespace Trains.Services
{
    public class GraphService : IGraphService
    {
        public Graph CreateGraph(string inputGraphText)
        {
            var graph = new Graph(inputGraphText);

            return graph;
        }
    }
}
