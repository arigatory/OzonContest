using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace E_Task
{
    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var tNum = ReadInt();

            for (var t = 0; t < tNum; t++)
            {
                _reader.ReadLine();
                var items = ReadList();
                var n = items[0];
                var total = 2*n;
                var q = items[1];
                var freePlaces = new HashSet<int>(Enumerable.Range(1, total));
                var freeQp = new SortedSet<int>(Enumerable.Range(1,n));
                for (int i = 0; i < q; i++)
                {
                    var req = ReadList();
                    var reqType = req[0];
                    if (reqType == 1)
                    {
                        var place = req[1];
                        if (freePlaces.Contains(place))
                        {
                            _writer.WriteLine("SUCCESS");
                            freePlaces.Remove(place);
                            if (place%2==0)
                            {
                                freeQp.Remove(place / 2);
                            }
                            else
                            {
                                freeQp.Remove((place+1)/2);
                            }
                        }
                        else
                        {
                            _writer.WriteLine("FAIL");
                        }
                    }
                    else if (reqType == 2)
                    {
                        var place = req[1];
                        if (!freePlaces.Contains(place))
                        {
                            _writer.WriteLine("SUCCESS");
                            freePlaces.Add(place);
                            if (place%2==0)
                            {
                                if (freePlaces.Contains(place-1))
                                {
                                    freeQp.Add(place/2);
                                }
                            }
                            else
                            {
                                if (freePlaces.Contains(place + 1))
                                {
                                    freeQp.Add((place + 1) / 2);
                                }
                            }
                        }
                        else
                        {
                            _writer.WriteLine("FAIL");
                        }
                    }
                    else if (reqType == 3)
                    {
                        var qp = freeQp.FirstOrDefault();
                        if (qp!=0)
                        {
                            var p2 = qp*2;
                            var p1 = p2-1;
                            _writer.WriteLine($"SUCCESS {p1}-{p2}");
                            freeQp.Remove(qp);
                            freePlaces.Remove(p1);
                            freePlaces.Remove(p2);
                        }
                        else
                        {
                            _writer.WriteLine("FAIL");
                        }
                    }
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
