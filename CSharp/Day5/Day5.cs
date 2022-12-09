using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class Day5
    {
        string inputPath;

        Stack<string>[] stack9000 = new Stack<string>[9];
        Stack<string>[] stack9001 = new Stack<string>[9];

        public Day5()
        {
            this.inputPath = $"{Core.directoryPath}/Day5-Input.txt";

            ReadFromInput();
        }

        private void ReadFromInput()
        {
            List<string> tempListForStacks = new List<string>();

            int lineCounter = 0;

            InitilaizeStacks();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    if (lineCounter < 8)
                    {
                        tempListForStacks.Add(currentLine);
                    }
                    else if (lineCounter > 9)
                    {
                        MoveItems(currentLine);
                        MoveMultipleItems(currentLine);
                    }
                    else
                    {
                        PopulateStacks(tempListForStacks);
                    }

                    lineCounter++;
                }
            }
        }

        private void InitilaizeStacks()
        {
            for (int index = 0; index < 9; index++)
            {
                stack9000[index] = new Stack<string>();
                stack9001[index] = new Stack<string>();
            }
        }

        private void PopulateStacks(List<string> tempListForStacks)
        {
            int stackCounter = 0;

            for (int index = tempListForStacks.Count() - 1; index >= 0; index--)
            {
                for (int letterIndex = 1; letterIndex < 34; letterIndex += 4)
                {
                    if (Convert.ToString(tempListForStacks[index][letterIndex]) != " ")
                    {
                        stack9000[stackCounter].Push(Convert.ToString(tempListForStacks[index][letterIndex]));
                        stack9001[stackCounter].Push(Convert.ToString(tempListForStacks[index][letterIndex]));
                    }

                    stackCounter++;
                }

                stackCounter = 0;
            }
        }

        private void MoveItems(string currentLine)
        {
            for (int index = 0; index < Convert.ToInt32(currentLine.Split()[1]); index++)
            {
                string itemTomove = stack9000[Convert.ToInt32(currentLine.Split()[3]) - 1].Pop();

                stack9000[Convert.ToInt32(currentLine.Split()[5]) - 1].Push(itemTomove);
            }
        }

        private void MoveMultipleItems(string currentLine)
        {
            List<string> currentlyMovingItems = new List<string>();

            for (int index = 0; index < Convert.ToInt32(currentLine.Split()[1]); index++)
            {
                currentlyMovingItems.Add(stack9001[Convert.ToInt32(currentLine.Split()[3]) - 1].Pop());
            }

            currentlyMovingItems.Reverse();

            for (int index = 0; index < currentlyMovingItems.Count; index++)
            {
                stack9001[Convert.ToInt32(currentLine.Split()[5]) - 1].Push(currentlyMovingItems[index]);
            }
        }

        public void ShowItemsOnTop()
        {
            string itemsOnTop9000 = "";
            string itemsOnTop9001 = "";

            for (int index = 0; index < stack9000.Length; index++)
            {
                itemsOnTop9000 += stack9000[index].Peek();
                itemsOnTop9001 += stack9001[index].Peek();
            }

            Console.WriteLine($"9000: {itemsOnTop9000}");
            Console.WriteLine($"9001: {itemsOnTop9001}");
        }

    }
}
