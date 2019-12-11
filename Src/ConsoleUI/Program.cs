using System;
using static System.Console;

namespace AdventOfCode2019.ConsoleUI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("*-*-*-*-*-*-*- Advent of Code 2019 -*-*-*-*-*-*-*");
            WriteLine("--- Day 1: The Tyranny of the Rocket Equation ---");
            WriteLine("                --- Part One ---                 ");
            WriteLine($"Sum of the fuel requirement (Modules): {PuzzleAnswers.Day1.Part1.GetResult()}");
            WriteLine("                --- Part Two ---                 ");
            WriteLine($"Sum of the fuel requirement (Modules + Fuel): {PuzzleAnswers.Day1.Part2.GetResult()}");
            WriteLine("--- Day 2: 1202 Program Alarm ---");
            WriteLine("                --- Part One ---                 ");
            WriteLine($"Value left at position [0]: {PuzzleAnswers.Day2.Part1.GetResult()}");
            WriteLine("                --- Part Two ---                 ");
            var day2part2 = PuzzleAnswers.Day2.Part2.GetResult();
            WriteLine($"100 * noun({day2part2.Item1}) + verb({day2part2.Item2}): {100 * day2part2.Item1 + day2part2.Item2}");
            WriteLine("--- Day 3: Crossed Wires ---");
            WriteLine("                --- Part One ---                 ");
            WriteLine($"Manhattan distance from central port to closest intersection: {PuzzleAnswers.Day3.Part1.GetResult()}");
            WriteLine("                --- Part Two ---                 ");
            WriteLine($"Fewest combined steps to reach intersection: {PuzzleAnswers.Day3.Part2.GetResult()}");
        }
    }
}
