using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedCrypto
{
    class BitOperations
    {
        public static byte[] Combine(byte[] a1, byte[] a2)
        {
            byte[] rv = new byte[a1.Length + a2.Length];
            Buffer.BlockCopy(a1, 0, rv, 0, a1.Length);
            Buffer.BlockCopy(a2, 0, rv, a1.Length, a2.Length);
            return rv;
        }

        public static int getIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }

        public static List<BitArray> splitKBits(BitArray keyBits)
        {
            BitArray split1 = new BitArray(16);
            BitArray split2 = new BitArray(16);

            for (int i = 0; i < 32; i++)
            {
                if (i < 16)
                {
                    split1[i] = keyBits[i];
                }
                else
                {
                    split2[i - 16] = keyBits[i];
                }
            }

            List<BitArray> list = new List<BitArray>();
            list.Add(split1); list.Add(split2);

            return list;
        }

        public static BitArray CombineBitArrays(BitArray array1, BitArray array2)
        {
            BitArray combinedArray = new BitArray(32);

            for (int i = 0; i < 32; i++)
            {
                if (i < 16)
                {
                    combinedArray[i] = array1[i];
                }
                else
                {
                    combinedArray[i] = array2[i-16];
                }
            }
            return combinedArray;
        }

        public static String getBinaryString(BitArray bitArray)
        {
            StringBuilder str = new StringBuilder();

            foreach(bool x in bitArray)
            {
                if (x)
                    str.Append("1");
                else
                    str.Append("0");
            }
            return str.ToString();
        }
    }
}
