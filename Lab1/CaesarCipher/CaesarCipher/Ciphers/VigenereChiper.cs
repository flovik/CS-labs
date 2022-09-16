using CaesarCipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaesarCipher.Utils;

namespace CaesarCipher.Ciphers
{
    public class VigenereChiper : ICypher
    {
        private StringBuilder alphabet = new StringBuilder("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        private Dictionary<char, int> letterIndexes = new Dictionary<char, int>();
        private char[,] vigenereTable = new char[26, 26];
        private string Keyword;
        private string KeywordMessage = string.Empty;
        public VigenereChiper(string keyword)
        {
            Keyword = keyword;
            InitDictionary();
            InitTable();
        }

        private void InitTable()
        {
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    vigenereTable[i, j] = alphabet[j];
                }

                alphabet.Append(alphabet[0]);
                alphabet.Remove(0, 1);
            }
            
        }

        private void InitDictionary()
        {
            int i = 1;
            foreach (var letter in alphabet.ToString())
            {
                letterIndexes.Add(letter, i++);
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < vigenereTable.GetLength(0); i++)
            {
                for (int j = 0; j < vigenereTable.GetLength(1); j++)
                {
                    Console.Write($"{vigenereTable[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        public string encryptMessage(char[] message)
        {
            var input = TextManipulation.RemoveSpecialCharacters((new string(message)).ToUpper());
            matchKeyword(input);
            var result = GenerateEncryptedMessage(input);
            return result;
        }

        public string decryptMessage(char[] encryptedMessage)
        {
            return GenerateDecryptedMessage(new string(encryptedMessage));
        }

        private void matchKeyword(string input)
        {
            Console.WriteLine(input);
            int keywordIndex = 0;
            var tempMessage = new StringBuilder();

            foreach (var _ in input)
            {
                if (keywordIndex == Keyword.Length) keywordIndex = 0;
                tempMessage.Append(Keyword[keywordIndex++]);
            }

            KeywordMessage = tempMessage.ToString();
        }

        private string GenerateEncryptedMessage(string input)
        {
            var word = new StringBuilder(input);
            //match letters of Keyword with plaintext to generate encrypted message
            for (int i = 0; i < word.Length; i++)
            {
                var keywordLetter = KeywordMessage[i];
                var messageLetter = word[i];
                int indexRow = letterIndexes[keywordLetter] - 1;
                int indexColumn = letterIndexes[messageLetter] - 1;
                word[i] = vigenereTable[indexRow, indexColumn];
            }

            return word.ToString();
        }

        private string GenerateDecryptedMessage(string input)
        {
            var word = new StringBuilder(input);
            //match letters of Keyword with plaintext to generate encrypted message
            for (int i = 0; i < word.Length; i++)
            {
                var keywordLetter = KeywordMessage[i];
                var encryptedMessageLetter = word[i];

                int indexRow = letterIndexes[keywordLetter] - 1;
                int indexColumn = 0;
                for (int j = 0; j < vigenereTable.GetLength(0); j++)
                {
                    if (vigenereTable[indexRow, j] != encryptedMessageLetter) continue;
                    indexColumn = j;
                    break;
                }

                word[i] = vigenereTable[0, indexColumn];
            }

            return word.ToString();
        }
    }
}
