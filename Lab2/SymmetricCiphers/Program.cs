//implement a stream cipher

//implement a block cipher

//abstraction/contract/interface
//use directories to logically split files

//client class or test classes to showcase

//more explicit namings
//composition in loc de inheritance

//reports in the same folder

//ciphers encrypts in blocks of 64 bits (8 bytes - 8 characters at a time)
//implement different character length text by padding it - in utils class

using SymmetricCiphers.BlockCipher;

var text = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab2\SymmetricCiphers\message.txt");
var key = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab2\SymmetricCiphers\key.txt");

Console.WriteLine($"Message to be encrypted: {text}");
var blockCipher = new BlockCipher(key);
blockCipher.Encrypt(text);