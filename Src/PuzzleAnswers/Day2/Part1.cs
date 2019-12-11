using System;
using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day2
{
    public static class Part1
    {
        public static int GetResult()
        {
            int[] input = Array.ConvertAll(File.ReadAllText("Inputs/Day2.txt").Split(','), int.Parse);

            // Restore "1202 program alarm" state
            input[1] = 12;
            input[2] = 2;

            for (int i = 0; i < input.Length; i += 4)
            {
                if (input[i] == 1)
                {
                    input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];
                }
                else if (input[i] == 2)
                {
                    input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                }
                else
                {
                    break;
                }
            }

            return input[0];
        }
    }
}
