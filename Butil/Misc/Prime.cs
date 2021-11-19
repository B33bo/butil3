using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace b33bo.utils
{
    /// <summary>Checks if numbers are prime</summary>
    public static class Prime
    {
        /// <summary>Is the current number a prime?</summary>
        /// <param name="num">the number</param>
        /// <returns>true if the number is prime (no divisors)</returns>
        public static bool IsPrime(this uint num)
        {
            if (num <= 1)
                return false; //sorry, 0 and 1

            int floorSquareRoot = Mathf.FloorToInt(Mathf.Sqrt(num));

            for (int i = 2; i <= floorSquareRoot; i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }

        /// <summary>Is the current number a prime?</summary>
        /// <param name="num">the number</param>
        /// <returns>true if the number is prime (no divisors)</returns>
        public static bool IsPrime(this int num)
        {
            if (num < 0)
                return false; //Negatives are NOT prime >:)

            return IsPrime((uint)num);
        }
    }
}
