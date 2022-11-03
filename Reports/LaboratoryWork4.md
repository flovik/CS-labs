# Hash functions and Digital Signatures.

### Course: Cryptography & Security
### Author: Florescu Victor

----
&ensp;&ensp;&ensp; Hashing is a technique used to compute a new representation of an existing value, message or any piece of text. The new representation is also commonly called a digest of the initial text, and it is a one way function meaning that it should be impossible to retrieve the initial content from the digest.

&ensp;&ensp;&ensp; Such a technique has the following usages:
  * Offering confidentiality when storing passwords,
  * Checking for integrity for some downloaded files or content,
  * Creation of digital signatures, which provides integrity and non-repudiation.

&ensp;&ensp;&ensp; In order to create digital signatures, the initial message or text needs to be hashed to get the digest. After that, the digest is to be encrypted using a public key encryption cipher. Having this, the obtained digital signature can be decrypted with the public key and the hash can be compared with an additional hash computed from the received message to check the integrity of it.

### SHA256

&ensp;&ensp;&ensp; SHA 256 is a part of the SHA 2 family of algorithms, where SHA stands for Secure Hash Algorithm. Published in 2001,
it was a joint effort between the NSA and NIST to introduce a successor to the SHA 1 family,
which was slowly losing strength against brute force attacks.

&ensp;&ensp;&ensp; The significance of the 256 in the name stands for the final hash digest value, i.e. irrespective of the size of plaintext/cleartext, the hash value will always be 256 bits.

## Objectives:
1. Get familiar with the hashing techniques/algorithms.
2. Use an appropriate hashing algorithms to store passwords in a local DB.
    1. You can use already implemented algortihms from libraries provided for your language.
    2. The DB choise is up to you, but it can be something simple, like an in memory one.
3. Use an asymmetric cipher to implement a digital signature process for a user message.
    1. Take the user input message.
    2. Preprocess the message, if needed.
    3. Get a digest of it via hashing.
    4. Encrypt it with the chosen cipher.
    5. Perform a digital signature check by comparing the hash of the message with the decrypted one.

## Implementation description

&ensp;&ensp;&ensp; To complete the laboratory work, I used [SHA256 Class](#Sha256) defined in .NET 6 to hash user input. Then the password is encrypted with RSA cipher 
implemented in LAB3 and the information [is added in the database](#AddUser). Then method [Verify signature](#VerifySignature) is invoked, 
it tells to type in again a password, it is hashed afterwards. Then the password of current user is fetched from database, decrypted with RSA cipher and 
compared with the password that was typed in. 

# Sha256
```
using var sha256 = SHA256.Create();
var secretBytes = Encoding.UTF8.GetBytes(password);
var secretHash = sha256.ComputeHash(secretBytes);
return Convert.ToHexString(secretHash);
```

# AddUser
```
var password = Console.ReadLine();
password = _sha256Encryptor.HashPassword(password);

var hexEncryptedPassword = _rsaCipher.Encrypt(password);
_userId = Guid.NewGuid();

var user = new User
{
    Login = login,
    Password = hexEncryptedPassword,
    UserId = _userId
};

_userRepository.CreateUser(user);
```

# VerifySignature
```
var password = Console.ReadLine();
password = _sha256Encryptor.HashPassword(password);

var user = _userRepository.GetUser(_userId);
var decryptedPassword = _rsaCipher.Decrypt(user.Password);

return string.Equals(password, decryptedPassword, StringComparison.OrdinalIgnoreCase);
```

## Screenshots

All process is done via console. 

Results of hashing of verifying of the digital signature:

![image](https://user-images.githubusercontent.com/57410984/199773600-c2a7716a-e712-4bf0-9927-07bb129de098.png)
![image](https://user-images.githubusercontent.com/57410984/199774008-afc266c6-4147-4aed-9aa3-7f91734278d8.png)

![image](https://user-images.githubusercontent.com/57410984/199773826-48dff315-e935-4bee-a90a-61b0247a191e.png)

## Conclusions

In that laboratory work, I got familiar with the hashing algorithm SHA256 and used my RSA cipher to encrypt and decrypt hash values :)
