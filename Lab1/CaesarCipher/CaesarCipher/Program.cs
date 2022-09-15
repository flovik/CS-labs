using CaesarCipher.Ciphers;

var text = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab1\CaesarCipher\CaesarCipher\text.txt")
    .ToCharArray();

//var caesarCipher = new CaesarCipherr(3);
//string encryptMessage = caesarCipher.encryptMessage(text);
//Console.WriteLine(encryptMessage);
//string decryptedMessage = caesarCipher.decryptMessage(text);
//Console.WriteLine(decryptedMessage);


var caesarCipherWithPermutation = new CaesarCipherWithPermutation(10, "STRING");
string encryptMessage = caesarCipherWithPermutation.encryptMessage(text);
Console.WriteLine(encryptMessage); 
string decryptedMessage = caesarCipherWithPermutation.decryptMessage(text);
Console.WriteLine(decryptedMessage);

//var playfair = new PlayfairCipher("", "");
//var vigenere = new VigenereChiper("", "");