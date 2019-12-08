using System;
using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day1
{
    public static class Part2
    {
        public static int GetResult()
        {
            var modulesMass = Array.ConvertAll(File.ReadAllLines("Inputs/Day1.txt"), int.Parse);

            var result = 0;
            var tempFuel = 0;
            foreach (var mass in modulesMass)
            {
                tempFuel = (mass / 3) - 2;
                while(tempFuel > 0)
                {
                    result += tempFuel;
                    tempFuel = (tempFuel / 3) - 2;
                }
            }
            return result;
        }
    }
}
