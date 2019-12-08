using System;

namespace AdventOfCode2019.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*-*-*-*-*-*-*- Advent of Code 2019 -*-*-*-*-*-*-*");
            Console.WriteLine("--- Day 1: The Tyranny of the Rocket Equation ---");
            Console.WriteLine("                --- Part One ---                 ");
            Console.WriteLine($"Sum of the fuel requirement (Modules): {PuzzleAnswers.Day1.Part1.GetResult()}");
            Console.WriteLine("                --- Part Two ---                 ");
            Console.WriteLine($"Sum of the fuel requirement (Modules + Fuel): {PuzzleAnswers.Day1.Part2.GetResult()}");
        }
    }
}
