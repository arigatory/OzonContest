using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace C_Task
{
    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var t = ReadInt();

            for (var tt = 0; tt < t; tt++)
            {
                var n = ReadInt();
                HashSet<string> set = new HashSet<string>();

                for (int nn = 0; nn < n; nn++)
                {
                    var input = _reader.ReadLine();
                    if (CheckSpelling(input))
                    {
                        var normalizeInput = input.ToLower();
                        if (set.Contains(normalizeInput))
                        {
                            _writer.WriteLine("NO");
                        }
                        else
                        {
                            _writer.WriteLine("YES");
                            set.Add(normalizeInput);
                        }
                    }
                    else
                    {
                        _writer.WriteLine("NO");
                    }
                }
                _writer.WriteLine();
            }

            CloseStreams();
        }

        private static bool CheckSpelling(string s)
        {
            if (s[0] == '-')
            {
                return false;
            }
            if (s.Length < 2 || s.Length > 24)
            {
                return false;
            }
            bool isValid = Regex.IsMatch(s, @"^[0-9-_A-Za-z]*$");
            if (isValid)
            {
                return true;
            }

            return false;
        }

        private static void CloseStreams()
        {
            _reader.Close();
            _writer.Close();
        }

        private static void InitialiseStreams()
        {
            _reader = new StreamReader(Console.OpenStandardInput());
            _writer = new StreamWriter(Console.OpenStandardOutput());
        }

        private static int ReadInt()
        {
            return int.Parse(_reader.ReadLine());
        }

        private static List<int> ReadList()
        {
            return _reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }


}
