using CaesarCipher.Ciphers;

var text = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab1\CaesarCipher\CaesarCipher\text.txt")
    .ToCharArray();

//var caesarCipher = new CaesarCipherr(3);
//string encryptMessage = caesarCipher.encryptMessage(text);
//Console.WriteLine(encryptMessage);
//string decryptedMessage = caesarCipher.decryptMessage(text);
//Console.WriteLine(decryptedMessage);


//var caesarCipherWithPermutation = new CaesarCipherWithPermutation(10, "STRING");
//string encryptMessage = caesarCipherWithPermutation.encryptMessage(text);
//Console.WriteLine(encryptMessage); 
//string decryptedMessage = caesarCipherWithPermutation.decryptMessage(text);
//Console.WriteLine(decryptedMessage);

//var newText = "balloon";
//var playfair = new PlayfairCipher(newText);
//playfair.PrintMatrix();

//var encoded = playfair.encryptMessage(text);
//Console.WriteLine(encoded);
//var decoded = playfair.decryptMessage(encoded.ToCharArray());
//Console.WriteLine(decoded);

var keyword = "LEMON";
var vigenere = new VigenereChiper(keyword);
var encoded = vigenere.encryptMessage(text);

vigenere.PrintMatrix();