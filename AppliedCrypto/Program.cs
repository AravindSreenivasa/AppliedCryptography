using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedCrypto
{
    class Program
    {
        static String OUTPUT_PATH = "D:\\Masters\\2nd sem\\Applied Crypto\\Homework 1\\Output\\";
        static String INPUT_PATH = "D:\\Masters\\2nd sem\\Applied Crypto\\Homework 1\\Input\\";
        static BitArray fileData;

        static void Main(string[] args)
        {
            encryption encryptObject = new encryption();
            encryptObject.Encrypt(INPUT_PATH + "2.jpg", "1234", OUTPUT_PATH + "2.jpg");

            decryption decryptionObject = new decryption();
            decryptionObject.decrypt(OUTPUT_PATH + "2.jpg", "1234", OUTPUT_PATH + "2_d.jpg");
        }

        
    }
}
