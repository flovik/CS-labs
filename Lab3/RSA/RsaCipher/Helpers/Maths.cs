using System.Numerics;
using System.Reflection;

namespace RSA.RsaCipher.Helpers;

public static class Maths
{
    public static BigInteger LeastCommonMultiple(BigInteger P, BigInteger Q)
    {
        //apply euclidean algorithm to find lcm
        var ab = P * Q;
        var gcd = Gcd(P, Q);
        return ab / gcd;
    }

    private static BigInteger Gcd(BigInteger a, BigInteger b)
    {
        //at each iteration, a or b will hold the remainder of mod operation 
        //other one will hold its predecessor and vice versa until remainder is 0 
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    public static BigInteger ModularInverse(BigInteger left, BigInteger mod)
    {
        //returns a and b factors of ax + by = gcd(left, mod), only need a
        var egcd = ExtendedGcd(left, mod);

        var result = egcd.a;

        if (result < 0)
            result += mod;

        return result % mod;
    }

    private static (BigInteger a, BigInteger b) 
        ExtendedGcd(BigInteger inverseOf, BigInteger mod)
    {
        //function returns both s and t, can be simplified to return only one value

        //sa + tb = gcd(a,b)
        //inverseOf (calculate inverse of input), mod (with corresponding modulo) 
        //q (quotient of mod / inverseOf), r (remainder of mod / inverseOf)
        
        //first row initial values
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

            mod = inverseOf; //as in euclidean algorithm, move the previous inverseOf to current mod
            inverseOf = r; //inverseOf will be the remainder of 
            s1 = s2; //s1 becomes s2 from previous row
            t1 = t2; //t1 becomes t2 from previous row 
            s2 = s3; //s2 becomes s3 from previous row
            t2 = t3; //t2 becomes t3 from previous row 
        }

        return (s1, t1);
    }
}