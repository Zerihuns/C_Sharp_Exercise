using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Ex
{
    static class GroupBy
    {
       static IEnumerable<IGrouping<char,char>> letters;

        //query that returns letters and their frequencies in the string.\  
        static public void Execute(string word)
        {
            var letters = word.GroupBy(x => x);

            foreach (var l in letters)
            {
                Console.Write($"Letter {l.Key} occurs {l.Count()} time(s), \n");
                // Letter a occurs 5 time(s), Letter b occurs 2 time(s), Letter r occurs 2 time(s)
                // Letter r occurs 2 time(s), Letter c occurs 1 time(s), Letter d occurs 1 time(s)
            }

        }

    }

}
