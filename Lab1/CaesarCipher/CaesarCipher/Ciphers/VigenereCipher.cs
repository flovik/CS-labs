using CaesarCipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaesarCipher.Utils;

namespace CaesarCipher.Ciphers;

public class VigenereCipher : ICypher
{
    private readonly StringBuilder _alphabet = new("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    private readonly Dictionary<char, int> _letterIndexes = new();
    private char[,] _vigenereTable = new char[26, 26];
    private readonly string _keyword;
    private string _keywordMessage = string.Empty;
    public VigenereCipher(string keyword)
    {
        _keyword = keyword;
        InitDictionary(); //in dictionary hold the alphabet letters and corresponding indexes
        InitTable(); //create Vigenere table
    }

    private void InitTable()
    {
        //reason is to insert alphabet in each row, then rotate alphabet with one character on each row and insert again
        for (int i = 0; i < 26; i++)
        {
            //insert an alphabet
            for (int j = 0; j < 26; j++)
            {
                _vigenereTable[i, j] = _alphabet[j];
            }

            //put at the back of the alphabet first letter
            _alphabet.Append(_alphabet[0]);
            //remove the first letter
            _alphabet.Remove(0, 1);
        }
    }

    private void InitDictionary()
    {
        int i = 1;
        foreach (var letter in _alphabet.ToString())
        {
            _letterIndexes.Add(letter, i++);
        }
    }

    public void PrintMatrix()
    {
        for (int i = 0; i < _vigenereTable.GetLength(0); i++)
        {
            for (int j = 0; j < _vigenereTable.GetLength(1); j++)
            {
                Console.Write($"{_vigenereTable[i, j]} ");
            }

            Console.WriteLine();
        }
    }

    public string encryptMessage(char[] message)
    {
        //remove junk
        var input = TextManipulation.RemoveSpecialCharacters((new string(message)).ToUpper());
        MatchKeyword(input);
        var result = GenerateEncryptedMessage(input);
        return result;
    }

    public string decryptMessage(char[] encryptedMessage)
    {
        return GenerateDecryptedMessage(new string(encryptedMessage));
    }

    private void MatchKeyword(string input)
    {
        //keyword index is to keep track at the current char of keyword
        int keywordIndex = 0;
        var tempMessage = new StringBuilder();

        //iterate every letter in input and add the char from keyword in the word
        //that word will be used to encrypt the message
        foreach (var _ in input)
        {
            //if reached the back of word, go to beginning
            if (keywordIndex == _keyword.Length) keywordIndex = 0;
            tempMessage.Append(_keyword[keywordIndex++]);
        }

        _keywordMessage = tempMessage.ToString();
    }

    private string GenerateEncryptedMessage(string input)
    {
        var word = new StringBuilder(input);
        //match letters of Keyword with plaintext to generate encrypted message
        //need to find intersection between big keyword message ("lemonlemonlemonle...") with original message
        //
        for (int i = 0; i < word.Length; i++)
        {
            var keywordLetter = _keywordMessage[i];
            var messageLetter = word[i];
            int indexRow = _letterIndexes[keywordLetter] - 1; //get index of keyword letter from dictionary
            int indexColumn = _letterIndexes[messageLetter] - 1; //get index of plaintext letter from dictionary
            word[i] = _vigenereTable[indexRow, indexColumn]; //intersection char of two letters
        }

        return word.ToString();
    }

    private string GenerateDecryptedMessage(string input)
    {
        var word = new StringBuilder(input);
        //match letters of Keyword with plaintext to generate encrypted message
        //here we go a little backwards, we need the keyword letter to find the encrypted letter
        //from that encrypted letter we find the column of the original letter that belongs to the first row

        //having encrypted message, we have the encrypted letter
        for (int i = 0; i < word.Length; i++)
        {
            var keywordLetter = _keywordMessage[i];
            var encryptedMessageLetter = word[i];

            int indexRow = _letterIndexes[keywordLetter] - 1;
            int indexColumn = 0;

            //to find column of original letter need to find the encrypted letter from the message
            for (int j = 0; j < _vigenereTable.GetLength(0); j++)
            {
                if (_vigenereTable[indexRow, j] != encryptedMessageLetter) continue;
                indexColumn = j;
                break;
            }

            word[i] = _vigenereTable[0, indexColumn];
        }

        return word.ToString();
    }
}