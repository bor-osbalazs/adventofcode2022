using System;
using System.IO;

namespace AdventOfCode
{
    class Day4
    {
        string inputPath;
        int fullContainedCounter;
        int containedAtAllCounter;

        public Day4()
        {
            this.inputPath = $"{Core.directoryPath}/Day4-Input.txt";

            ReadInput();
        }

        private void ReadInput()
        {
            fullContainedCounter = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    PopulateArea(currentLine);
                    IsOverlappingAtAll(currentLine);
                }
            }
        }

        private void PopulateArea(string currentLine)
        {
            char[] splitChars = new char[] { '-', ',' };

            int startingIndexElfOne = Convert.ToInt32(currentLine.Split(splitChars)[0]);
            int endingIndexElfOne = Convert.ToInt32(currentLine.Split(splitChars)[1]);

            int startingIndexElfTwo = Convert.ToInt32(currentLine.Split(splitChars)[2]);
            int endingIndexElfTwo = Convert.ToInt32(currentLine.Split(splitChars)[3]);

            string firstElfArea = "";
            string secondElfArea = "";

            for (int currentIndex = startingIndexElfOne; currentIndex < endingIndexElfOne + 1; currentIndex++)
            {
                firstElfArea += "-" + Convert.ToString(currentIndex) + "-";
            }

            for (int currentIndex = startingIndexElfTwo; currentIndex < endingIndexElfTwo + 1; currentIndex++)
            {
                secondElfArea += "-" + Convert.ToString(currentIndex) + "-";
            }

            IsFullyContained(firstElfArea, secondElfArea);
        }

        private void IsFullyContained(string areaOne, string areaTwo)
        {
            if (areaOne.Contains(areaTwo) == true)
            {
                fullContainedCounter++;
            }
            else if (areaTwo.Contains(areaOne) == true)
            {
                fullContainedCounter++;
            }
        }

        private void IsOverlappingAtAll(string currentLine)
        {
            char[] splitChars = new char[] { '-', ',' };

            int startingIndexElfOne = Convert.ToInt32(currentLine.Split(splitChars)[0]);
            int endingIndexElfOne = Convert.ToInt32(currentLine.Split(splitChars)[1]);

            int startingIndexElfTwo = Convert.ToInt32(currentLine.Split(splitChars)[2]);
            int endingIndexElfTwo = Convert.ToInt32(currentLine.Split(splitChars)[3]);

            for (int firstElfAreaIndex = startingIndexElfOne; firstElfAreaIndex < endingIndexElfOne + 1; firstElfAreaIndex++)
            {
                for (int secondElfAreaindex = startingIndexElfTwo; secondElfAreaindex < endingIndexElfTwo + 1; secondElfAreaindex++)
                {
                    if (firstElfAreaIndex == secondElfAreaindex)
                    {
                        containedAtAllCounter++;
                        return;
                    }
                }
            }
        }

        public void ShowContainedPairNumber()
        {
            Console.WriteLine($"{fullContainedCounter} pairs contain fully the partner's area");
            Console.WriteLine($"{containedAtAllCounter} pairs overlap in a single or more area");
        }

    }
}
