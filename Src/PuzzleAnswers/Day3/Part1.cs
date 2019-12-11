using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.PuzzleAnswers.Day3
{
    public static class Part1
    {
        internal struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }

        public static int GetResult()
        {
            var input = File.ReadAllLines("Inputs/Day3.txt")
                .Select(l => l.Split(',').Select(p => (direction: p[0], distance: int.Parse(p[1..]))).ToList())
                .ToList();

            var wires = new HashSet<Point>[2];

            for(int i = 0; i < input.Count; i++)
            {
                wires[i] = new HashSet<Point>();
                int x = 0, y = 0;

                for(int c = 0; c < input[i].Count; c++)
                {
                    for (int p = 0; p < input[i][c].distance; p++)
                    {
                        switch (input[i][c].direction)
                        {
                            case 'R':
                                x++;
                                break;
                            case 'L':
                                x--;
                                break;
                            case 'U':
                                y++;
                                break;
                            case 'D':
                                y--;
                                break;
                        }

                        wires[i].Add(new Point(x, y));
                    }
                }
            }

            var result = wires[0].Intersect(wires[1]).Min(p => Math.Abs(p.X) + Math.Abs(p.Y));

            return result;
        }
    }
}
