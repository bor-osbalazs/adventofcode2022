﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Core
    {
        static void Main(string[] args)
        {
            ElfCaloriesCounter elfcaloriescounter = new ElfCaloriesCounter();
            elfcaloriescounter.ReadMaxCalories();

            Console.ReadKey();
        }
    }
}
