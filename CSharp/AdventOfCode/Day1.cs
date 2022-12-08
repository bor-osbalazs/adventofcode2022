using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class CalorieCounter
    {
        private string inputFilePath;
        private List<List<int>> elvesCaloriesFoodList = new List<List<int>>();
        private List<int> elvesCaloriesSumList = new List<int>();

        public CalorieCounter()
        {
            this.inputFilePath = $"{Core.directoryPath}/CaloriesInput.txt";

            PopulateCaloriesList();
            AddCaloriesTogether();
        }

        private void PopulateCaloriesList()
        {
            elvesCaloriesFoodList.Add(new List<int>());
            int elfCounter = 0;

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    if (currentLine == "")
                    {
                        elvesCaloriesFoodList.Add(new List<int>());
                        elfCounter++;
                    }
                    else
                    {
                        elvesCaloriesFoodList[elfCounter].Add(Convert.ToInt32(currentLine));
                    }
                }
            }
        }

        private void AddCaloriesTogether()
        {
            for (int currentElfIndex = 0; currentElfIndex < elvesCaloriesFoodList.Count(); currentElfIndex++)
            {
                int currentCalorieSum = 0;
                int currentElfFoodCount = elvesCaloriesFoodList[currentElfIndex].Count();

                for (int currentFoodIndex = 0; currentFoodIndex < currentElfFoodCount; currentFoodIndex++)
                {
                    currentCalorieSum += elvesCaloriesFoodList[currentElfIndex][currentFoodIndex];

                    if (currentFoodIndex == currentElfFoodCount - 1)
                    {
                        elvesCaloriesSumList.Add(currentCalorieSum);
                    }
                }
            }
        }

        private List<int> SortCaloriesList()
        {
            List<int> sortedElvesCaloriesSumList = elvesCaloriesSumList;

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
            for (int currentElfIndex = 0; currentElfIndex < elvesCaloriesFoodList.Count(); currentElfIndex++)
            {
                Console.WriteLine($"{currentElfIndex}. elf's calorie is {elvesCaloriesSumList[currentElfIndex]}");
            }
        }

        public void ShowMaxCalorie()
        {
            int maxCalories = elvesCaloriesSumList.Max();

            Console.WriteLine($"The most calories an elf has is: {maxCalories}");

        }

        public void ShowTopCalorie(int topElvesNumber)
        {
            Console.WriteLine($"The top {topElvesNumber} elves calories added together is: {AddTopCaloriesTogether(topElvesNumber)}");
        }
    }
}
