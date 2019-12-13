using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day4
{
    public static class Part2
    {
        public static int GetResult()
        {
            var range = File.ReadAllText("Inputs/Day4.txt").Split('-');
            var min = int.Parse(range[0]);
            var max = int.Parse(range[1]);
            var result = 0;
            double first, second, third, identical;
            bool isIdentical;

            for (int password = min; password <= max; password++)
            {
                first = 0;
                second = 0;
                identical = 0;
                isIdentical = false;

                int length = password.ToString().Length;
                for (int digit = 0; digit < length; digit++)
                {
                    third = char.GetNumericValue(password.ToString()[digit]);
                    if(second == third && !isIdentical)
                    {
                        isIdentical = true;
                        identical = third;
                    }

                    if(first == second && second == third && second == identical)
                    {
                        isIdentical = false;
                    }

                    if (second > third)
                        break;

                    first = second;
                    second = third;

                    if (digit == (length - 1) && isIdentical)
                        result++;
                }
            }

            return result;
        }
    }
}
