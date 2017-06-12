using System;
using System.IO;
using Trains.Entities;
using Trains.Services;

namespace Trains
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\n---------- Trains ----------\n");
            Console.WriteLine("Please enter 'help' to see a list of commands");

            try
            {
                if (args != null && args.Length == 1)
                {
                    if (args[0] == string.Empty) throw new ArgumentException("The argument should be a string containing a path to an input text file");

                    var inputGraphText = GetFileFromPath(args);

                    if (string.IsNullOrEmpty(inputGraphText)) throw new FileNotFoundException("The input text file could not be found.");

                    var graphService = new GraphService(new Graph(inputGraphText));
                    Console.WriteLine("\nTowns: " + graphService.PrintNodes());

                    while (true)
                    {
                        string options;
                        var command = GetInputCommand(out options);

                        switch (command.ToLower())
                        {
                            case "exit":
                                {
                                    return;
                                }
                            case "help":
                                {
                                    Console.WriteLine("- enter 'exit' to exit the application");
                                    Console.WriteLine("- enter 'tests' to run the test data");
                                    Console.WriteLine("\n-- commands for working with the input graph --\n");
                                    Console.WriteLine("- 'route' - this will find the distance of a given route e.g. 'route A-B-C'");
                                    Console.WriteLine("- 'trips-maxstops' - this will find the number of journeys with a maxmium number of stops e.g. 'trips-maxstops C-C-3'");
                                    Console.WriteLine("- 'trips-exactstops' - this will find the number of journeys with an exact number of stops e.g. 'trips-exactstops C-C-3'");
                                    Console.WriteLine("- 'shortest' - this will find the shortest distance between two stops e.g. 'shortest C-C'");
                                    Console.WriteLine("- 'route-maxdistance' - this will find the number of routes between two stops below a maximum distance e.g. 'route-maxdistance C-C-30'");
                                    break;
                                }
                            case "tests":
                                {
                                    RunTestData();
                                    break;
                                }
                            case "route":
                                {
                                    var route = options.ToUpper().Replace("-", "").ToCharArray();
                                    Console.WriteLine("\n" + graphService.FindDistanceForJourney(route));
                                    break;
                                }
                            case "trips-maxstops":
                                {
                                    var trip = options.ToUpper().Replace("-", "");
                                    Console.WriteLine("\n" + graphService.FindJourneysWithMaxStopsFor(trip[0], trip[1], int.Parse(trip.Substring(2))));
                                    break;
                                }
                            case "trips-exactstops":
                                {
                                    var trip = options.ToUpper().Replace("-", "");
                                    Console.WriteLine("\n" + graphService.FindJourneysWithExactStopsFor(trip[0], trip[1], int.Parse(trip.Substring(2))));
                                    break;
                                }
                            case "shortest":
                                {
                                    var trip = options.ToUpper().Replace("-", "").ToCharArray();
                                    Console.WriteLine("\n" + graphService.FindShortestDistanceBetween(trip[0], trip[1]));
                                    break;
                                }
                            case "route-maxdistance":
                                {
                                    var trip = options.ToUpper().Replace("-", "");
                                    Console.WriteLine("\n" + graphService.FindNumberOfRoutesForJourneyWithMaxDistance(trip[0], trip[1], int.Parse(trip.Substring(2))));
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Not a valid command, please use 'help' to see a list of available commands");
                                    break;
                                }
                        }
                    }
                }

                throw new ArgumentException("There should be one argument. A path string to an input text file.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e);
                throw e;
            }

        }

        private static string GetInputCommand(out string options)
        {
            Console.Write("\nCommand: ");

            var input = Console.ReadLine();
            int commandIndex = input.IndexOf(' ');

            var command = string.Empty;
            options = string.Empty;

            if (commandIndex > -1)
            {
                command = input.Substring(0, commandIndex);
                options = input.Substring(commandIndex + 1, input.Length - commandIndex - 1);
            }
            else
            {
                command = input;
            }
            return command;
        }


        private static string GetFileFromPath(string[] args)
        {
            if (!File.Exists(args[0])) return null;

            using (var sr = new StreamReader(args[0]))
            {
                var line = sr.ReadLine();

                return line;
            }
        }

        private static void RunTestData()
        {
            const string testDataInput = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            var graphService = new GraphService(new Graph(testDataInput));

            Console.WriteLine("\n" + graphService.PrintNodes() + "\n");

            Console.WriteLine("Ouput #1: " + graphService.FindDistanceForJourney(new[] { 'A', 'B', 'C' }));
            Console.WriteLine("Ouput #2: " + graphService.FindDistanceForJourney(new[] { 'A', 'D' }));
            Console.WriteLine("Ouput #3: " + graphService.FindDistanceForJourney(new[] { 'A', 'D', 'C' }));
            Console.WriteLine("Ouput #4: " + graphService.FindDistanceForJourney(new[] { 'A', 'E', 'B', 'C', 'D' }));
            Console.WriteLine("Ouput #5: " + graphService.FindDistanceForJourney(new[] { 'A', 'E', 'D' }));
            Console.WriteLine("Ouput #6: " + graphService.FindJourneysWithMaxStopsFor('C', 'C', 3));
            Console.WriteLine("Ouput #7: " + graphService.FindJourneysWithExactStopsFor('A', 'C', 4));
            Console.WriteLine("Ouput #8: " + graphService.FindShortestDistanceBetween('A', 'C'));
            Console.WriteLine("Ouput #9: " + graphService.FindShortestDistanceBetween('B', 'B'));
            Console.WriteLine("Ouput #10: " + graphService.FindNumberOfRoutesForJourneyWithMaxDistance('C', 'C', 30));
        }
    }
}
