using System.IO;

namespace AdventOfCode2019.PuzzleAnswers.Day4
{
    public static class Part1
    {
        public static int GetResult()
        {
            var range = File.ReadAllText("Inputs/Day4.txt").Split('-');
            var min = int.Parse(range[0]);
            var max = int.Parse(range[1]);
            var result = 0;
            double previous, current;
            bool isIdentical;

            for(int password = min; password <= max; password++)
            {
                previous = 0;
                isIdentical = false;

                int length = password.ToString().Length;
                for (int digit = 0; digit < length; digit++)
                {
                    current = char.GetNumericValue(password.ToString()[digit]);
                    if (previous == current)
                        isIdentical = true;

                    if (previous > current)
                        break;

                    previous = current;

                    if (digit == (length - 1) && isIdentical)
                        result++;
                }
            }

            return result;
        }
    }
}
