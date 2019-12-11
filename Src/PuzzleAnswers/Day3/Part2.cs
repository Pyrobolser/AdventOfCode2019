using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.PuzzleAnswers.Day3
{
    public static class Part2
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
            int x = 0, y = 0;

            for (int i = 0; i < input.Count; i++)
            {
                wires[i] = new HashSet<Point>();
                x = 0;
                y = 0;

                for (int c = 0; c < input[i].Count(); c++)
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

            var intersections = wires[0].Intersect(wires[1]);
            var result = int.MaxValue;
            var currentResult = 0;

            foreach(var intersection in intersections)
            {
                currentResult = 0;

                for (int i = 0; i < input.Count; i++)
                {
                    x = 0;
                    y = 0;

                    for (int c = 0; c < input[i].Count; c++)
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

                            currentResult++;
                            if (intersection.X == x && intersection.Y == y)
                                break;
                        }

                        if (intersection.X == x && intersection.Y == y)
                        {
                            if (i == (input.Count - 1) && currentResult < result)
                                result = currentResult;
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
