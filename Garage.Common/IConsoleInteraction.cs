using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Common
{
    public interface IConsoleInteraction
    {
        void ForegroundColor(ConsoleColor color);

        void Write(char character);

        void WriteLine(string text);

        void WriteLines(IEnumerable<string> rows);

        void Clear();

        ConsoleKeyInfo ReadKey();

        string ReadLine();
    }
}
