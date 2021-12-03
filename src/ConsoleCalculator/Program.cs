﻿using ConsoleCalculator.Services;
using StringCalculator.Services;
using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringCalculateService = new StringCalculatorService();
            var consoleService = new ConsoleService();
            var consoleCalculateService = new ConsoleCalculateService(stringCalculateService, consoleService);
            consoleCalculateService.Start();
            
        }
    }
}
