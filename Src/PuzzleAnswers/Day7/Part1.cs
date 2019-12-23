using System;
using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day7
{
    public static class Part1
    {
        internal class IntcodeComputer
        {
            public int[] Memory { get; }

            public string[] Instructions { get; private set; }

            public bool IsOver { get; private set; }

            public int OutputValue { get; private set; }

            public int Pointer { get; private set; }

            public string Opcode { get; private set; }

            public IntcodeComputer(int[] memory)
            {
                Memory = memory;
                IsOver = false;
                OutputValue = -1;
                Pointer = 0;
            }

            public int Compute()
            {
                while (!IsOver)
                {
                    GetParameters();
                    ComputeInstruction();
                }

                return OutputValue;
            }

            private void GetParameters()
            {
                string current = Memory[Pointer].ToString();

                if (current.Length == 1)
                {
                    Opcode = string.Concat("0", current[^1].ToString());
                }
                else
                {
                    Opcode = current[^2..^0];
                }

                switch (Opcode)
                {
                    case "01":
                    case "02":
                    case "07":
                    case "08":
                        Instructions = new string[3] { "0", "0", "0" };
                        break;
                    case "03":
                    case "04":
                        Instructions = new string[1] { "0" };
                        break;
                    case "05":
                    case "06":
                        Instructions = new string[2] { "0", "0" };
                        break;
                    default:
                        return;
                }

                // Parameters mode
                for (int j = 0; j < Instructions.Length; j++)
                {
                    if (current.Length > j + 2)
                    {
                        Instructions[j] = current[^((j + 2) + 1)].ToString();
                    }
                }
            }

            private void ComputeInstruction()
            {
                switch (Opcode)
                {
                    case "01":
                        Add();
                        break;
                    case "02":
                        Mult();
                        break;
                    case "03":
                        Input();
                        break;
                    case "04":
                        Output();
                        break;
                    case "05":
                        JumpIfTrue();
                        break;
                    case "06":
                        JumpIfFalse();
                        break;
                    case "07":
                        LessThan();
                        break;
                    case "08":
                        Equals();
                        break;
                    case "99":
                        IsOver = true;
                        break;
                }
            }

            private void Add()
            {
                var first = Instructions[0] == "0" ? Memory[Memory[Pointer + 1]] : Memory[Pointer + 1];
                var second = Instructions[1] == "0" ? Memory[Memory[Pointer + 2]] : Memory[Pointer + 2];

                if (Instructions[2] == "0")
                {
                    Memory[Memory[Pointer + 3]] = first + second;
                }
                else if (Instructions[2] == "1")
                {
                    Memory[Pointer + 3] = first + second;
                }

                Pointer += Instructions.Length + 1;
            }

            private void Mult()
            {
                var first = Instructions[0] == "0" ? Memory[Memory[Pointer + 1]] : Memory[Pointer + 1];
                var second = Instructions[1] == "0" ? Memory[Memory[Pointer + 2]] : Memory[Pointer + 2];

                if (Instructions[2] == "0")
                {
                    Memory[Memory[Pointer + 3]] = first * second;
                }
                else if (Instructions[2] == "1")
                {
                    Memory[Pointer + 3] = first * second;
                }

                Pointer += Instructions.Length + 1;
            }

            private void Input()
            {
                Console.Write("Input: ");
                if (Instructions[0] == "0")
                {
                    Memory[Memory[Pointer + 1]] = int.Parse(Console.ReadLine());
                }
                else if (Instructions[0] == "1")
                {
                    Memory[Pointer + 1] = int.Parse(Console.ReadLine());
                }

                Pointer += Instructions.Length + 1;
            }

            private void Output()
            {
                if (Instructions[0] == "0")
                {
                    OutputValue = Memory[Memory[Pointer + 1]];
                }
                else if (Instructions[0] == "1")
                {
                    OutputValue = Memory[Pointer + 1];
                }
                Console.WriteLine($"Output: {OutputValue}");

                Pointer += Instructions.Length + 1;
            }

            private void JumpIfTrue()
            {
                var first = Instructions[0] == "0" ? Memory[Memory[Pointer + 1]] : Memory[Pointer + 1];

                if (first != 0)
                {

                    Pointer = Instructions[1] == "0" ? Memory[Memory[Pointer + 2]] : Memory[Pointer + 2];
                }
                else
                {
                    Pointer += Instructions.Length + 1;
                }

            }

            private void JumpIfFalse()
            {
                var first = Instructions[0] == "0" ? Memory[Memory[Pointer + 1]] : Memory[Pointer + 1];

                if (first == 0)
                {

                    Pointer = Instructions[1] == "0" ? Memory[Memory[Pointer + 2]] : Memory[Pointer + 2];
                }
                else
                {
                    Pointer += Instructions.Length + 1;
                }
            }

            private void LessThan()
            {
                var first = Instructions[0] == "0" ? Memory[Memory[Pointer + 1]] : Memory[Pointer + 1];
                var second = Instructions[1] == "0" ? Memory[Memory[Pointer + 2]] : Memory[Pointer + 2];

                if (first < second)
                {
                    if (Instructions[2] == "0")
                    {
                        Memory[Memory[Pointer + 3]] = 1;
                    }
                    else if (Instructions[2] == "1")
                    {
                        Memory[Pointer + 3] = 1;
                    }
                }
                else
                {
                    if (Instructions[2] == "0")
                    {
                        Memory[Memory[Pointer + 3]] = 0;
                    }
                    else if (Instructions[2] == "1")
                    {
                        Memory[Pointer + 3] = 0;
                    }
                }

                Pointer += Instructions.Length + 1;
            }

            private void Equals()
            {
                var first = Instructions[0] == "0" ? Memory[Memory[Pointer + 1]] : Memory[Pointer + 1];
                var second = Instructions[1] == "0" ? Memory[Memory[Pointer + 2]] : Memory[Pointer + 2];

                if (first == second)
                {
                    if (Instructions[2] == "0")
                    {
                        Memory[Memory[Pointer + 3]] = 1;
                    }
                    else if (Instructions[2] == "1")
                    {
                        Memory[Pointer + 3] = 1;
                    }
                }
                else
                {
                    if (Instructions[2] == "0")
                    {
                        Memory[Memory[Pointer + 3]] = 0;
                    }
                    else if (Instructions[2] == "1")
                    {
                        Memory[Pointer + 3] = 0;
                    }
                }

                Pointer += Instructions.Length + 1;
            }
        }

        public static int GetResult()
        {
            int[] input = Array.ConvertAll(File.ReadAllText("Inputs/Day7.txt").Split(','), int.Parse);

            input = new int[] { 3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0 };

            var computer = new IntcodeComputer(input);

            var output = computer.Compute();
            
            

            return output;
        }
    }
}
