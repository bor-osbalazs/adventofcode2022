using System;
using System.IO;

namespace AdventOfCode
{
    class Core
    {
        public static string directoryPath = Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            /*Day1 day1 = new Day1();
            day1.ShowMaxCalorie();
            day1.ShowTopCalorie(3);*/

            /*Day2 day2 = new Day2();
            day2.ShowPoints();*/

            /*Day3 day3 = new Day3();
            day3.ShowPrioritySum();*/

            /*Day4 day4 = new Day4();
            day4.ShowContainedPairNumber();*/

            /*Day5 day5 = new Day5();
            day5.ShowItemsOnTop();*/

            /*Day6 day6 = new Day6();
            day6.ShowMarkerIndex();*/

            Day7 day7 = new Day7();
            day7.WriteSmallDirectoriesSum();

            Console.ReadKey();
        }
    }
}
