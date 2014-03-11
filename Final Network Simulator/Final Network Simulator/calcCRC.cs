using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Network_Simulator
{
    class calcCRC
    {
        public string calculateCRC(string a, bool check)
        {
            string input = a;
            int divisor = Convert.ToInt32("01101001",2);

            if (!check)
            {
                input += "0000000";
                return Convert.ToString(Convert.ToInt32(input, 2) % divisor, 2);
            }
            else {
                return "Didn't work";
            }

        }
    }
}
