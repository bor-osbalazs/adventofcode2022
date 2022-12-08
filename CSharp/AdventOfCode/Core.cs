using System;

namespace AdventOfCode
{
    class Core
    {
        static void Main(string[] args)
        {
            ElfCaloriesCounter elfcaloriescounter = new ElfCaloriesCounter();
            elfcaloriescounter.ReadTopCalories();

            Console.ReadKey();
        }
    }
}
