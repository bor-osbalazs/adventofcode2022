using System;
using System.IO;

namespace AdventOfCode
{
    class Core
    {
        public static string directoryPath = Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            /*CalorieCounter calorieCounter = new CalorieCounter();
            calorieCounter.ShowMaxCalorie();
            calorieCounter.ShowTopCalorie(3);*/

            RockPaperScissorsSimulator rockPaperScissorsSimulator = new RockPaperScissorsSimulator();
            rockPaperScissorsSimulator.ShowPoints();

            Console.ReadKey();
        }
    }
}
