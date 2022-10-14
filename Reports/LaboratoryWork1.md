# Intro to Cryptography. Classical ciphers. Caesar cipher.

### Course: Cryptography & Security
### Author: Florescu Victor

----
&ensp;&ensp;&ensp; Ciphers are arguably the corner stone of cryptography. In general, a cipher is simply just a set of steps (an algorithm) 
for performing both an encryption, and the corresponding decryption.

&ensp;&ensp;&ensp; Despite might what seem to be a relatively simple concept, ciphers play a crucial role in modern technology. 
Technologies involving communication rely on ciphers in order to maintain both security and privacy.

### Caesar cipher

&ensp;&ensp;&ensp; In cryptography, a **Caesar cipher** is one of the simplest and most widely known encryption techniques. 
It is a type of substitution cipher in which each letter in the plaintext is replaced by a letter some fixed number of positions down the alphabet. 
For example, with a left shift of 3, *D* would be replaced by *A*, *E* would become *B*, and so on. 
The method is named after Julius Caesar, who used it in his private correspondence.

### Playfair cipher

&ensp;&ensp;&ensp; The Playfair cipher or Playfair square or Wheatstone–Playfair cipher
is a manual symmetric encryption technique and was the first literal digram substitution cipher.
The Playfair cipher uses a 5 by 5 table containing a key word or phrase.

&ensp;&ensp;&ensp; To generate the key table, one would first fill in the spaces in the table with the letters of the keyword (dropping any duplicate letters), 
then fill the remaining spaces with the rest of the letters of the alphabet.

&ensp;&ensp;&ensp; To encrypt a message, one would break the message into digrams (groups of 2 letters) such that, for example, "HelloWorld" becomes "HE LL OW OR LD". These digrams will be substituted using the key table. 
Since encryption requires pairs of letters, messages with an odd number of characters usually append an uncommon letter, such as "X", to complete the final digram. 
The two letters of the digram are considered opposite corners of a rectangle in the key table. 
To perform the substitution, apply the following 4 rules, in order, to each pair of letters in the plaintext:

1. If the letters appear on the same row of your table, replace them with the letters to their immediate right respectively (wrapping around to the left side of the row if a letter in the original pair was on the right side of the row).
2. If the letters appear on the same column of your table, replace them with the letters immediately below respectively (wrapping around to the top side of the column if a letter in the original pair was on the bottom side of the column).
3. If the letters are not on the same row or column, replace them with the letters on the same row respectively but at the other pair of corners of the rectangle defined by the original pair. The order is important – the first letter of the encrypted pair is the one that lies on the same row as the first letter of the plaintext pair.

&ensp;&ensp;&ensp; To decrypt, use the inverse of the two shift rules, selecting the letter to the left or upwards as appropriate. 
The last rule remains unchanged and a repeat of the transformation returns the selection to its original state. 

### Vigenere cipher

&ensp;&ensp;&ensp; The Vigenère cipher is a method of encrypting alphabetic text by using a series of interwoven Caesar ciphers, based on the letters of a keyword.
It employs a form of polyalphabetic substitution.
The Vigenère cipher has several Caesar ciphers in sequence with different shift values.

&ensp;&ensp;&ensp; To encrypt, a table of alphabets can be used called Vigenère table. It has the alphabet written out 26 times in different rows, 
each alphabet shifted cyclically to the left compared to the previous alphabet, corresponding to the 26 possible Caesar ciphers. 
At different points in the encryption process, the cipher uses a different alphabet from one of the rows. 
The alphabet used at each point depends on a repeating keyword.

For example, suppose that the plaintext to be encrypted is

> attackatdawn.

The person sending the message chooses a keyword and repeats it until it matches the length of the plaintext, for example, the keyword "LEMON":

> LEMONLEMONLE

&ensp;&ensp;&ensp; Each row starts with a key letter. The rest of the row holds the letters A to Z (in shifted order). 
Although there are 26 key rows shown, a code will use only as many keys (different alphabets) as there are unique letters in the key string,
here just 5 keys: {L, E, M, O, N}. For successive letters of the message, successive letters of the key string will be taken and each message letter
enciphered by using its corresponding key row. The next letter of the key is chosen, and that row is gone along to find the column heading that matches 
the message character. The letter at the intersection of [keywordRow, messageColumn] is the enciphered letter.

&ensp;&ensp;&ensp; Decryption is performed by going to the row in the table corresponding to the key, finding the position of the ciphertext letter in that row and then
using the column's label as the plaintext.

## Objectives:
1. Get familiar with the basics of cryptography and classical ciphers.

2. Implement 4 types of the classical ciphers:
    - Caesar cipher with one key used for substitution (as explained above),
    - Caesar cipher with one key used for substitution, and a permutation of the alphabet,
    - Vigenere cipher,
    - Playfair cipher.

3. Structure the project in methods/classes/packages as neeeded.

## Implementation description

* Caesar cipher

&ensp;&ensp;&ensp; I have a class called CaesarCipherr which has two methods - of encryption a message and decryption of it. Both methods use the same method from the
base class *ReplaceText(char[] message)*, which iterates the whole message to subsequently encrypt or decrypt it. Obviously, we need to check the text for whitespaces, dots, commas 
and for that I check with the method *IsInAlphabet(char character)* if the current character is indeed a character. If the current character is a letter, I proceed to replace it
using *ReplaceChar(char character)* method. I search for the current character in the predefined alphabet and then apply the substitution formula to change it, if
char value exceed the length of the alphabet, mod 26 is applied to start right from the beginning of the alphabet as it should. Works both with upperCase and lowerCase
letters. 

&ensp;&ensp;&ensp; Decryption uses the same approach, but the substituion key is changed to *subsitutionKey = 26 - substitutionKey*, in that manner the encrypted letter will be
replaced with the original one. 

```
public class CaesarCipherr : AbstractCipher, ICypher
{
    public CaesarCipherr() {}
    public CaesarCipherr(int substitutionKey)
    {
        SubstitutionKey = substitutionKey;
    }

    public string encryptMessage(char[] message)
    {
        message = ReplaceText(message);
        return new string(message);
    }

    public string decryptMessage(char[] message)
    {
        SubstitutionKey = 26 - SubstitutionKey; //change substitution key to make a full circle
        message = ReplaceText(message);
        SubstitutionKey = 26 - SubstitutionKey; //restore original value of key
        return new string(message);
    }
}
```

```
public abstract class AbstractCipher
{
    private protected string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private protected int SubstitutionKey;

    protected virtual bool IsInAlphabet(char character)
    {
        return character is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
    }

    protected virtual char ReplaceChar(char character)
    {
        char replacedChar;
        if (char.IsLower(character))
        {
            var index = alphabet.IndexOf(char.ToUpper(character)); //take index of character in the alphabet
            replacedChar = char.ToLower(alphabet[(index + SubstitutionKey) % alphabet.Length]); //apply substitution rule
        }
        else replacedChar = alphabet[(alphabet.IndexOf(character)+ SubstitutionKey) % alphabet.Length];

        return replacedChar;
    }

    protected char[] ReplaceText(char[] message)
    {
        for (var i = 0; i < message.Length; i++)
        {
            //skip whitespaces, commas, dots, etc.
            if (!IsInAlphabet(message[i])) continue;

            //replace char with a new encrypted one
            message[i] = ReplaceChar(message[i]);
        }

        return message;
    }
}
```

* Caesar cipher with alphabet permutation

&ensp;&ensp;&ensp; That cipher is basically the same as simple cipher, but we have one extra step: insert a word in the without any repeating characters,
then insert the rest of the alphabet, don't include any characters if they are already in the permutatedAlphabet. 

```
public class CaesarCipherWithPermutation : CaesarCipherr
{
    public CaesarCipherWithPermutation(int substitutionKey, string permutationKey)
    {
        SubstitutionKey = substitutionKey;
        alphabet = ChangeAlphabet(permutationKey);
    }

    public void PrintNewAlphabet()
    {
        Console.WriteLine(alphabet);
    }

    //generate a new alphabet from adding permutationKey and then adding the rest letters of the alphabet
    //apart from added letters
    private string ChangeAlphabet(string permutationKey)
    {
        var secondAlphabet = new StringBuilder(permutationKey);
        foreach (var letter in alphabet)
        {
            if (!permutationKey.Contains(letter)) secondAlphabet.Append(letter);
        }

        return secondAlphabet.ToString();
    }
}
```

* Playfair cipher

&ensp;&ensp;&ensp; For playfair cipher I created a Class called PlayfairCipher which has a predefined alphabet, 5x5 matrix (will be used for encryption and decryption),
a dictionary that stores the coorinates of letters in the matrix. First of all we need to initialize the matrix *_letters* by adding 
the keyword and then the rest of the alphabet. For that is used a function *AddLetters(string message)*. Iterate the whole word and add the letters 
in the matrix by applying some rules:
 1. If current character is not a letter, skip it.
 2. Check if letter is in the dictionary of letters that have been added to the matrix, if in dictionary - skip it
 3. Check if letter is either 'I' or 'J', because in that cipher they are the same

&ensp;&ensp;&ensp; When matrix is built, program is ready to encrypt and decrypt messages. When *encryptMessage(char[] message)* is invoked, plaintext message should be cleared up from
whitespaces, dots, commas and should be in uppercase in order to encrypt the text. To remove whitespaces and dots I used 
*TextManipulation.RemoveSpecialCharacters(new string(message))* function which uses a Regex. 

&ensp;&ensp;&ensp; Now I want to separate neighbouring characters in case they are same. For that I go 2 by 2 characters, if they are the same I insert
an 'X' to separate them and have different neighbouring characters. If length of message is odd, I insert another X at the end of the message.

&ensp;&ensp;&ensp; Now I need to separate the message into pairs of two character per pair. To make that I extended string class with the method *TakeEvery(int count)*. 
When that job is done, time for replacing characters. I call the method *ReplaceLetters(IEnumerable< string > pairs, int iterations)*. At the decryption I will explain
the reasosing behind iterations. 

&ensp;&ensp;&ensp; Now in the ReplaceLetters(), I iterate every pair, take the coordinates of each letter in the pair and check whether they are on the sameRow, sameColumn or neither. 
*sameRow((int i, int j) coordinatesA, (int i, int j) coordinatesB)* moves the letter to the right of the current character in the matrix, if at the rightmost part, take
the leftmost character. *sameColumn((int i, int j) coordinatesA, (int i, int j) coordinatesB)* changes the letter with the one below the current one. If at the bottom,
take the top one from the column. And finally, *rectangle((int i, int j) coordinatesA, (int i, int j) coordinatesB)*, Two letters form a rectangle in the matrix, 
the function switches the character on the same row, but from the corner of the other character in pair. Same for the second character. 

&ensp;&ensp;&ensp; To decrypt a message, the same ReplaceLetters() is applied, but with iterations = 3, which means that the same rules are applied with the encryption, but coordinates 
are changed in order to target the original characters. Imagine the row A B C D E, in encryption BC is encrypted into CD, in decryption from CD I change coordinates to
AB to move forward to BC which was the original text pair. 

```
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
```

```
public static class TextManipulation
    {
        public static string RemoveSpecialCharacters(string input)
        {
            //that one removes whitespaces and special characters
            var r = new Regex("(?:[^a-z0-9]|(?<=['\"])s)",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
    }
```

```
public static class StringExtensions
    {
        public static IEnumerable<string> TakeEvery(this string s, int count)
        {
            int index = 0;
            while (index < s.Length)
            {
                if (s.Length - index >= count)
                {
                    yield return s.Substring(index, count);
                }
                else
                {
                    yield return s.Substring(index, s.Length - index);
                }

                index += count;
            }
        }
    }
```
* Vigenere cipher

&ensp;&ensp;&ensp; For Vigenere cipher we need to create a 26x26 matrix that contains the alphabet on each row, but for every row - alphabet is rotated by one character.
To encrypt a message, remove the whitespaces with *TextManipulation.RemoveSpecialCharacters(string input);*. Then make a message from the input keyword (e.g. "LEMON") 
to match the length of the plaintext message that needs to be encrypted. When words match, *GenerateEnctyptedMessage(string input)* is invoked. Here iterate the message and 
the keyword at the same time character by character, find their indexes in the dictionary and find the intersection character of the keyword character and the message character.
That intersection character is the encrypted character.

&ensp;&ensp;&ensp; To decrypt a message, call *GenerateDecryptedMessage(string input)*, where keyword and encrypted message are iterated as in the *GenerateEnctyptedMessage(string input)*,
we get the indexes of keyword character and encrypted character (by iterating the row of keyword character). Index of the encrypted character is the column where original
character lays.

```
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
```
## Screenshots

Plaintext message is saved in the .txt file and used throughout the whole program. 

Results of encryption:
- Caesar cipher with substitution key 18:

![image](https://user-images.githubusercontent.com/57410984/190854960-b7b2c1bf-9816-4dc3-8a95-a08d9f1dfb02.png)
- Caesar cipher with substitution key 18 and new alphabet beginning with "STRING":

![image](https://user-images.githubusercontent.com/57410984/190855247-9ee8959e-da17-4e9d-87e9-c6fd679d7c1a.png)

- Playfair cipher, keyword used "balloon"

![image](https://user-images.githubusercontent.com/57410984/190855344-0e080927-7d39-4362-9094-7523bfc64a0a.png)

- Vigenere cipher, keyword used "LEMON"

![image](https://user-images.githubusercontent.com/57410984/190855411-18132614-bb0b-4b1c-a557-5fa4dfccceb4.png)


## Conclusions

In that laboratory work, I got familiar with basic cryptography and implemented four classical ciphers.
