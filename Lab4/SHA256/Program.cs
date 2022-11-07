// See https://aka.ms/new-console-template for more information

using RSA.RsaCipher;
using RSA.RsaCipher.Interfaces;
using SHA256.Context;
using SHA256.Models;
using SHA256Encryption.Interfaces;
using SHA256Encryption.Repositories;
using SHA256Encryption.Services;

using var context = new ShaContext();

IUserRepository repository = new UserRepository(context);
IRsaCipher cipher = new RsaCipher();
ISha256Encryptor encryptor = new Sha256Encryptor();

var userService = new UserService(repository, cipher, encryptor);
userService.AddUser();
Console.WriteLine(userService.SignatureCheck());