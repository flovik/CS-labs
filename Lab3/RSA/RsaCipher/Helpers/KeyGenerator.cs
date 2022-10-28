using System.Numerics;
using RSA.RsaCipher.Interfaces;
using static RSA.RsaCipher.Helpers.Maths;
namespace RSA.RsaCipher.Helpers;

public class KeyGenerator : IKeyGenerator
{
    private const uint P = 2689; //2689
    private const uint Q = 103; //103
    public const uint E = 65537; //65537
    private BigInteger N;
    private BigInteger L;
    private BigInteger D;

    public KeyGenerator()
    {
        //Step 1, compute n = pq
        N = P * Q;
    }

    public (BigInteger, BigInteger) GeneratePublicKey()
    {
        return (N, E);
    }

    public (BigInteger, BigInteger) GeneratePrivateKey()
    {
        //Step 2, compute lcm(p - 1, q - 1)
        L = LeastCommonMultiple(new BigInteger(P - 1), new BigInteger(Q - 1));
        //Step 3, compute d = e^-1 (mod l(n)), d is modular multiplicative inverse of e modulo l(n)
        D = ModularInverse(E, L);
        return (N, D);
    }
}