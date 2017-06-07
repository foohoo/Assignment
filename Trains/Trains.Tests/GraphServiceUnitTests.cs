using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Trains.Services;

namespace Trains.Tests
{
    [TestFixture]
    public class GraphServiceUnitTests
    {
        private GraphService _graphService;

        [SetUp]
        public void Setup()
        {
            _graphService = new GraphService();
        }

        [Test]
        public void Should_Create_Graph_From_Input_Text()
        {
            var inputGraphText = "AB1";

            var graph = _graphService.CreateGraph(inputGraphText);

            Assert.That(graph.Nodes.Count, Is.EqualTo(2));
            Assert.That(graph.NodeMap.Count, Is.EqualTo(2));

            Assert.That(graph.NodeMap['A'].Name, Is.EqualTo('A'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges.Count, Is.EqualTo(1));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[0].Destination.Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[0].Distance, Is.EqualTo(1));

            Assert.That(graph.NodeMap['B'].Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['B'].Name, Is.EqualTo('B'));
        }

    }
}
