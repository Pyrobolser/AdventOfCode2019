using System;
using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day5
{
    public static class Part1
    {
        internal class IntcodeComputer
        {
            public int[] Memory { get; }

            public bool IsOver { get; set; }

            public int OutputValue { get; set; }

            public IntcodeComputer(int[] memory)
            {
                Memory = memory;
                IsOver = false;
                OutputValue = -1;
            }

            public int Compute()
            {
                int pointer;
                for (int index = 0; index < Memory.Length; index += pointer)
                {
                    pointer = ComputeInstruction(index, GetParameters(index));

                    if (IsOver)
                        break;
                }

                return OutputValue;
            }

            private string[] GetParameters(int index)
            {
                string instruction = Memory[index].ToString();
                string[] parameters;
                string opcode;

                if(instruction.Length == 1)
                {
                    opcode = string.Concat("0", instruction[^1].ToString());
                }
                else
                {
                    opcode = instruction[^2..^0];
                }

                switch(opcode)
                {
                    case "01":
                    case "02":
                        parameters = new string[4] { opcode, "0", "0", "0" };
                        break;
                    case "03":
                    case "04":
                        parameters = new string[2] { opcode, "0" };
                        break;
                    default:
                        parameters = new string[1] { opcode };
                        break;
                }

                // Parameters mode
                for (int j = 1; j < parameters.Length; j++)
                {
                    if (instruction.Length > j + 1)
                    {
                        parameters[j] = instruction[^((j + 1) + 1)].ToString();
                    }
                }

                return parameters;
            }

            private int ComputeInstruction(int index, string[] instructions)
            {
                switch(instructions[0])
                {
                    case "01":
                        Add(index, instructions[1..]);
                        break;
                    case "02":
                        Mult(index, instructions[1..]);
                        break;
                    case "03":
                        Input(index, instructions[1..]);
                        break;
                    case "04":
                        Output(index, instructions[1..]);
                        break;
                    case "99":
                        IsOver = true;
                        break;
                }

                return instructions.Length;
            }

            private void Add(int index, string[] instructions)
            {
                var first = instructions[0] == "0" ? Memory[Memory[index + 1]] : Memory[index + 1];
                var second = instructions[1] == "0" ? Memory[Memory[index + 2]] : Memory[index + 2];

                if (instructions[2] == "0")
                {
                    Memory[Memory[index + 3]] = first + second;
                }
                else if (instructions[2] == "1")
                {
                    Memory[index + 3] = first + second;
                }
            }

            private void Mult(int index, string[] instructions)
            {
                var first = instructions[0] == "0" ? Memory[Memory[index + 1]] : Memory[index + 1];
                var second = instructions[1] == "0" ? Memory[Memory[index + 2]] : Memory[index + 2];

                if (instructions[2] == "0")
                {
                    Memory[Memory[index + 3]] = first * second;
                }
                else if (instructions[2] == "1")
                {
                    Memory[index + 3] = first * second;
                }
            }

            private void Input(int index, string[] instructions)
            {
                Console.Write("ID of the system to test: ");
                if (instructions[0] == "0")
                {
                    Memory[Memory[index + 1]] = int.Parse(Console.ReadLine());
                }
                else if (instructions[0] == "1")
                {
                    Memory[index + 1] = int.Parse(Console.ReadLine());
                }
            }

            private void Output(int index, string[] instructions)
            {
                if (instructions[0] == "0")
                {
                    OutputValue = Memory[Memory[index + 1]];
                }
                else if (instructions[0] == "1")
                {
                    OutputValue = Memory[index + 1];
                }
                Console.WriteLine($"Output: {OutputValue}");
            }
        }

        public static int GetResult()
        {
            int[] input = Array.ConvertAll(File.ReadAllText("Inputs/Day5.txt").Split(','), int.Parse);

            var computer = new IntcodeComputer(input);

            var output = computer.Compute();

            return output;
        }
    }
}
