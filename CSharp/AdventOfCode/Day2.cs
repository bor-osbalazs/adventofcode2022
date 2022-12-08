using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class RockPaperScissorsSimulator
    {
        private string inputPath;
        private int points;
        private int pointsTwo;
        private Dictionary<string, int> pointDictionary = new Dictionary<string, int>();

        public RockPaperScissorsSimulator()
        {
            this.inputPath = $"{Core.directoryPath}/Day2-Input.txt";

            PopulateDictionary();
            CalculatePoint();
        }

        private void PopulateDictionary()
        {
            /*
            Opponent        Response
            A: Rock         X: Rock
            B: Paper        Y: Paper
            C: Scissors     Z: Scissors
            */

            pointDictionary.Add("X", 1);
            pointDictionary.Add("Y", 2);
            pointDictionary.Add("Z", 3);
            pointDictionary.Add("Lose", 0);
            pointDictionary.Add("Draw", 3);
            pointDictionary.Add("Win", 6);
        }

        private void CalculatePoint()
        {

            using(StreamReader reader = new StreamReader(inputPath))
            {
                while(reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    if (pointDictionary.TryGetValue(currentLine.Split()[1], out int symbolPoint) == true && 
                        pointDictionary.TryGetValue(IsWinOrLoseOrDraw(currentLine.Split()[0], currentLine.Split()[1]), out int statusPoint) == true)
                    {
                        points += symbolPoint;
                        points += statusPoint;
                    }

                    if (pointDictionary.TryGetValue(DeterminateOwnSymbol(currentLine.Split()[0], currentLine.Split()[1]), out int symbolPointTwo) == true &&
                        pointDictionary.TryGetValue(DeterminateGameEnding(currentLine.Split()[1]), out int statusPointTwo) == true)
                    {
                        pointsTwo += symbolPointTwo;
                        pointsTwo += statusPointTwo;
                    }
                }
            }
        }

        private string IsWinOrLoseOrDraw(string opponentSymbol, string ownSymbol)
        {
            string winStatus = "Win";
            string drawStatus = "Draw";
            string loseStatus = "Lose";

            string matchStatus = "";

            if (opponentSymbol == "A" && ownSymbol == "Y")
            {
                matchStatus = winStatus;
            }
            else if (opponentSymbol == "B" && ownSymbol == "Z")
            {
                matchStatus = winStatus;
            }
            else if (opponentSymbol == "C" && ownSymbol == "X")
            {
                matchStatus = winStatus;
            }
            else if (opponentSymbol == "A" && ownSymbol == "X")
            {
                matchStatus = drawStatus;
            }
            else if (opponentSymbol == "B" && ownSymbol == "Y")
            {
                matchStatus = drawStatus;
            }
            else if (opponentSymbol == "C" && ownSymbol == "Z")
            {
                matchStatus = drawStatus;
            }
            else if (opponentSymbol == "A" && ownSymbol == "Z")
            {
                matchStatus = loseStatus;
            }
            else if (opponentSymbol == "B" && ownSymbol == "X")
            {
                matchStatus = loseStatus;
            }
            else if (opponentSymbol == "C" && ownSymbol == "Y")
            {
                matchStatus = loseStatus;
            }

            return matchStatus;
        }

        private string DeterminateGameEnding(string ownSymbol)
        {
            string winStatus = "Win";
            string drawStatus = "Draw";
            string loseStatus = "Lose";

            string matchStatus = "";

            if (ownSymbol == "X")
            {
                matchStatus = loseStatus;
            }
            else if (ownSymbol == "Y")
            {
                matchStatus = drawStatus;
            }
            else if (ownSymbol == "Z")
            {
                matchStatus = winStatus;
            }

            return matchStatus;
        }

        private string DeterminateOwnSymbol(string opponentSymbol, string statusSymbol)
        {
            /*
            Opponent        Response
            A: Rock         X: Rock
            B: Paper        Y: Paper
            C: Scissors     Z: Scissors
            */

            string rockSymbol = "X";
            string paperSymbol = "Y";
            string scissorsSymbol = "Z";

            string ownSymbol = "";

            if (opponentSymbol == "A" && DeterminateGameEnding(statusSymbol) == "Win")
            {
                ownSymbol = paperSymbol;
            }
            else if (opponentSymbol == "B" && DeterminateGameEnding(statusSymbol) == "Win")
            {
                ownSymbol = scissorsSymbol;
            }
            else if (opponentSymbol == "C" && DeterminateGameEnding(statusSymbol) == "Win")
            {
                ownSymbol = rockSymbol;
            }
            else if (opponentSymbol == "A" && DeterminateGameEnding(statusSymbol) == "Draw")
            {
                ownSymbol = rockSymbol;
            }
            else if (opponentSymbol == "B" && DeterminateGameEnding(statusSymbol) == "Draw")
            {
                ownSymbol = paperSymbol;
            }
            else if (opponentSymbol == "C" && DeterminateGameEnding(statusSymbol) == "Draw")
            {
                ownSymbol = scissorsSymbol;
            }
            else if (opponentSymbol == "A" && DeterminateGameEnding(statusSymbol) == "Lose")
            {
                ownSymbol = scissorsSymbol;
            }
            else if (opponentSymbol == "B" && DeterminateGameEnding(statusSymbol) == "Lose")
            {
                ownSymbol = rockSymbol;
            }
            else if (opponentSymbol == "C" && DeterminateGameEnding(statusSymbol) == "Lose")
            {
                ownSymbol = paperSymbol;
            }

            return ownSymbol;
        }

        public void ShowPoints()
        {
            Console.WriteLine($"First points: {points}");
            Console.WriteLine($"Second points: {pointsTwo}");
        }
    }
}
