# Symmetric Ciphers. Stream Ciphers. Block Ciphers.

### Course: Cryptography & Security
### Author: Florescu Victor

----
&ensp;&ensp;&ensp; A symmetric cipher is one that uses the same key for encryption and decryption. Examples of symmetric ciphers are Advanced Encryption Standard (AES), Data Encryption Standard (DES), Blowfish, and International Data Encryption Algorithm (IDEA).


### A5/1 Stream cipher

&ensp;&ensp;&ensp; A5/1 is a stream cipher used to provide over-the-air communication privacy in the GSM cellular telephone standard. It is one of several implementations of the A5 security protocol. It was initially kept secret, but became public knowledge through leaks and reverse engineering.

### Data Encryption Standard

&ensp;&ensp;&ensp; The Data Encryption Standard is a symmetric-key algorithm for the encryption of digital data. 
Although its short key length of 56 bits makes it too insecure for modern applications, it has been highly influential in the advancement of cryptography.
Developed in the early 1970s at IBM and based on an earlier design by Horst Feistel,
the algorithm was submitted to the National Bureau of Standards (NBS) following the agency's invitation to propose a candidate for the protection of sensitive, 
unclassified electronic government data.

## Objectives:
1. Get familiar with the symmetric cryptography, stream and block ciphers.

2. Implement an example of a stream cipher.

3. Implement an example of a block cipher.

4. The implementation should, ideally follow the abstraction/contract/interface used in the previous laboratory work.

## Implementation description

* A5/1 Stream cipher

&ensp;&ensp;&ensp; To start encryption and decryption, we need to initialize the A5/1 cipher. Firstly, we need a 64-bit session key and 22-bit frame key. Afterwards, LFSRs are
initialized with their respective values of clocking bit, tapped bit. Initial values of LSFRs keys are zeros. After initialization, apply for each LFSR the XOR step, where key
is XORed with the session key 64 times. Then, the LFSRs keys are XORed 22 times. Next step is to clock LFSRs which clocking bit is the same as the majority bit in all 3 LFSRs. 
The minoroty LFSRs are left unchanged. Then, apply XOR to last bits of each LFSRs keys, create the Key Stream (which will be used in encryption) by appending the result of 3 bits XOR,
and apply the previous step with majority clocking. 

&ensp;&ensp;&ensp; Encryption is done by applying XOR between Key Stream bit and plaintext bit.

```
public void Xor(string key)
{
    foreach (var bit in key)
    {
        var lfsrBit = TappedBitsXor(Key[TappedBits[^1]], TappedBits.Count - 2);
        var finalBit = FinalXor(bit, lfsrBit);
        ShiftRight(finalBit);
    }
}
```

```
public StreamCipher(string sessionKey)
{
    Lfsr1 = new Lfsr(19, 8, new List<int> { 13, 16, 17, 18 });
    Lfsr2 = new Lfsr(22, 10, new List<int> { 20, 21 });
    Lfsr3 = new Lfsr(23, 10, new List<int> { 7, 20, 21, 22 });

    Lfsr1.Xor(SessionKey);
    Lfsr2.Xor(SessionKey);
    Lfsr3.Xor(SessionKey);

    Lfsr1.Xor(FrameKey);
    Lfsr2.Xor(FrameKey);
    Lfsr3.Xor(FrameKey);

    for (int i = 0; i < 100; i++)
    {
        var bit = MajorityVote();
        if (Lfsr1.Key[Lfsr1.ClockingBit] == bit)
        {
            Lfsr1.MajorityVote();
        }
        if (Lfsr2.Key[Lfsr2.ClockingBit] == bit)
        {
            Lfsr2.MajorityVote();
        }
        if (Lfsr3.Key[Lfsr3.ClockingBit] == bit)
        {
            Lfsr3.MajorityVote();
        }
    }

    for (int i = 0; i < 228; i++)
    {
        var keyStreamChar = Utils.Xor(Lfsr1.Key[^1], Lfsr2.Key[^1]);
        keyStreamChar = Utils.Xor(keyStreamChar, Lfsr3.Key[^1]);
        KeyStream.Append(keyStreamChar);

        var bit = MajorityVote();
        if (Lfsr1.Key[Lfsr1.ClockingBit] == bit)
        {
            Lfsr1.MajorityVote();
        }
        if (Lfsr2.Key[Lfsr2.ClockingBit] == bit)
        {
            Lfsr2.MajorityVote();
        }
        if (Lfsr3.Key[Lfsr3.ClockingBit] == bit)
        {
            Lfsr3.MajorityVote();
        }
    }
}
private char MajorityVote()
{
    var clockingBits = new List<char>
    {
                Lfsr1.Key[Lfsr1.ClockingBit],
                Lfsr2.Key[Lfsr2.ClockingBit],
                Lfsr3.Key[Lfsr3.ClockingBit],
            };

    var zeros = clockingBits.Count(c => c == '0');
    return zeros > 1 ? '0' : '1';
}
```

* Data Encryption Standard

&ensp;&ensp;&ensp; DES consists of some main parts: Initial Permutation, Key Generation, Rounds, SBox Substituion and Final Permutation. 
DES is initialized by applying initial permutation. In *Permutation(block)* change bits of the block with the bits of initial permutation matrix, which then is 
split into two halves of 32 bits. After that, Key Generation is done with the secret key you want, I used "secrtkey" (64 bits), every 8th bit of that key is discarded
by applying *ChangeBits(matrix)*, that 56-bit key is split into two halves of 28-bit keys. Now that we apply 16 rounds, we will need 16 keys, every time that 28-bit keys
are shifted using ShiftRounds list with how many shifts should be applied to each round key, and change the bits of key using another matrix. That key is saved and will be
used in Rounds. 

&ensp;&ensp;&ensp; When applying 16 rounds on each block of text that needs to be encrypted, the right half from initial permutation is expanded to 48-bit key, then it is
XORed with the key of the current round. That 48-bit key is passed to Sbox Substituion part, where it is split into 8 blocks of 6 bits each, one block per box, now for each 
block we should get the row and column of Sbox. Row is calculated by the first and last bit of 6-bit chunk, column is calculated by the rest of bits from 1-5 of 6-bit chunk.
Find value of Sbox[row, column], convert to binary and add zeros to the left is not length 4. 

&ensp;&ensp;&ensp; After Sbox substitution, permute the result, XOR it with leftPlainText and swap leftPlainText with RightPlaintText. Repeat 15 more times. After block is
processed 16 times, apply one final permutation on the left plain text and right plain text results.

&ensp;&ensp;&ensp; Encryption is done by splitting the whole text into blocks of 8 chars (64 bits), add padding if needed by
PKCS7 (add char of length that is missing to fill padding until length is accurate). Result is returned in Hex. Decryption is
also returned in Hex. 


```
foreach (var block in blockText)
{
    //Initial Permutation
    (string leftPlainText, string rightPlainText) = _initialPermutation.Permutation(block);
    var temp = string.Empty;

    //Perform 16 rounds on block
    for (int i = 0; i < 16; i++)
    {
        //expand rightPlainText to match 48bit key
        var rightPlainTextExpanded = _round.ExpandBlock(rightPlainText);

        //xor with a key
        var xor = _round.Xor(rightPlainTextExpanded, _keyGenerator.KeyList[Math.Abs(i - encrypt)]);

        //SBox substitution
        temp = _sBoxSubstitution.Permute(xor);

        //round permutation
        var temp2 = _round.PermuteBlock(temp);

        //xor with leftPlainText
        xor = _round.Xor(leftPlainText, temp2);

        //swap LFT with RPT
        leftPlainText = rightPlainText;
        rightPlainText = xor;
    }

    result.Add(_finalPermutation.Permute(rightPlainText + leftPlainText));
}
```

```
public (string, string) Permutation(string binaryString)
{
    binaryString = ChangeBits(binaryString, Matrix);
    return (binaryString[..32], binaryString[32..]);
}
```

```
private void GenerateKeys()
{
    for (int i = 0; i < 16; i++)
    {
        left = ShiftLeftBy(left, ShiftRounds[i]);
        right = ShiftLeftBy(right, ShiftRounds[i]);
        var key = left + right;
        var newKey = ChangeBits(key, KeyMatrix2);
        KeyList.Add(newKey);
    }
}
```

```
public string SBoxPermute(string block)
{
    var result = new List<string>();
    var blocks = block.SplitInto(6).ToList();
    for (int i = 1; i <= blocks.Count; i++)
    {
        var chunk = blocks[i - 1];
        var row = Convert.ToInt32(new string(new char[] { chunk[0], chunk[^1] }), 2);
        var column = Convert.ToInt32(new string(new[] { chunk[1], chunk[2], chunk[3], chunk[4] }), 2);
        var box = Sboxes[i];
        var SBoxValue = box[row, column];
        var binary = Convert.ToString(SBoxValue, 2).PadLeft(4, '0'); //add zeros to the left if not len 4
        result.Add(binary);
    }

    return string.Join("", result);
}
```

## Screenshots

Plaintext message is saved in the .txt file and used throughout the whole program. 

Results of encryption:
- A5/1 
![image](https://user-images.githubusercontent.com/57410984/195867743-d3791e66-9013-4f1d-8428-2ea784190072.png)

- DES 
![image](https://user-images.githubusercontent.com/57410984/195873489-e5a3dfe9-c9e1-4c72-84bd-2e4a043f2cfa.png)
![image](https://user-images.githubusercontent.com/57410984/195873819-8fe10071-e580-4d01-9651-125483ba36a7.png)
![image](https://user-images.githubusercontent.com/57410984/195874122-ccd10259-5224-4e24-8e93-d9fb86815498.png)
![image](https://user-images.githubusercontent.com/57410984/195874529-48ddd795-90ce-4635-8e4f-52c400346559.png)



## Conclusions

In that laboratory work, I got familiar with the symmetric cryptography, stream and block ciphers.
