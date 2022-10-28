using System.Numerics;

namespace RSA.RsaCipher.Interfaces;

public interface IKeyGenerator
{
    (BigInteger, BigInteger) GeneratePublicKey();
    (BigInteger, BigInteger) GeneratePrivateKey();
}