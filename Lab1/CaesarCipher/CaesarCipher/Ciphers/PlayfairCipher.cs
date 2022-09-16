using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CaesarCipher.Extensions;
using CaesarCipher.Interfaces;
using CaesarCipher.Utils;

namespace CaesarCipher.Ciphers
{
    public class PlayfairCipher : ICypher
    {
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private char[,] letters = new char[5, 5];
        private Dictionary<char, (int, int)> usedLetters = new(); //to keep track what letters were used in initialization and their coordinates
        private int i = 0, j = 0;
        
        public PlayfairCipher(string message)
        {
            AddLetters(message.ToUpper());
            AddLetters(alphabet);
        }

        private void AddLetters(string message)
        {
            foreach (var letter in message)
            {
                //if not a letter skip it
                if (!char.IsLetter(letter)) continue;
                //if already in set of used letters skip it
                if (usedLetters.ContainsKey(letter)) continue;
                
                switch (letter)
                {
                    //check in case for j or i
                    case 'I' when usedLetters.ContainsKey('J'):
                    case 'J' when usedLetters.ContainsKey('I'):
                        continue;
                }

                usedLetters.Add(letter, (i, j));
                letters[i, j++] = letter;
                //if outOfRange of array, go one row down
                if (j != 5) continue;
                i++;
                j = 0;
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < letters.GetLength(0); i++)
            {
                for (int j = 0; j < letters.GetLength(1); j++)
                {
                    Console.Write($"{letters[i, j]}\t");
                }

                Console.WriteLine();
            }
        }

        public string encryptMessage(char[] message)
        {
            var splitAndConcat = TextManipulation.RemoveSpecialCharacters(new string(message));
            var formattedText = new StringBuilder(splitAndConcat.ToUpper());

            int i = 1;
            while (i < formattedText.Length)
            {
                if (!formattedText[i].Equals(formattedText[i - 1]))
                {
                    i += 2;
                    continue;
                }

                //if two chars are equal, separate them with an X
                formattedText.Insert(i, 'X');

                i += 2;
            }

            //in case we don't have full pairs
            if (formattedText.Length % 2 == 1) formattedText.Append('X');

            //split string into chunks of two characters
            var pairs = formattedText.ToString().TakeEvery(2);
            var encoded = ReplaceLetters(pairs, 0);

            //X is used as a substitution to fill the next 
            //message = ReplaceText(message);
            //return new string(message);
            return string.Join("", encoded);
        }

        public string decryptMessage(char[] message)
        {
            //split string into chunks of two characters
            var pairs = new string(message).TakeEvery(2);
            //we will need to execute 4 more iterations on SameRow and SameColumn
            var encoded = ReplaceLetters(pairs, 3);

            return string.Join("", encoded);
        }

        private List<string> ReplaceLetters(IEnumerable<string> pairs, int iterations)
        {
            var result = new List<string>();
            foreach (var pair in pairs)
            {
                //take coordinates of each letter
                (int i, int j) coordinatesA = usedLetters[pair[0]];
                (int i, int j) coordinatesB = usedLetters[pair[1]];

                if (coordinatesA.i == coordinatesB.i)
                {
                    //change coordinates in case we have more iterations to decrypt
                    coordinatesA = new(coordinatesA.i, (coordinatesA.j + iterations) % 5);
                    coordinatesB = new (coordinatesB.i, (coordinatesB.j + iterations) % 5);
                    //coordinatesA.j += iterations % 5;
                    //coordinatesB.j += iterations % 5;
                    result.Add(SameRow(coordinatesA, coordinatesB));
                }
                else if (coordinatesA.j == coordinatesB.j)
                {

                    coordinatesA = new ((coordinatesA.i + iterations) % 5, coordinatesA.j);
                    coordinatesB = new((coordinatesB.i + iterations) % 5, coordinatesB.j);
                    //coordinatesA.i += iterations % 5;
                    //coordinatesB.i += iterations % 5;
                    result.Add(SameColumn(coordinatesA, coordinatesB));
                }
                else result.Add(Rectangle(coordinatesA, coordinatesB));
            }

            return result;
        }

        private string SameRow((int i, int j) coordinatesA, (int i, int j) coordinatesB)
        {
            //takes the letter from the right of the current letter
            //if to the rightmost part, take the leftmost letter
            //mod to get the leftmost, i + 1 mod 5, concatenate two chars from the table

            char[] pair =
            {
                letters[coordinatesA.i % 5, (coordinatesA.j + 1) % 5],
                letters[coordinatesB.i % 5, (coordinatesB.j + 1) % 5]
            };

            return new string(pair);
        }

        private string SameColumn((int i, int j) coordinatesA, (int i, int j) coordinatesB)
        {
            //takes the letter from the bottom of the current letter
            //if at the bottom, take the top one
            //mod to get the top one if at the bottom, j + 1 mod 5, concatenate two chars from the table
            char[] pair =
            {
                letters[(coordinatesA.i + 1) % 5, coordinatesA.j],
                letters[(coordinatesB.i + 1) % 5, coordinatesB.j]
            };

            return new string(pair);
        }

        private string Rectangle((int i, int j) coordinatesA, (int i, int j) coordinatesB)
        {
            //for current letter pick from the same row on the opposite corner

            char[] pair =
            {
                letters[coordinatesA.i, coordinatesB.j],
                letters[coordinatesB.i, coordinatesA.j]
            };

            return new string(pair);
        }
    }
}
