using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Trains.Entities;
using Trains.Services;

namespace Trains.Tests
{
    [TestFixture]
    public class GraphUnitTests
    {
        #region Setup_Tests

        [Test]
        public void Should_Throw_An_Error_If_Graph_Input_Text_Is_Not_Valid()
        {
            Assert.Throws<ArgumentException>(() => new Graph("AB1df, not correct"));
        }

        [Test]
        public void Should_Create_Graph_From_Input_Text()
        {
            var graph = new Graph("AB1");

            Assert.That(graph.Nodes.Count, Is.EqualTo(2));
            Assert.That(graph.NodeMap.Count, Is.EqualTo(2));

            Assert.That(graph.NodeMap['A'].Name, Is.EqualTo('A'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges.Count, Is.EqualTo(1));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[0].Destination.Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[0].Distance, Is.EqualTo(1));

            Assert.That(graph.NodeMap['B'].Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['B'].Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['B'].OutgoingEdges.Count, Is.EqualTo(0));
        }

        [Test]
        public void Should_Create_Graph_From_Input_Text_Multiple_Towns()
        {
            var inputGraphText = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            var graph = new Graph(inputGraphText);

            Assert.That(graph.Nodes.Count, Is.EqualTo(5));
            Assert.That(graph.NodeMap.Count, Is.EqualTo(5));

            Assert.That(graph.NodeMap['A'].Name, Is.EqualTo('A'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges.Count, Is.EqualTo(3));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[0].Destination.Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[1].Destination.Name, Is.EqualTo('D'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[2].Destination.Name, Is.EqualTo('E'));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[0].Distance, Is.EqualTo(5));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[1].Distance, Is.EqualTo(5));
            Assert.That(graph.NodeMap['A'].OutgoingEdges[2].Distance, Is.EqualTo(7));

            Assert.That(graph.NodeMap['B'].Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['B'].OutgoingEdges.Count, Is.EqualTo(1));
            Assert.That(graph.NodeMap['B'].OutgoingEdges[0].Destination.Name, Is.EqualTo('C'));
            Assert.That(graph.NodeMap['B'].OutgoingEdges[0].Distance, Is.EqualTo(4));

            Assert.That(graph.NodeMap['C'].Name, Is.EqualTo('C'));
            Assert.That(graph.NodeMap['C'].OutgoingEdges.Count, Is.EqualTo(2));
            Assert.That(graph.NodeMap['C'].OutgoingEdges[0].Destination.Name, Is.EqualTo('D'));
            Assert.That(graph.NodeMap['C'].OutgoingEdges[1].Destination.Name, Is.EqualTo('E'));
            Assert.That(graph.NodeMap['C'].OutgoingEdges[0].Distance, Is.EqualTo(8));
            Assert.That(graph.NodeMap['C'].OutgoingEdges[1].Distance, Is.EqualTo(2));

            Assert.That(graph.NodeMap['D'].Name, Is.EqualTo('D'));
            Assert.That(graph.NodeMap['D'].OutgoingEdges.Count, Is.EqualTo(2));
            Assert.That(graph.NodeMap['D'].OutgoingEdges[0].Destination.Name, Is.EqualTo('C'));
            Assert.That(graph.NodeMap['D'].OutgoingEdges[1].Destination.Name, Is.EqualTo('E'));
            Assert.That(graph.NodeMap['D'].OutgoingEdges[0].Distance, Is.EqualTo(8));
            Assert.That(graph.NodeMap['D'].OutgoingEdges[1].Distance, Is.EqualTo(6));

            Assert.That(graph.NodeMap['E'].Name, Is.EqualTo('E'));
            Assert.That(graph.NodeMap['E'].OutgoingEdges.Count, Is.EqualTo(1));
            Assert.That(graph.NodeMap['E'].OutgoingEdges[0].Destination.Name, Is.EqualTo('B'));
            Assert.That(graph.NodeMap['E'].OutgoingEdges[0].Distance, Is.EqualTo(3));
        }
        #endregion

        #region Find_Distance_Tests

        [TestCase("ABC", 9)]
        [TestCase("AD", 5)]
        [TestCase("ADC", 13)]
        [TestCase("AEBCD", 22)]
        [TestCase("AED", -1)]
        public void Should_Find_Distance_For_Given_Journey(string journeyText, int expectedDistance)
        {
            var inputGraphText = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            var graph = new Graph(inputGraphText);

            var journey = journeyText.ToCharArray();

            var distance = graph.FindDistanceForJourney(journey);

            Assert.That(distance, Is.EqualTo(expectedDistance));
        }

        #endregion

        #region Find_Journeys_With_Max_Limit

        [Test]
        public void Should_Find_Number_Of_Journeys_From_Start_To_Destination_Below_A_Max_Stop_Amount()
        {
            var inputGraphText = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            var graph = new Graph(inputGraphText);

            var startingTown = 'C';
            var finishingTown = 'C';
            var maxStops = 3;

            var numberOfJourneys = graph.FindJourneysWithMaxStopsFor(startingTown, finishingTown, maxStops);

            Assert.That(numberOfJourneys, Is.EqualTo(2));
        }

        #endregion

    }
}
