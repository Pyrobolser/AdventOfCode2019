using System;
using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day1
{
    public static class Part1
    {
        public static int GetResult()
        {
            var modulesMass = Array.ConvertAll(File.ReadAllLines("Inputs/Day1.txt"), int.Parse);

            var result = 0;
            foreach(var mass in modulesMass)
                result += (mass / 3) - 2;

            return result;
        }
    }
}
