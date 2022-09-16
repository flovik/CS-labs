using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CaesarCipher.Extensions;
using CaesarCipher.Interfaces;
using CaesarCipher.Utils;

namespace CaesarCipher.Ciphers;

public class PlayfairCipher : ICypher
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private readonly char[,] _letters = new char[5, 5];
    private readonly Dictionary<char, (int, int)> _usedLetters = new(); //to keep track what letters were used in initialization and their coordinates
    private int i, j;
        
    public PlayfairCipher(string message)
    {
        AddLetters(message.ToUpper()); //add in matrix letters of the keyword
        AddLetters(Alphabet); //then add the rest of the alphabet
    }

    private void AddLetters(string message)
    {
        foreach (var letter in message)
        {
            //if not a letter skip it
            if (!char.IsLetter(letter)) continue;
            //if already in set of used letters skip it
            if (_usedLetters.ContainsKey(letter)) continue;
                
            switch (letter)
            {
                //check in case for j or i
                case 'I' when _usedLetters.ContainsKey('J'):
                case 'J' when _usedLetters.ContainsKey('I'):
                    continue;
            }

            _usedLetters.Add(letter, (i, j)); //letter and its coordinates in the matrix
            _letters[i, j++] = letter; //add in the matrix
            //if outOfRange of array, go one row down
            if (j != 5) continue;
            i++;
            j = 0;
        }
    }

    public void PrintMatrix()
    {
        for (int i = 0; i < _letters.GetLength(0); i++)
        {
            for (int j = 0; j < _letters.GetLength(1); j++)
            {
                Console.Write($"{_letters[i, j]}\t");
            }

            Console.WriteLine();
        }
    }

    public string encryptMessage(char[] message)
    {
        //remove all junk like whitespace, commas and dots to have only letters
        var splitAndConcat = TextManipulation.RemoveSpecialCharacters(new string(message));
        var formattedText = new StringBuilder(splitAndConcat.ToUpper());

        //while loop to separate characters if are equal
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
        //we will need to execute 3 more iterations on SameRow and SameColumn
        var encoded = ReplaceLetters(pairs, 3);

        return string.Join("", encoded);
    }

    //method returns a list of pairs that have been modified by the rules applied in Playfair cipher
    private List<string> ReplaceLetters(IEnumerable<string> pairs, int iterations)
    {
        var result = new List<string>();
        foreach (var pair in pairs)
        {
            //take coordinates of each letter
            (int i, int j) coordinatesA = _usedLetters[pair[0]];
            (int i, int j) coordinatesB = _usedLetters[pair[1]];

            if (coordinatesA.i == coordinatesB.i)
            {
                //change coordinates in case we have more iterations to decrypt, in case goes outOfRange I apply % 5 to go back at the beginning of the array
                coordinatesA = new(coordinatesA.i, (coordinatesA.j + iterations) % 5);
                coordinatesB = new (coordinatesB.i, (coordinatesB.j + iterations) % 5);

                result.Add(SameRow(coordinatesA, coordinatesB));
            }
            else if (coordinatesA.j == coordinatesB.j)
            {

                coordinatesA = new ((coordinatesA.i + iterations) % 5, coordinatesA.j);
                coordinatesB = new((coordinatesB.i + iterations) % 5, coordinatesB.j);

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
            _letters[coordinatesA.i % 5, (coordinatesA.j + 1) % 5],
            _letters[coordinatesB.i % 5, (coordinatesB.j + 1) % 5]
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
            _letters[(coordinatesA.i + 1) % 5, coordinatesA.j],
            _letters[(coordinatesB.i + 1) % 5, coordinatesB.j]
        };

        return new string(pair);
    }

    private string Rectangle((int i, int j) coordinatesA, (int i, int j) coordinatesB)
    {
        //for current letter pick from the same row on the opposite corner

        char[] pair =
        {
            _letters[coordinatesA.i, coordinatesB.j],
            _letters[coordinatesB.i, coordinatesA.j]
        };

        return new string(pair);
    }
}