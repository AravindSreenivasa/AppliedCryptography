
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedCrypto
{
    class key
    {
        BitArray keyBits_32;
        BitArray keyBits_16;
        byte[] keyBytes;
        BitArray KeyBits_Original;
        String KeyString;

        public key(String key_string)
        {
            KeyString = key_string;
            byte[] bytes = UTF8Encoding.Default.GetBytes(KeyString);
            KeyBits_Original = new BitArray(bytes);
            keyBits_32 = KeyBits_Original;
        }

        public BitArray GetNextKey()
        {
            List<BitArray> list = BitOperations.splitKBits(keyBits_32);
            int left = BitOperations.getIntFromBitArray(list[0]);
            left = left << (left % 16) | left >> (16 - (left % 16));
            byte[] byteArray_left = BitConverter.GetBytes((short)left);

            int right = BitOperations.getIntFromBitArray(list[1]);
            right = right >> (right % 16) | right << (16 - (right % 16));
            byte[] byteArray_right = BitConverter.GetBytes((short)right);

            keyBits_16 = new BitArray(BitOperations.Combine(new byte[] { byteArray_left[1] }, new byte[] { byteArray_right[0] }));
            keyBits_32 = new BitArray(BitOperations.Combine(byteArray_left, byteArray_right));

            //remember to remove this
            //Console.WriteLine(BitOperations.getIntFromBitArray(keyBits_16));
            return keyBits_16;
        }

       
    }
}
