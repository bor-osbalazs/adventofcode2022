using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Reorganizer
    {
        string inputPath;
        Dictionary<string, int> itemPriority = new Dictionary<string, int>();
        int prioritySum;
        int badgePrioritySum;

        public Reorganizer()
        {
            this.inputPath = $"{Core.directoryPath}/ReorganizerInput.txt";

            PopulateDictionary();
            CalculatePrioritySum();
        }


        private void PopulateDictionary()
        {
            string alphabetWhole = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int currentLetterIndex = 0; currentLetterIndex < alphabetWhole.Length; currentLetterIndex++)
            {
                itemPriority.Add(Convert.ToString(alphabetWhole[currentLetterIndex]), currentLetterIndex + 1);
            }
        }

        private void CalculatePrioritySum()
        {
            using (StreamReader reader = new StreamReader(inputPath))
            {
                int elfCounter = 0;
                List<string> elvesComparmentList = new List<string>();

                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    Tuple<string, string> comparments = new Tuple<string, string>(FindCompartments(currentLine).Item1, FindCompartments(currentLine).Item2);
                    UpdatePrioritySum(comparments);

                    elvesComparmentList.Add(currentLine);

                    if (elfCounter < 2)
                    {
                        elfCounter++;
                    }
                    else
                    {
                        UpdateBadgePrioritySum(elvesComparmentList);
                        elvesComparmentList.Clear();
                        elfCounter = 0;
                    }
                }
            }
        }

        private void UpdatePrioritySum(Tuple<string, string> comparments)
        {
            List<string> alreadyPrioritizedLetter = new List<string>();

            for (int firstComparmentLetterIndex = 0; firstComparmentLetterIndex < comparments.Item1.Length; firstComparmentLetterIndex++)
            {
                for (int secondComparmentLetterIndex = 0; secondComparmentLetterIndex < comparments.Item2.Length; secondComparmentLetterIndex++)
                {
                    bool isLetterSame = comparments.Item1[firstComparmentLetterIndex] == comparments.Item2[secondComparmentLetterIndex];
                    bool isLetterInDictionary = itemPriority.TryGetValue(Convert.ToString(comparments.Item1[firstComparmentLetterIndex]), out int priorityValue);
                    bool isLetterAlreadyPrioritized = alreadyPrioritizedLetter.Contains(Convert.ToString(comparments.Item1[firstComparmentLetterIndex]));

                    if (isLetterSame == true && isLetterInDictionary == true && isLetterAlreadyPrioritized == false)
                    {
                        alreadyPrioritizedLetter.Add(Convert.ToString(comparments.Item1[firstComparmentLetterIndex]));
                        prioritySum += priorityValue;
                    }
                }
            }
        }

        private void UpdateBadgePrioritySum(List<string> elvesComparmentList)
        {
            List<string> alreadyPrioritizedLetter = new List<string>();

            for (int firstElfComparmentLetterIndex = 0; firstElfComparmentLetterIndex < elvesComparmentList[0].Count(); firstElfComparmentLetterIndex++)
            {
                for (int secondElfComparmentLetterIndex = 0; secondElfComparmentLetterIndex < elvesComparmentList[1].Count(); secondElfComparmentLetterIndex++)
                {
                    bool isFirstAndSecondElfLettersSame = elvesComparmentList[0][firstElfComparmentLetterIndex] == elvesComparmentList[1][secondElfComparmentLetterIndex];

                    if (isFirstAndSecondElfLettersSame == true)
                    {
                        for (int thirdElfComparmentLetterIndex = 0; thirdElfComparmentLetterIndex < elvesComparmentList[2].Count(); thirdElfComparmentLetterIndex++)
                        {
                            bool isSecondAndThirdElfLettersSame = elvesComparmentList[1][secondElfComparmentLetterIndex] == elvesComparmentList[2][thirdElfComparmentLetterIndex];
                            bool isLetterInDictionary = itemPriority.TryGetValue(Convert.ToString(elvesComparmentList[0][firstElfComparmentLetterIndex]), out int priorityValue);
                            bool isAlreadyPrioritized = alreadyPrioritizedLetter.Contains(Convert.ToString(elvesComparmentList[0][firstElfComparmentLetterIndex]));

                            if (isSecondAndThirdElfLettersSame == true && isLetterInDictionary == true && isAlreadyPrioritized == false)
                            {
                                alreadyPrioritizedLetter.Add(Convert.ToString(elvesComparmentList[0][firstElfComparmentLetterIndex]));
                                badgePrioritySum += priorityValue;
                            }
                        }
                    }
                }
            }
        }

        private (string, string) FindCompartments(string currentLine)
        {
            int currentLineHalfLength = currentLine.Length / 2;

            string firstComparment = currentLine.Substring(0, currentLineHalfLength);
            string secondComparment = currentLine.Substring(currentLineHalfLength, currentLineHalfLength);

            return (firstComparment, secondComparment);
        }

        public void ShowPrioritySum()
        {
            Console.WriteLine($"Priority sum is: {prioritySum} and Badge Priority sum is: {badgePrioritySum}");
        }
    }
}
