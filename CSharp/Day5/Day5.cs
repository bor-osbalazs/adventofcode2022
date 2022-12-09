using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day5
    {
        string inputPath;

        Stack<string>[] stack = new Stack<string>[9];

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
                stack[index] = new Stack<string>();
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
                        stack[stackCounter].Push(Convert.ToString(tempListForStacks[index][letterIndex]));
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
                string itemTomove = stack[Convert.ToInt32(currentLine.Split()[3]) - 1].Pop();

                stack[Convert.ToInt32(currentLine.Split()[5]) - 1].Push(itemTomove);
            }
        }

        public void ShowItemsOnTop()
        {
            string itemsOnTop = "";

            for (int index = 0; index < stack.Length; index++)
            {
                itemsOnTop += stack[index].Peek();
            }

            Console.WriteLine(itemsOnTop);
        }

    }
}
