using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace G_Task
{
    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;
        private static int[] dx = new int[] { 1, 0, -1, 0, 1, 1, -1, -1 };
        private static int[] dy = new int[] { 0, 1, 0, -1, 1, -1, 1, -1 };

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var tNum = ReadInt();

            for (var t = 0; t < tNum; t++)
            {
                var items = ReadList();
                var n = items[0];
                var m = items[1];
                int[,] board = new int[n, m];

                if (!ReadBoard(n, m, board))
                {
                    _writer.WriteLine("NO");
                }
                else
                {
                    int[,] labeledBoard = board.Clone() as int[,];

                    MakeComponents(board, n, m, labeledBoard);

                    var countShips = CalculateShips(labeledBoard, n, m);
                    if (countShips.Keys.All(x => x == 1 || x == 3 || x == 5 || x == 7))
                    {
                        _writer.WriteLine("YES");
                        foreach (var k in countShips.Keys.OrderBy(x => x))
                        {
                            _writer.Write($"{string.Concat(Enumerable.Repeat(k.ToString() + " ", countShips[k]))}");
                        }
                        _writer.WriteLine();
                    }
                    else
                    {
                        _writer.WriteLine("NO");
                    }
                }
                
            }

            CloseStreams();
        }


        private static Dictionary<int, int> CalculateShips(int[,] labeledBorad, int n, int m)
        {
            var result = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (result.ContainsKey(labeledBorad[i, j]))
                    {
                        result[labeledBorad[i, j]]++;
                    }
                    else
                    {
                        result.Add(labeledBorad[i, j], 1);
                    }
                }
            }

            var newResult = new Dictionary<int, int>();
            result.Remove(0);

            foreach (int v in result.Values)
            {
                if (newResult.ContainsKey(v))
                {
                    newResult[v]++;
                }
                else
                {
                    newResult.Add(v, 1);
                }
            }

            return newResult;
        }

        private static bool ReadBoard(int n, int m, int[,] board)
        {
            for (int i = 0; i < n; i++)
            {
                var s = _reader.ReadLine();
                for (int j = 0; j < m; j++)
                {
                    if (s[j] == '*')
                    {
                        board[i, j] = 1;
                    }
                    else if (s[j] == '.')
                    {
                        board[i, j] = 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static void Dfs(int x, int y, int current_label, int[,] board, int n, int m, int[,] labeledBoard)
        {
            if (x < 0 || x == n)
            {
                return;
            }
            if (y < 0 || y == m)
            {
                return;
            }
            if (board[x, y] != 1 || labeledBoard[x, y] == 0 || labeledBoard[x, y] == current_label)
            {
                return;
            }
            labeledBoard[x, y] = current_label;
            for (int dir = 0; dir < 8; dir++)
            {
                Dfs(x + dx[dir], y + dy[dir], current_label, board, n, m, labeledBoard);
            }
        }

        private static void MakeComponents(int[,] board, int n, int m, int[,] labeledBoard)
        {
            int component = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (labeledBoard[i, j] != 0 && board[i, j] == 1)
                    {
                        Dfs(i, j, ++component, board, n, m, labeledBoard);
                    }
                }
            }
        }

        private static void PrintArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        private static bool CheckRow(string s, int total)
        {
            var starCount = s.Count(c => c == '*');
            var dotCount = s.Count(c => c == '.');
            if (starCount + dotCount != total)
            {
                return false;
            }
            return true;

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
