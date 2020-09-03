using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Trains
{
    public class Program
    {
        private const string DefaultFileName = "easy-01";

        static void Main(string[] args)
        {
            while (true)
            {
                var trainLines = new string[0];
                while (trainLines.Length == 0)
                {
                    Console.WriteLine("\nEnter data file name (without .txt):");
                    var dataFileName = Console.ReadLine();
                    if (string.IsNullOrEmpty(dataFileName))
                    {
                        dataFileName = DefaultFileName;
                    }

                    try
                    {
                        trainLines = File.ReadAllLines($"samples/{dataFileName}.txt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                    
                Console.WriteLine("\nEnter desired destination:");
                var destination = Console.ReadLine()!.ToUpper().First();

                var solution = new TrainsStarter().Start(trainLines, destination);
                Console.WriteLine("\nSolution found:");
                Console.WriteLine(solution);
            }
        }
    }
}
