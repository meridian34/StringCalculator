using ConsoleCalculator.Services;
using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ConsoleService();
            service.Start();
        }
    }
}
