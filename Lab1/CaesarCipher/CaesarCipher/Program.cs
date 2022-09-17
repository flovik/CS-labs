using CaesarCipher.Ciphers;

var text = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab1\CaesarCipher\CaesarCipher\text.txt")
    .ToCharArray();

Console.WriteLine($"Message to be encrypted: {new string(text)}");


//var caesarCipher = new CaesarCipherr(18);
//string encryptMessage = caesarCipher.encryptMessage(text);
//Console.WriteLine($"Encrypted message: {encryptMessage}");
//string decryptedMessage = caesarCipher.decryptMessage(encryptMessage.ToCharArray());
//Console.WriteLine($"Decrypted message: {decryptedMessage}");


//var caesarCipherWithPermutation = new CaesarCipherWithPermutation(18, "STRING");
//Console.Write("New alphabet: ");
//caesarCipherWithPermutation.PrintNewAlphabet();
//string encryptMessage = caesarCipherWithPermutation.encryptMessage(text);
//Console.WriteLine($"Encrypted message: {encryptMessage}");
//string decryptedMessage = caesarCipherWithPermutation.decryptMessage(encryptMessage.ToCharArray());
//Console.WriteLine($"Decrypted message: {decryptedMessage}");

//var newText = "balloon";
//var playfair = new PlayfairCipher(newText);
//playfair.PrintMatrix();

//var encoded = playfair.encryptMessage(text);
//Console.WriteLine($"Encrypted message: {encoded}");
//var decoded = playfair.decryptMessage(encoded.ToCharArray());
//Console.WriteLine($"Decrypted message: {decoded}");

var keyword = "LEMON";
var vigenere = new VigenereCipher(keyword);
vigenere.PrintMatrix();
var encoded = vigenere.encryptMessage(text);
Console.WriteLine($"Encrypted message: {encoded}");
var decoded = vigenere.decryptMessage(encoded.ToCharArray());
Console.WriteLine($"Decrypted message: {decoded}");