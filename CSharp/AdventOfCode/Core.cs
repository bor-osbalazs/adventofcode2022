using System;
using System.IO;

namespace AdventOfCode
{
    class Core
    {
        public static string directoryPath = Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            //CalorieCounter elfcaloriescounter = new CalorieCounter();
            //elfcaloriescounter.ReadTopCalories();

            RockPaperScissors rockPaperScissors = new RockPaperScissors();
            rockPaperScissors.ShowPoints();

            Console.ReadKey();
        }
    }
}
