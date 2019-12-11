using System;
using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day2
{
    public static class Part2
    {
        public static (int, int) GetResult()
        {
            int[] input = Array.ConvertAll(File.ReadAllText("Inputs/Day2.txt").Split(','), int.Parse);
            int[] memory = new int[input.Length];

            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    Array.Copy(input, memory, input.Length);
                    memory[1] = noun;
                    memory[2] = verb;

                    for (int i = 0; i < memory.Length; i += 4)
                    {
                        if (memory[i] == 1)
                        {
                            memory[memory[i + 3]] = memory[memory[i + 1]] + memory[memory[i + 2]];
                        }
                        else if (memory[i] == 2)
                        {
                            memory[memory[i + 3]] = memory[memory[i + 1]] * memory[memory[i + 2]];
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (memory[0] == 19690720)
                        return (noun, verb);
                }
            }

            throw new Exception("Cannot find a suitable noun and verb between 0 and 99");
        }
    }
}
