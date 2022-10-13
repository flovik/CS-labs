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
using System.Text;
using SymmetricCiphers.StreamCipher;
using static SymmetricCiphers.BlockCipher.Helpers.Utils;

Console.OutputEncoding = Encoding.Unicode;
var text = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab2\SymmetricCiphers\message.txt");
var key = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab2\SymmetricCiphers\key.txt");

Console.WriteLine($"Message to be encrypted: {text}");
var blockCipher = new BlockCipher(key);
var blocks = blockCipher.Encrypt(text);
foreach (var block in blocks)
{
    Console.WriteLine(block + ' ');
}

Console.WriteLine();

blocks = blockCipher.Decrypt(blocks);
for (int i = 0; i < blocks.Count; i++)
{
    Console.WriteLine(blocks[i]);
}

//used that site to check myself, ECB, PKCS7, secrtkey
//https://the-x.cn/en-US/cryptography/Des.aspx

Console.WriteLine($"Message to be encrypted: {text}");
var streamCipher = new StreamCipher();
