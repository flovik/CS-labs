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

            Console.WriteLine(Key);
        }
    }
}
