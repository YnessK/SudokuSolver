using System;

namespace MiniTests
{
    public static class SuspiciousFunctions
    {
        public static int Fibonacci(int n)
        {
            if (n == 0) {
                return 0;
            }
            if (n == 1) {
                return 1;
            }

            var f1 = 1;
            var f2 = 1;

            for (var i = 2; i < n; i++)
            {
                int tmp = f1 + f2;
                f1 = f2;
                f2 = tmp;
            }

            return f2;
        }

        public static int ViceMax(int[] array)
        {
            var max = array[0];
            var viceMax = array[0];

            foreach (var x in array)
            {
                if (x > max) {
                    viceMax = max;
                    max = x;
                }
                if (x < max && x > viceMax) {
                    viceMax = x;
                }
                if (max == viceMax && x != max) {
                    viceMax = x;
                }
            }

            return viceMax;
        }
        
    }
}