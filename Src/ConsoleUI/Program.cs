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
        }
    }
}
