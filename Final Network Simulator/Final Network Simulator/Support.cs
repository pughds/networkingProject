using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Final_Network_Simulator
{
    class SupportClasses
    {
        //Converts the ASCII message input by the user to binary
        public static string binPrint(string input)
        {
            string returnString = null;
            string binaryMessage = input;
            foreach (char ch in binaryMessage)
            {
                //Prepends the appropriate number of zeros to the beginning of a binary conversion 
                if ((int)ch < 64)
                {
                    returnString += "00";
                }
                else
                {
                    returnString += "0";
                }
                returnString += Convert.ToString((int)ch, 2);

            }
            Console.WriteLine(returnString);
            return returnString;

        }

        //Builds the  Ethernet frame
        public static string buildFrame(string dest, string source)
        {
            string frameString = null;
            string typeField = "0806";
            frameString += "1010101010101010101010101010101010101010101010101010101010101011";
            string strippedDestString = dest.Replace(":", "");
            string strippedSourceString = source.Replace(":", "");
            frameString += Convert.ToString(Convert.ToInt64(strippedDestString, 16), 2);
            frameString += Convert.ToString(Convert.ToInt64(strippedSourceString, 16), 2);
            frameString += Convert.ToString(Convert.ToInt64(typeField, 16), 2);
            return frameString;
        }

        public static string generateCRC(string a)
        {
            string input = a;
            int divisor = Convert.ToInt32("10111010", 2);

            input += "0000000";
            return Convert.ToString(Convert.ToInt64(input, 2) % divisor, 2);
        }

        public static bool checkCRC(string a)
        {
            string checkString = a.Substring(0,a.Length-7);
            Console.WriteLine(checkString);
            string crcValue = a.Substring(a.Length - 7);
            Console.WriteLine(crcValue);
            return (crcValue==generateCRC(checkString));
        }

    }
}
