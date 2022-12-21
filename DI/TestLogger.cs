using System;

namespace DI
{
    class TestLogger
    {
        public TestLogger() { }

        public TestLogger Log(string stepDescription, bool spaceAfter = false)
        {
            Console.WriteLine($"♦ {stepDescription}");
            if (spaceAfter) Console.WriteLine();
            return this;
        }

        public TestLogger Header(string stepDescription)
        {
            Console.WriteLine($"═════ {stepDescription}");
            return this;
        }

        static readonly string defaultSeparator = new('─', 67);
        public TestLogger Separator()
        {
            Console.WriteLine(defaultSeparator);
            return this;
        }
    }
}
