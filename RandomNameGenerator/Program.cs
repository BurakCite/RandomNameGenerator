using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNameGenerator
{
    class Program
    {
        private static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static readonly double[] Frequency = { 8.12, 1.49, 2.71, 4.32, 12.02, 2.30, 2.03, 5.92, 7.31, 0.10, 0.69, 3.98, 2.61, 6.95, 7.68, 1.82, 0.11, 6.02, 6.28, 9.10, 2.88, 1.11, 2.09, 0.17, 2.11, 0.07 };
        private static readonly string Vowels = "aeiouy";
        private static readonly string Consonants = "bcdfghjklmnpqrstvwxyz";
        private static readonly string[] NameEndings2L = { "us", "el", "im", "ah" };
        private static readonly string[] NameEndings3L = { "sis", "iel", "xus" };
        private static readonly double[] NameEndingFrequency = { 75.00, 15.00, 10.00 };
        private static readonly int NameCount = 20;
        private static readonly int MinNameLength = 3;
        private static readonly int MaxNameLength = 10;
        private static readonly bool PsuedoRandomMode = false;
        private static int RandomNumber = 0;
        static void Main(string[] args)
        {
            RandomNumber = GenerateRandomNumber(0, 100);
            int nameLength = -1;
            for (int i = 0; i < NameCount; i++)
            {
                string name = String.Empty;
                Console.Write(name);
                string previousLetters = String.Empty;
                int nameEnding = GenerateRandomNumber(1, 4);
                nameLength = GenerateRandomNumber(MinNameLength, MaxNameLength + 1) - nameEnding;
                if (nameEnding == 1)
                {
                    nameLength++;
                }
                for (int j = 0; j < nameLength; j++)
                {
                    char letter = GetAppropriateLetter(name);
                    if (j == 0)
                    {
                        letter = letter.ToString().ToUpper()[0];
                    }
                    Console.Write(letter);
                    name += letter;
                }
                if (name.Length >= 2)
                {
                    previousLetters = name.Substring(name.Length - 2).ToLower();
                }
                if (nameEnding == 2)
                {
                    int nameEndingIndex = GenerateRandomNumber(0, NameEndings2L.Length);
                    name += NameEndings2L[nameEndingIndex];
                    Console.Write(NameEndings2L[nameEndingIndex]);
                }
                else if (nameEnding == 3)
                {
                    int nameEndingIndex = GenerateRandomNumber(0, NameEndings3L.Length);
                    name += NameEndings3L[nameEndingIndex];
                    Console.Write(NameEndings3L[nameEndingIndex]);
                }
                name = name.Trim();
                //Console.WriteLine(name);
                Console.Write('\n');
            }

            Console.ReadLine();
        }
        private static char GetAppropriateLetter(string name)
        {
            string previousLetters = String.Empty;
            if (name.Length >= 2)
            {
                previousLetters = name.Substring(name.Length - 2).ToLower();
            }
            char letter = '\0';
            do
            {
                letter = GetRandomLetter();
            }
            while (!IsLetterAppropriate(letter, previousLetters));
            return letter;
        }
        private static int GetFrequencyCounter(double[] frequencyArray)
        {
            int letterIndex1 = GenerateRandomNumber(0, 100);
            int letterIndex2 = GenerateRandomNumber(0, 100);
            double letterIndex = Convert.ToDouble(letterIndex1) + (Convert.ToDouble(letterIndex2) / 100);
            double total = 0.0;
            int counter = -1;
            while (total <= letterIndex)
            {
                counter++;
                try
                {
                    total += frequencyArray[counter];
                }
                catch (Exception e)
                {
                    break;
                }
            }
            return counter;
        }
        private static char GetRandomLetter()
        {
            int counter = GetFrequencyCounter(Frequency);
            char letter = Alphabet[counter];
            return letter;
        }
        private static bool IsLetterAppropriate(char newLetter, string previousLetters)
        {
            if (previousLetters.Length == 0 && newLetter == ' ')
            {
                return false;
            }
            bool isAppropriate = true;
            int letterIndex = -1;
            bool isLetterVowel = false;
            bool isLetterConsonant = false;

            letterIndex = Vowels.IndexOf(newLetter);
            if (letterIndex != -1)
            {
                isLetterVowel = true;
            }
            letterIndex = Consonants.IndexOf(newLetter);
            if (letterIndex != -1)
            {
                isLetterConsonant = true;
            }

            if (isLetterVowel)
            {
                int vowelCounter = 0;
                foreach (char letter in previousLetters)
                {
                    letterIndex = Vowels.IndexOf(letter);
                    if (letterIndex != -1)
                    {
                        vowelCounter++;
                    }
                }
                if (vowelCounter >= 2)
                {
                    return false;
                }
            }
            else if (isLetterConsonant)
            {
                int consonantCounter = 0;
                foreach (char letter in previousLetters)
                {
                    letterIndex = Consonants.IndexOf(letter);
                    if (letterIndex != -1)
                    {
                        consonantCounter++;
                    }
                }
                if (consonantCounter >= 2)
                {
                    return false;
                }
            }
            else
            {
                foreach (char letter in previousLetters)
                {
                    letterIndex = previousLetters.IndexOf(newLetter);
                    if (letterIndex != -1)
                    {
                        return false;
                    }
                }
            }
            return isAppropriate;
        }
        private static int GenerateRandomNumber(int minLimit, int maxLimit)
        {
            if (PsuedoRandomMode)
            {
                int generatedRandomNumber = RandomNumber;
                generatedRandomNumber *= 13;
                generatedRandomNumber += 11;
                generatedRandomNumber %= maxLimit;
                generatedRandomNumber += minLimit;
                RandomNumber++;
                return generatedRandomNumber;
            }
            else
            {
                System.Threading.Thread.Sleep(10);
                int generatedRandomNumber = -1;
                Random random = new Random();
                generatedRandomNumber = random.Next(minLimit, maxLimit);
                return generatedRandomNumber;
            }
        }
    }
}
