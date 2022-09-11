using System;
using System.Net.NetworkInformation;

namespace MiniTests
{
    public static class EmptyFunctions
    {
        public static int GetDigit(int n, int digit)
        {
            string number = n.ToString();
            int pos = number.Length - digit;
            return Int32.Parse(number[pos].ToString());
        }
        
        public static bool IsNumberPalindrome(int n)
        {
            string number = n.ToString();
            bool palindrome = true;
            
            if (number.Length % 2 == 0) {
                for (int i = 0; i < number.Length / 2 ; i++) 
                {
                    if (number[i] != number[number.Length - i - 1]) {
                        palindrome = false;
                    }
                }
            }
            return palindrome;
        }
    }
}