using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day7
{
    public static class Part2
    {
        internal class IntcodeComputer
        {
            public int[] Memory { get; set; }

            public string[] Instructions { get; private set; }

            public bool IsOver { get; private set; }

            public int OutputValue { get; private set; }

            public int Pointer { get; private set; }

            public string Opcode { get; private set; }

            public bool IsPhaseSetting { get; private set;  }

            public int PhaseSetting { get; }

            public int InputSignal { get; }

            public IntcodeComputer(int[] memory, int phaseSetting, int inputSignal)
            {
                Memory = memory;
                IsOver = false;
                OutputValue = -1;
                Pointer = 0;
                IsPhaseSetting = true;
                PhaseSetting = phaseSetting;
                InputSignal = inputSignal;
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
                int input;
                if(IsPhaseSetting)
                {
                    input = PhaseSetting;
                    Console.WriteLine($"Phase Setting : {input}");
                    IsPhaseSetting = false;
                }
                else
                {
                    input = InputSignal;
                    Console.WriteLine($"Input Signal: {input}");
                }

                if (Instructions[0] == "0")
                {
                    Memory[Memory[Pointer + 1]] = input;
                }
                else if (Instructions[0] == "1")
                {
                    Memory[Pointer + 1] = input;
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

            public static IEnumerable<int[]> GetPhaseSettings(int[] phaseSettings)
            {
                int temp;
                var c = new int[phaseSettings.Length];

                yield return phaseSettings;

                var i = 0;
                while (i < phaseSettings.Length)
                {
                    if (c[i] < i)
                    {
                        if (i % 2 == 0)
                        {
                            temp = phaseSettings[0];
                            phaseSettings[0] = phaseSettings[i];
                            phaseSettings[i] = temp;
                        }
                        else
                        {
                            temp = phaseSettings[c[i]];
                            phaseSettings[c[i]] = phaseSettings[i];
                            phaseSettings[i] = temp;
                        }
                        yield return phaseSettings;

                        c[i]++;
                        i = 0;
                    }
                    else
                    {
                        c[i] = 0;
                        i++;
                    }
                }
            }
        }

        public static int GetResult()
        {
            int[] input = Array.ConvertAll(File.ReadAllText("Inputs/Day7.txt").Split(','), int.Parse);

            int[] phaseSettings = new int[] { 5, 6, 7, 8, 9 };
            int result = 0, output;
            IntcodeComputer ampA, ampB, ampC, ampD, ampE;

            foreach (var phaseSetting in IntcodeComputer.GetPhaseSettings(phaseSettings))
            {
                ampA = new IntcodeComputer(input, phaseSetting[0], 0);
                output = ampA.Compute();
                ampB = new IntcodeComputer(input, phaseSetting[1], output);
                output = ampB.Compute();
                ampC = new IntcodeComputer(input, phaseSetting[2], output);
                output = ampC.Compute();
                ampD = new IntcodeComputer(input, phaseSetting[3], output);
                output = ampD.Compute();
                ampE = new IntcodeComputer(input, phaseSetting[4], output);
                output = ampE.Compute();

                if (output > result)
                    result = output;
            }

            return result;
        }
    }
}
