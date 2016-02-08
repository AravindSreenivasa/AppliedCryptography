using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppliedCrypto
{
    class decryption
    {
        String filename;
        int offset;
        BitArray fileBits;
        StringBuilder finalString;
        private const int DelayOnRetry = 1000;

        public decryption()
        {
            offset = 0;
            finalString = new StringBuilder();
        }

        public void decrypt(String inputFile, String keyString, String outputFile)
        {
            filename = inputFile;
            fileBits = GetFileBits();
            byte[] byteArray = new byte[fileBits.Length / 8];
            int index = 0;
            int counter = 0;

            while (counter < fileBits.Length)
            {
                BitArray Bits_32 = ReadNext32Bits();
                counter += 32;
                //Console.WriteLine("Input 32 bits : " + BitOperations.getBinaryString(Bits_32));

                BitArray encrypted32bits = decrypt32bits(keyString, Bits_32);
                //Console.WriteLine("Input 32 bits after encryption : " + BitOperations.getBinaryString(encrypted32bits));

                encrypted32bits.CopyTo(byteArray, index);
                index += 4;

            }
            try
            {
                File.WriteAllBytes(outputFile, byteArray);
            }
            catch (IOException e)
            {
                Thread.Sleep(DelayOnRetry);
            }

        }

        private BitArray decrypt32bits(String keyString, BitArray bitText32)
        {
            key obj = new key(keyString);
            BitArray keyBits = new BitArray(16);
            Stack keyStack = setKeyStack(keyString);
            List<BitArray> list = BitOperations.splitKBits(bitText32);
            BitArray right = new BitArray(16);
            BitArray left = new BitArray(16);

            for (int i = 0; i < 8; i++)
            {
                keyBits = (BitArray) keyStack.Pop();
                right = (BitArray)list[1].Clone();
                //Console.WriteLine("Input 32 bits split into : " + BitOperations.getBinaryString(list[0]) +" and "+ BitOperations.getBinaryString(list[1]));

                left = list[0].Xor(feistelFunction(keyBits, list[1]));
                //Console.WriteLine("left after xoring woth fiestal output : " + BitOperations.getBinaryString(left));
                list[0] = right; list[1] = left;
            }
            return BitOperations.CombineBitArrays(left, right);
        }

        private BitArray feistelFunction(BitArray keyBits, BitArray Bittext_16)
        {
            BitArray afterParity = Bittext_16;
            if (checkParity(keyBits)) //true if parity is even
            {
                afterParity = Bittext_16.Not();
                //Console.WriteLine("Right after negating : " + BitOperations.getBinaryString(afterParity));
            }
            //Console.WriteLine("Keybits for xoring : " + BitOperations.getBinaryString(keyBits));
            var feistelOutput = keyBits.Xor(new BitArray(afterParity));
            //Console.WriteLine("right 16 bits after fiestal function : " + BitOperations.getBinaryString(a));
            return feistelOutput;
        }

        private bool checkParity(BitArray keyBits)
        {
            return true;
        }

        private BitArray ReadNext32Bits()
        {
            BitArray next32bits = new BitArray(32);

            for (int i = 0; i < 32; i++)
            {
                if (offset < fileBits.Length)
                    next32bits[i] = fileBits[offset++];
            }

            return next32bits;
        }

        private BitArray GetFileBits()
        {
            byte[] bytes;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
            }
            return new BitArray(bytes);
        }

        private Stack setKeyStack(string keyString)
        {
            key obj = new key(keyString);
            Stack keyStack = new Stack();
            for (int counter = 0; counter < 8; counter++)
            {
                keyStack.Push(obj.GetNextKey());
            }

            return keyStack;
        }
        

        
    }
}
