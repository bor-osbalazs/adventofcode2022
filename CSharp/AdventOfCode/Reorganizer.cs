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
                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    Tuple<string, string> comparments = new Tuple<string, string>(FindCompartments(currentLine).Item1, FindCompartments(currentLine).Item2);

                    LoopThroughEveryLetter(comparments);
                }
            }
        }

        private void LoopThroughEveryLetter(Tuple<string, string> comparments)
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

        private (string, string) FindCompartments(string currentLine)
        {
            int currentLineHalfLength = currentLine.Length / 2;

            string firstComparment = currentLine.Substring(0, currentLineHalfLength);
            string secondComparment = currentLine.Substring(currentLineHalfLength, currentLineHalfLength);

            return (firstComparment, secondComparment);
        }

        public void ShowPrioritySum()
        {
            Console.WriteLine($"Priority sum is: {prioritySum}");
        }
    }
}
