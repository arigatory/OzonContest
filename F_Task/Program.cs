using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace F_Task
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
                var n = ReadInt();
                var compiledModulesSet = new HashSet<string>();
                var dependModuleDict = new Dictionary<string, List<string>>();
                for (int i = 0; i < n; i++)
                {
                    var s = _reader.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries);
                    var module = s[0];
                    if (s.Length > 1)
                    {
                        var dependModules = s[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        dependModuleDict[module] = new List<string>(dependModules);
                    }
                    else
                    {
                        dependModuleDict[module] = new List<string>();
                    }
                }

                var q = ReadInt();
                for (int j = 0; j < q; j++)
                {
                    var req = _reader.ReadLine();
                    List<string> compiled = Compile(req, compiledModulesSet, dependModuleDict);
                    _writer.WriteLine($"{compiled.Count} {string.Join(" ", compiled)}");
                }
            }

            CloseStreams();
        }

        private static List<string> Compile(string req, HashSet<string> compiledModulesSet, Dictionary<string, List<string>> dependModuleDict)
        {
            if (compiledModulesSet.Contains(req))
            {
                return new List<string>();
            }
            var result  = new List<string>();
            foreach (var module in dependModuleDict[req])
            {
                if (!compiledModulesSet.Contains(module))
                {
                    var compiled = Compile(module, compiledModulesSet, dependModuleDict);
                    result.AddRange(compiled);
                    compiledModulesSet.Add(module);
                }
            }
            compiledModulesSet.Add(req);
            result.Add(req);
            return result;
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
