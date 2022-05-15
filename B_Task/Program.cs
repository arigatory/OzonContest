using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace B_Task
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
                _reader.ReadLine();
                var nm = ReadList();
                var n = nm[0];
                var m = nm[1];
                var array = new List<List<int>>();
                for (int i = 0; i < n; i++)
                {
                    var nums = ReadList();
                    array.Add(new List<int>(nums));
                }
                var k = ReadInt();
                var c = ReadList();

                for (int i = 0; i < k; i++)
                {
                    array = array.OrderBy(x => x[c[i] - 1]).ToList();
                }
                Print(array);
            }

            CloseStreams();
        }

        private static void Print(List<List<int>> ar)
        {
            var n = ar.Count;
            for (int i = 0; i < n; i++)
            {
                _writer.WriteLine(string.Join(" ", ar[i]));
            }
            _writer.WriteLine();
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
