using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Services.Abstractions
{
    public interface IConsoleService
    {
        string ReadLine();
        void WriteLine(string data);
    }
}
