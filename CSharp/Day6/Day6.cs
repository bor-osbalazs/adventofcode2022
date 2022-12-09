using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Day6
    {
        string inputPath;

        List<char> characterList = new List<char>();
        List<char> characterListMessage = new List<char>();

        int markerIndex;
        int markerMessageIndex;

        public Day6()
        {
            this.inputPath = $"{Core.directoryPath}/Day6-Input.txt";

            ReadFromInput();
        }

        private void ReadFromInput()
        {
            int characterCounter = 0;
            bool isMarkerFound = false;
            bool isMessageMarkerFound = false;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while (reader.EndOfStream == false)
                {
                    char currentCharacter = Convert.ToChar(reader.Read());

                    isMarkerFound = IsMarkerFound(characterCounter, isMarkerFound);
                    isMessageMarkerFound = IsMessageMarkerFound(characterCounter, isMessageMarkerFound);

                    UpdateCharacterList(currentCharacter);
                    UpdateCharacterListMessage(currentCharacter);

                    characterCounter++;
                }
            }
        }

        private bool IsMessageMarkerFound(int characterCounter, bool isMessageMarkerFound)
        {
            if (isMessageMarkerFound == false && characterListMessage.Count == 14)
            {
                if (IsMarkerMessage() == true)
                {
                    markerMessageIndex = characterCounter;
                    isMessageMarkerFound = true;
                }
            }

            return isMessageMarkerFound;
        }

        private bool IsMarkerFound(int characterCounter, bool isMarkerFound)
        {
            if (isMarkerFound == false && characterList.Count == 4)
            {
                if (IsMarker() == true)
                {
                    markerIndex = characterCounter;
                    isMarkerFound = true;
                }
            }

            return isMarkerFound;
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

        private void UpdateCharacterListMessage(char character)
        {
            if (characterListMessage.Count < 14)
            {
                characterListMessage.Add(character);
            }
            else
            {
                characterListMessage.RemoveAt(0);
                characterListMessage.Add(character);
            }
        }

        private bool IsMarker()
        {
            bool isMarker = false;

            string currentCharactersInString = PopulateCharacterString(false);

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

        private bool IsMarkerMessage()
        {
            bool isMarker = false;
            bool[] isUniqeCharacters = new bool[14];

            string currentCharactersInString = PopulateCharacterString(true);

            for (int index = 0; index < characterListMessage.Count; index++)
            {
                isUniqeCharacters[index] = currentCharactersInString.IndexOf(characterListMessage[index]) == currentCharactersInString.LastIndexOf(characterListMessage[index]);

                if (isUniqeCharacters[index] == true)
                {
                    isMarker = true;
                }
                else
                {
                    isMarker = false;
                    break;
                }
            }

            return isMarker;
        }

        private string PopulateCharacterString(bool isMessageMarker)
        {
            string currentCharactersInString = "";

            if (isMessageMarker == true)
            {
                for (int index = 0; index < characterListMessage.Count; index++)
                {
                    currentCharactersInString += characterListMessage[index];
                }
            }
            else
            {
                for (int index = 0; index < characterList.Count; index++)
                {
                    currentCharactersInString += characterList[index];
                }
            }

            return currentCharactersInString;
        }

        public void ShowMarkerIndex()
        {
            Console.WriteLine($"Marker index is: {markerIndex} and Message Marker index is: {markerMessageIndex}");
        }
    }
}
