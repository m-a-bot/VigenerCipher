using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PVigenerCipher
{
    public class Tools
    {
        public static string Stuff(string placeholder, int lengthSource)
        {
            string result = "";

            for (int i=0, indexOfPlaceHolder=0; i < lengthSource; i++, indexOfPlaceHolder++) 
            {
                indexOfPlaceHolder %= placeholder.Length;
                result += placeholder[indexOfPlaceHolder];
            }

            return result;
        }

        public static int Mod(int a, int b)
        {
            int c = a % b;

            if (c < 0)
                c += b;

            return c;
        }
    }
}
