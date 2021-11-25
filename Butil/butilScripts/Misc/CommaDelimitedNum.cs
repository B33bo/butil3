using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Numerics;
using b33bo.utils;

namespace b33bo.numerics
{
    /// <summary>Puts numbers in the number. For example 100000000 -> 100,000,000</summary>
    public static class CommaDelimitedNum
    {
        /// <summary>Puts numbers in the number. For example 100000000 -> 100,000,000</summary>
        public static string ToCommaDelimited(string number)
        {
            string newStr = "";

            //Backwards for loop
            for (int i = number.Length - 1; i >= 0; i--)
            {
                newStr += number[i];

                //If the current index (if this was forwards) is divisible by 3...
                if ((number.Length - i) % 3 == 0)
                    newStr += ",";
            }

            newStr = newStr.Flipped();

            if (newStr.StartsWith(","))
                newStr = newStr.Substring(1);

            return newStr;
        }

        /// <summary>Puts numbers in the number. For example 100000000 -> 100,000,000</summary>
        public static string ToCommaDelimited(this int number)
        {
            return ToCommaDelimited(number.ToString());
        }

        /// <summary>Puts numbers in the number. For example 100000000 -> 100,000,000</summary>
        public static string ToCommaDelimited(this uint number)
        {
            return ToCommaDelimited(number.ToString());
        }

        /// <summary>Puts numbers in the number. For example 100000000 -> 100,000,000</summary>
        public static string ToCommaDelimited(this long number)
        {
            return ToCommaDelimited(number.ToString());
        }

        /// <summary>Puts numbers in the number. For example 100000000 -> 100,000,000</summary>
        public static string ToCommaDelimited(this ulong number)
        {
            return ToCommaDelimited(number.ToString());
        }

        /// <summary>Puts numbers in the number. For example 100000000 -> 100,000,000</summary>
        public static string ToCommaDelimited<T>(T number)
        {
            return ToCommaDelimited(number.ToString());
        }
    }
}
