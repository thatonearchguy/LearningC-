using System;
using System.Linq;
using System.Collections.Generic;

namespace BIO_Exercises
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(WatchClock(1, 31));
        }
        public static List<int> calculateTimes(int fast)
        {
            var Intervals = new List<int>{0, 0};
            Intervals[1] = fast%60;
            Intervals[0] = ((fast/60)%24)+1;
            return Intervals;
        }
        public static int mod(int x, int m){return (x%m + m)%m;}
        public static List<int> AddTimes(List<int> clock, List<int> add)
        {
                var newvalue = clock[1]+add[1];
                if (newvalue > 60)
                {
                    clock[1]=mod(newvalue, 60);
                    clock[0]=mod((newvalue/60),24);
                }
                else{
                    clock[1] = newvalue;
                }
                clock[0] = mod((clock[0]+add[0]),24);
                return clock;
        }
        public static string WatchClock(int firstFast, int secondFast)
        {
            var firstClock = new List<int>{0, 0};
            var secondClock = new List<int>{0, 0};
            var firstAdd = calculateTimes(firstFast);
            var secondAdd = calculateTimes(secondFast);
            do 
            {
                firstClock = AddTimes(firstClock, firstAdd);
                secondClock = AddTimes(secondClock, secondAdd);
            } while (firstClock.SequenceEqual(secondClock)!=true);
            return String.Join(':', firstClock.ToArray());
        }  
    }
}