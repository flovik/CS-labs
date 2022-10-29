# Asymmetric Ciphers. RSA Cipher.

### Course: Cryptography & Security
### Author: Florescu Victor

----
&ensp;&ensp;&ensp; Asymmetric Cryptography (a.k.a. Public-Key Cryptography)deals with the encryption of plain text when having 2 keys, one being public and the other one private. The keys form a pair and despite being different they are related.

&ensp;&ensp;&ensp; As the name implies, the public key is available to the public but the private one is available only to the authenticated recipients. 

&ensp;&ensp;&ensp; A popular use case of the asymmetric encryption is in SSL/TLS certificates along side symmetric encryption mechanisms. It is necessary to use both types of encryption because asymmetric ciphers are computationally expensive, so these are usually used for the communication initiation and key exchange, or sometimes called handshake. The messages after that are encrypted with symmetric ciphers.

### RSA Cipher

&ensp;&ensp;&ensp; RSA (Rivest–Shamir–Adleman) is a public-key cryptosystem that is widely used for secure data transmission. It is also one of the oldest. 
In a public-key cryptosystem, the encryption key is public and distinct from the decryption key, which is kept secret (private). 
An RSA user creates and publishes a public key based on two large prime numbers, along with an auxiliary value. 
The prime numbers are kept secret. Messages can be encrypted by anyone, via the public key, but can only be decoded by someone who knows the prime numbers.

&ensp;&ensp;&ensp; The RSA algorithm involves three steps: key generation, encryption, and decryption.

&ensp;&ensp;&ensp; A basic principle behind RSA is the observation that it is practical to find three very large positive integers e, d, and n, such that with modular exponentiation for all integers m (with 0 ≤ m < n):

$$(m^d)^e \equiv m \pmod{n}$$
![image](https://user-images.githubusercontent.com/57410984/198629553-8883be1c-d31c-4d2e-b5ae-c3f9c4c2f8eb.png)

and that knowing e and n, or even m, it can be extremely difficult to find d. The triple bar (≡) here denotes modular congruence (which is to say that when you divide (me)d by n and m by n, they both have the same remainder).


## Objectives:
1. Get familiar with the asymmetric cryptography mechanisms.

2. Implement an example of an asymmetric cipher.

3. As in the previous task, please use a client class or test classes to showcase the execution of your programs.

## Implementation description

* RSA 

&ensp;&ensp;&ensp; **Key generation**. In that step public and private key are generated. We need two prime numbers _P_ and _Q_ which should be kept secret. 
Compute _N = PQ_. Then, Least Common Multiple should be computed, that variable will be used in private key. 
L = [LCM](#LCM) (P - 1, Q - 1) . To get the value of LCM, I used formula $$|(p - 1)(q - 1)| \over gcd((p - 1), (q - 1))$$
[GCD](#Gcd) is Greatest Common Divisor, at each iteration, a or b will hold the remainder of mod operation, 
other one will hold its predecessor and vice versa until remainder is 0. 

&ensp;&ensp;&ensp; Then, _d_ is calculated by applying [modular multiplicative inverse](#ModInverse) of E modulo L. To calculate it efficiently,
I used [Extended Euclidean algorithm](#EGcd), imagine we have sa + tb = gcd(a, b), it can be simplified to sa + tb = 1, because 
E and L are coprime. It is basically the Gcd algorithm with some additional temporary values to save values of _s_ and _t_. 
When remainder is 0, algorithm stops. Function returns both s and t, can be simplified to return only one value. When _s_ value is returned, it can be negative, so I 
add to it value of L until it is a positive number. That resulting number is _d_. Public key is a tuple of (N, E) and private key is a tuple of (N, D). 
  
&ensp;&ensp;&ensp; **Encryption**. Formula of encryption is the following: $$c = m^e \pmod{n}$$
where _c_ is resulting ciphertext, _m_ is the plaintext message, _e_ is the second value of public key and _n_ is the first value of public key. 

&ensp;&ensp;&ensp; **Decryption**. Formula of decryption is the following: $$m = c^d \pmod{n}$$
where _c_ is input ciphertext, _m_ is the plaintext message, _d_ is the second value of private key and _n_ is the first value of private key. 

```
public class KeyGenerator : IKeyGenerator
{
    private const uint P = 2689;
    private const uint Q = 103;
    public const uint E = 65537;
    private BigInteger N;
    private BigInteger L;
    private BigInteger D;

    public KeyGenerator()
    {
        N = P * Q;
    }

    public (BigInteger, BigInteger) GeneratePublicKey()
    {
        return (N, E);
    }

    public (BigInteger, BigInteger) GeneratePrivateKey()
    {
        L = LeastCommonMultiple(new BigInteger(P - 1), new BigInteger(Q - 1));
        D = ModularInverse(E, L);
        return (N, D);
    }
}
```

# LCM
```
public static BigInteger LeastCommonMultiple(BigInteger P, BigInteger Q)
{
    var ab = P * Q;
    var gcd = Gcd(P, Q);
    return ab / gcd;
}
```

# Gcd
```
private static BigInteger Gcd(BigInteger a, BigInteger b)
{
    while (a != 0 && b != 0)
    {
        if (a > b)
            a %= b;
        else
            b %= a;
    }

    return a | b;
}
```

# EGcd
```
private static (BigInteger a, BigInteger b)
    ExtendedGcd(BigInteger inverseOf, BigInteger mod)
{
    BigInteger s1 = 0;
    BigInteger t1 = 1;
    BigInteger s2 = 1;
    BigInteger t2 = 0;

    while (inverseOf != 0)
    {
        var q = mod / inverseOf;
        var r = mod % inverseOf;

        var s3 = s1 - s2 * q;
        var t3 = t1 - t2 * q;

        mod = inverseOf;
        inverseOf = r;
        s1 = s2;
        t1 = t2;
        s2 = s3;
        t2 = t3;
    }

    return (s1, t1);
}
```

# ModInverse
```
public static BigInteger ModularInverse(BigInteger left, BigInteger mod)
{
    var egcd = ExtendedGcd(left, mod);

    var result = egcd.a;

    if (result < 0)
        result += mod;

    return result % mod;
}
```


## Screenshots

Plaintext message is saved in the .txt file and used throughout the whole program. 

Results of encryption:
- RSA 
![image](https://user-images.githubusercontent.com/57410984/198647180-ca250486-8b07-4b05-98c1-44b9c5599b65.png)

## Conclusions

In that laboratory work, I got familiar with the asymmetric cryptograph mechanisms and implemented a RSA cipher.
