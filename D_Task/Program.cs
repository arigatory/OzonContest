using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace D_Task
{
    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var testsNum = ReadInt();

            for (var t = 0; t < testsNum; t++)
            {
                var namePhonesDict = new Dictionary<string, Queue<string>>();
                var n = ReadInt();
                for (int i = 0; i < n; i++)
                {
                    var items = _reader.ReadLine().Split();
                    var name = items[0];
                    var phone = items[1];
                    if (namePhonesDict.ContainsKey(name))
                    {
                        if (!namePhonesDict[name].Contains(phone))
                        {
                            namePhonesDict[name].Enqueue(phone);
                        }
                        else
                        {
                            namePhonesDict[name] = new Queue<string>(namePhonesDict[name].Where(x=>x!=phone));
                            namePhonesDict[name].Enqueue(phone);
                        }
                    }
                    else
                    {
                        namePhonesDict[name] = new Queue<string>();
                        namePhonesDict[name].Enqueue(phone);
                    }
                    if (namePhonesDict[name].Count > 5)
                    {
                        namePhonesDict[name].Dequeue();
                    }
                }
                foreach (var name in namePhonesDict.Keys.OrderBy(x=>x))
                {
                    _writer.WriteLine($"{name}: {namePhonesDict[name].Count} {string.Join(" ",namePhonesDict[name].Reverse())}");
                }
                _writer.WriteLine();
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
