using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    class Day6
    {
        string inputPath;

        List<char> characterList = new List<char>();
        int markerIndex;

        public Day6()
        {
            this.inputPath = $"{Core.directoryPath}/Day6-Input.txt";

            ReadFromInput();
        }

        private void ReadFromInput()
        {
            int characterCounter = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while (reader.EndOfStream == false)
                {
                    char currentCharacter = Convert.ToChar(reader.Read());

                    if (characterList.Count == 4)
                    {
                        if (IsMarker() == true)
                        {
                            markerIndex = characterCounter;
                            break;
                        }
                    }

                    UpdateCharacterList(currentCharacter);

                    characterCounter++;
                }
            }
        }

        private void UpdateCharacterList(char character)
        {
            if (characterList.Count < 4)
            {
                characterList.Add(character);
            }
            else
            {
                characterList.RemoveAt(0);
                characterList.Add(character);
            }
        }

        private bool IsMarker()
        {
            bool isMarker = false;

            string currentCharactersInString = "";

            for (int index = 0; index < characterList.Count; index++)
            {
                currentCharactersInString += characterList[index];
            }

            bool isFirstCharacterOccuresOnce = currentCharactersInString.IndexOf(characterList[0]) == currentCharactersInString.LastIndexOf(characterList[0]);
            bool isSecondCharacterOccuresOnce = currentCharactersInString.IndexOf(characterList[1]) == currentCharactersInString.LastIndexOf(characterList[1]);
            bool isThirdCharacterOccuresOnce = currentCharactersInString.IndexOf(characterList[2]) == currentCharactersInString.LastIndexOf(characterList[2]);
            bool isFourthCharacterOccuresOnce = currentCharactersInString.IndexOf(characterList[3]) == currentCharactersInString.LastIndexOf(characterList[3]);

            if (isFirstCharacterOccuresOnce == true && isSecondCharacterOccuresOnce == true && isThirdCharacterOccuresOnce == true && isFourthCharacterOccuresOnce == true)
            {
                isMarker = true;
            }

            return isMarker;
        }

        public void ShowMarkerIndex()
        {
            Console.WriteLine($"Marker index is: {markerIndex}");
        }
    }
}
