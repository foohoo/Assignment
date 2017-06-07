using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Trains
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                if (args[0] == string.Empty)
                    throw new ArgumentException("The argument should be a string containing a path");

                var inputGraphText = GetFileFromPath(args);

                if(string.IsNullOrEmpty(inputGraphText)) throw new FileNotFoundException("The input text file could not be found.");



                return;
            }

            throw new ArgumentException("There should be one argument. A path string to an input text file.");
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
    }
}
