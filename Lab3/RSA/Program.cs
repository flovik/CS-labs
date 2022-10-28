using System.Text;
using RSA.RsaCipher;
using RSA.RsaCipher.Interfaces;

Console.OutputEncoding = Encoding.Unicode;
string text = File.ReadAllText(@"C:\Personal testing\CS\CS-labs\Lab3\RSA\message.txt");
Console.WriteLine($"Text to be encrypted: {text}");
IRsaCipher rsa = new RsaCipher();
var encrypted = rsa.Encrypt(text);

Console.WriteLine("Encrypted text (in hex): ");
foreach (var block in encrypted)
{
    Console.Write(block + " ");
}

Console.WriteLine();
Console.WriteLine("Decrypted text:");
Console.WriteLine(rsa.Decrypt(encrypted));