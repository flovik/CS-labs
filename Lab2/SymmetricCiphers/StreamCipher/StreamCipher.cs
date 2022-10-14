using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymmetricCiphers.BlockCipher.Interfaces;
using SymmetricCiphers.StreamCipher.Helpers;
using SymmetricCiphers.StreamCipher.Interfaces;
using static SymmetricCiphers.BlockCipher.Helpers.Utils;

namespace SymmetricCiphers.StreamCipher
{
    public class StreamCipher : IStreamCipher
    {
        private readonly Lfsr Lfsr1;
        private readonly Lfsr Lfsr2;
        private readonly Lfsr Lfsr3;
        private string SessionKey { get; set; }
        private string FrameKey { get; set; }

        public StreamCipher(string sessionKey)
        {
            //convert session key string to bits
            SessionKey = ToBinary(ConvertToByteArray(sessionKey));

            var rnd = new Random();
            var temp = new StringBuilder(22);
            for (int i = 0; i < 22; i++)
            {
                temp.Append(rnd.Next(0, 2)); //append to stringbuilder 0 or 1
            }

            FrameKey = temp.ToString();

            //Step 1 - generate 3 LFSRs to generate its pseudo random key stream
            Lfsr1 = new Lfsr(19, 8, new List<int> { 13, 16, 17, 18 });
            Lfsr2 = new Lfsr(22, 10, new List<int> { 20, 21 });
            Lfsr3 = new Lfsr(23, 10, new List<int> { 7, 20, 21, 22});

            //Step 2 - clocking LFSRs with session key, 64 times
            //bits of the 64 bit session key are consecutively XORed in parallel with the feedback
            //of the register
            Lfsr1.Xor(SessionKey);
            Lfsr2.Xor(sessionKey);
            Lfsr3.Xor(sessionKey);


        }



        public string Encrypt(string plaintext)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string ciphertext)
        {
            throw new NotImplementedException();
        }
    }
}
