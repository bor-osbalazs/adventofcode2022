using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class ElfCaloriesCounter
    {
        private string inputFile;
        private List<List<int>> elfCalories = new List<List<int>>();
        private List<int> elfCaloriesSum = new List<int>();

        public ElfCaloriesCounter()
        {
            this.inputFile = $"{Directory.GetCurrentDirectory()}/CaloriesInput.txt";

            PopulateCaloriesList();
            AddCaloriesTogether();
        }

        private void PopulateCaloriesList()
        {
            elfCalories.Add(new List<int>());
            int elfCounter = 0;

            using (StreamReader reader = new StreamReader(inputFile))
            {
                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    if (currentLine == "")
                    {
                        elfCalories.Add(new List<int>());
                        elfCounter++;
                    }
                    else
                    {
                        elfCalories[elfCounter].Add(Convert.ToInt32(currentLine));
                    }
                }
            }
        }

        private void AddCaloriesTogether()
        {
            for (int currentElf = 0; currentElf < elfCalories.Count(); currentElf++)
            {
                int currentCaloriesSum = 0;
                int currentFoodSize = elfCalories[currentElf].Count();

                for (int currentFood = 0; currentFood < currentFoodSize; currentFood++)
                {
                    currentCaloriesSum += elfCalories[currentElf][currentFood];

                    if (currentFood == currentFoodSize - 1)
                    {
                        elfCaloriesSum.Add(currentCaloriesSum);
                    }
                }
            }
        }

        public void ReadCaloriesList()
        {
            for (int i = 0; i < elfCalories.Count(); i++)
            {
                Console.WriteLine($"{i}'s elf's calories is {elfCaloriesSum[i]}");
            }
        }

        public void ReadMaxCalories()
        {
            int maxCalories = elfCaloriesSum.Max();

            Console.WriteLine(maxCalories);

        }
    }
}
