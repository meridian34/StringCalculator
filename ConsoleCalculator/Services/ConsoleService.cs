using System;
using ConsoleCalculator.Services.Abstractions;
using StringCalculator.Services;

namespace ConsoleCalculator.Services
{
    public class ConsoleService : IConsoleService
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string data)
        {
            Console.WriteLine(data);
        }
    }
}
