using Garage.Common;
using System;
using System.Collections.Generic;

public class ConsoleMock : IConsoleInteraction
{
    public List<string> ConsoleOutputRows = new List<string>();

    public List<ConsoleKeyInfo> ConsoleInputKeys = new List<ConsoleKeyInfo>();

    public List<string> ConsoleInputRows = new List<string>();

    void IConsoleInteraction.Clear()
    {
        ConsoleOutputRows.Clear();
        ConsoleOutputRows.Add(string.Empty);
    }

    public ConsoleKeyInfo ReadKey()
    {
        if (ConsoleInputRows.Count > 0)
        {
            var userinput = ConsoleInputKeys[0];
            ConsoleInputKeys.RemoveAt(0);
            return userinput;
        }
        return new ConsoleKeyInfo();
    }

    void IConsoleInteraction.ForegroundColor(ConsoleColor color)
    {
    }

    void IConsoleInteraction.Write(char character)
    {
        ConsoleOutputRows[ConsoleOutputRows.Count - 1] += character;
    }

    void IConsoleInteraction.WriteLine(string text)
    {
        ConsoleOutputRows.Add(text);
    }

    void IConsoleInteraction.WriteLines(IEnumerable<string> rows)
    {
        foreach (var row in rows)
        {
            ConsoleOutputRows.Add(row);
        }
    }

    public string ReadLine()
    {
        if (ConsoleInputRows.Count > 0)
        {
            var userinput = ConsoleInputRows[0];
            ConsoleInputRows.RemoveAt(0);
            return userinput;
        }
        return string.Empty;
    }
}
