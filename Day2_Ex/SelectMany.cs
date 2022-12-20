using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Ex
{
    static class SelectMany
    {
       static IEnumerable<string> doubleLetters;
        static SelectMany()
        {
            doubleLetters = Enumerable.Range((char)65, 26)
                        .SelectMany(x => Enumerable.Range((char)65, 26).Select(y => (char)x + "" + (char)y));
        }

        //Write a query that returns double letters sequence in format: AA AB AC ... ZX ZY ZZ
        static public void Execute()
        {
            foreach (var doubleLetter in doubleLetters)
            {
                Console.Write(doubleLetter + " "); // AA AB AC ... AZ BA BB ... ZX ZY ZZ
            }
        }
    }
}
