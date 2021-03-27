using System;
using System.Linq;
using System.Collections.Generic;

namespace BIO_Exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Palindrome("17"));
        }
        public static List<string> NumToString(int number)
        {
            return number.ToString().Split("").ToList();
        }
        public static int Palindrome(string input)
        {
            var number = Convert.ToInt32(input);
            var numberArray = new List<string>();
            var ReversedArray = new List<string>();
            while(true)
            {
                numberArray = NumToString(number);
                ReversedArray = NumToString(number);
                numberArray.Reverse();
                if(ReversedArray.SequenceEqual(numberArray)) return number;
                else number += 1;
            }
        }
    }
}
