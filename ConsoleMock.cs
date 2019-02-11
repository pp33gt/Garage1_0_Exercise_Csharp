//using System;

//public class ConsoleMock : IConsoleInteraction
//{
//    public List<string> ConsoleOutputRows = new List<string>();

//    public List<ConsoleKeyInfo> ConsoleInputRows = new List<ConsoleKeyInfo>();

//    void IConsoleInteraction.Clear()
//    {
//        ConsoleOutputRows.Clear();
//        ConsoleOutputRows.Add(string.Empty);
//    }

//    public ConsoleKeyInfo ReadKey()
//    {
//        if (ConsoleInputRows.Count > 0)
//        {
//            var userinput = ConsoleInputRows[0];
//            ConsoleInputRows.RemoveAt(0);
//            return userinput;
//        }
//        return new ConsoleKeyInfo();
//    }

//    void IConsoleInteraction.ForegroundColor(ConsoleColor color)
//    {
//    }

//    void IConsoleInteraction.Write(char character)
//    {
//        ConsoleOutputRows[ConsoleOutputRows.Count - 1] += character;
//    }

//    void IConsoleInteraction.WriteLine(string text)
//    {
//        ConsoleOutputRows.Add(string.Empty);
//    }

//    void IConsoleInteraction.WriteLines(IEnumerable<string> rows)
//    {
//        foreach (var row in rows)
//        {
//            ConsoleOutputRows.Add(row);
//        }
//    }
//}
