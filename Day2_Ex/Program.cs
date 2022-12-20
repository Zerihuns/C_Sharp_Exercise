
using Day2_Ex;

namespace Day2_Ex
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //SelectMany Practice 
            SelectMany.Execute();

            Console.WriteLine("\n\n");

            //GroupBy Practice
            GroupBy.Execute("AAHJDSKHFJKDHGFGSKDKJZFGDSJGFKAS");

            Console.WriteLine("\n\n");

            //Where Practice
            Where.ExecuteWithLambda();
        }
    }
}
