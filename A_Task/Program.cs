using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A_Task
{
    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var t = ReadInt();

            for (var i = 0; i < t; i++)
            {
                var n = ReadInt();
                var prices = ReadList();
                Dictionary<int, int> priceCountDict = new Dictionary<int, int>();
                foreach (var price in prices)
                {
                    if (priceCountDict.ContainsKey(price))
                    {
                        priceCountDict[price]++;
                    }
                    else
                    {
                        priceCountDict.Add(price, 1);
                    }
                }
                int sum = 0;
                foreach (int price in priceCountDict.Keys)
                {
                    sum += price * (priceCountDict[price] - priceCountDict[price]/3);
                }
                _writer.WriteLine(sum);
            }

            CloseStreams();
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
