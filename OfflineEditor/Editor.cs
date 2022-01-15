using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Editor
{

    // https://codereview.stackexchange.com/questions/191503/simple-console-text-editor

    public class Editor
    {

        // private Buffer _buffer;
        // private Cursor _cursor;
        // Stack<object> _history;

        public Editor()
        {
            var lines = ReadLine("");
            Console.Write(lines);
        }

        static string ReadLine(string Default)
        {
            int pos = Console.CursorLeft;
            Console.Write(Default);
            ConsoleKeyInfo info;
            List<char> chars = new List<char>();
            if (string.IsNullOrEmpty(Default) == false)
            {
                chars.AddRange(Default.ToCharArray());
            }

            while (true)
            {
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Backspace && Console.CursorLeft > pos)
                {
                    chars.RemoveAt(chars.Count - 1);
                    Console.CursorLeft -= 1;`
                    Console.Write(' ');
                    Console.CursorLeft -= 1;
                }
                else if (info.Key == ConsoleKey.Enter)
                {
                    Console.Write('\n');
                    chars.Add('\n');
                }
                else if (char.IsLetterOrDigit(info.KeyChar))
                {
                    Console.Write(info.KeyChar);
                    chars.Add(info.KeyChar);
                }
                else if (info.Key == ConsoleKey.CursorLeft) {
                    Console.CursorLeft -= 1;
                }
                else if (info.Key == ConsoleKey.Escape) {
                    break;
                }
            }
            return new string(chars.ToArray());
        }
    }
}