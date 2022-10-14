using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricCiphers.StreamCipher.Helpers
{
    public class Lfsr
    {
        public int Length { get; set; }
        public int ClockingBit { get; set; }
        public List<int> TappedBits { get; set; }
        public string Key { get; set; }

        public Lfsr(int length, int clockingBit, List<int> tappedBits)
        {
            Length = length;
            ClockingBit = clockingBit;
            TappedBits = tappedBits;
            for (int i = 0; i < length; i++)
            {
                Key += '0';
            }
        }

        public void Xor(string key)
        {
            //will execute 64 rounds, because key is 64 bit long
            foreach (var bit in key)
            { 
                //do the xor thing with tapped bits, take the last bit of tapped bits and previous
                var lfsrBit = TappedBitsXor(Key[TappedBits[^1]], TappedBits.Count - 2);
                //do the last xor with sessionKey bit
                var finalBit = FinalXor(bit, lfsrBit);
                //shift string
                ShiftRight(finalBit);
                Console.WriteLine(Key);
            }
        }

        private char FinalXor(char leftBit, char rightBit)
        {
            if (leftBit == rightBit) return '0';
            return '1';
        }

        private void ShiftRight(char bit)
        {
            //copy everything from 0 to prelast char in Key, last is discarded
            var rightSide = Key[..^1];
            Key = bit + rightSide;
        }

        private char TappedBitsXor(char bit, int index)
        {
            //if at first bit in tapped bits, do the last xor with resulting bit from previous executions
            if (index == 0) return FinalXor(Key[TappedBits[index]], bit);
            return TappedBitsXor(FinalXor(Key[TappedBits[index]], bit), --index);
        }
    }
}
