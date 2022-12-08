using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day1
    {
        private string inputFilePath;

        private List<List<int>> elvesCaloriesListSeperate = new List<List<int>>() { new List<int>() };
        private List<int> elvesCaloriesListSum = new List<int>();

        public Day1()
        {
            this.inputFilePath = $"{Core.directoryPath}/Day1-Input.txt";

            ReadFromInput();
            AddCaloriesTogether();
        }

        private void ReadFromInput()
        {
            int elfCounter = 0;

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    if (currentLine == "")
                    {
                        elvesCaloriesListSeperate.Add(new List<int>());
                        elfCounter++;
                    }
                    else
                    {
                        elvesCaloriesListSeperate[elfCounter].Add(Convert.ToInt32(currentLine));
                    }
                }
            }
        }

        private void AddCaloriesTogether()
        {
            for (int currentElfIndex = 0; currentElfIndex < elvesCaloriesListSeperate.Count(); currentElfIndex++)
            {
                int currentCalorieSum = 0;
                int currentElfFoodCount = elvesCaloriesListSeperate[currentElfIndex].Count();

                for (int currentFoodIndex = 0; currentFoodIndex < currentElfFoodCount; currentFoodIndex++)
                {
                    currentCalorieSum += elvesCaloriesListSeperate[currentElfIndex][currentFoodIndex];

                    if (currentFoodIndex == currentElfFoodCount - 1)
                    {
                        elvesCaloriesListSum.Add(currentCalorieSum);
                    }
                }
            }
        }

        private List<int> SortCaloriesList()
        {
            List<int> sortedElvesCaloriesSumList = elvesCaloriesListSum;

            sortedElvesCaloriesSumList.Sort();
            sortedElvesCaloriesSumList.Reverse();

            return sortedElvesCaloriesSumList;
        }

        private int AddTopCaloriesTogether(int topElvesNumber)
        {
            int topCalorieSum = 0;
            List<int> calorieList = SortCaloriesList();

            for (int currentElfIndex = 0; currentElfIndex < topElvesNumber; currentElfIndex++)
            {
                topCalorieSum += calorieList[currentElfIndex];
            }

            return topCalorieSum;
        }

        public void ShowElfCalorieList()
        {
            for (int currentElfIndex = 0; currentElfIndex < elvesCaloriesListSeperate.Count(); currentElfIndex++)
            {
                Console.WriteLine($"{currentElfIndex}. elf's calorie is {elvesCaloriesListSum[currentElfIndex]}");
            }
        }

        public void ShowMaxCalorie()
        {
            int maxCalories = elvesCaloriesListSum.Max();

            Console.WriteLine($"The most calories an elf has is: {maxCalories}");

        }

        public void ShowTopCalorie(int topElvesNumber)
        {
            Console.WriteLine($"The top {topElvesNumber} elves calories added together is: {AddTopCaloriesTogether(topElvesNumber)}");
        }
    }
}
