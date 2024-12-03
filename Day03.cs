using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2024
{
    public class Day03 : Day
    {
        string Input;
        public Day03(string _input) : base(_input)
        {
            Input = this.CheckFile(_input);//.TrimEnd('\r', '\n');
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            List<(int, int)> Instructions = ParseMul(Input);
            int ReturnValue = 0;
            foreach((int, int) instruction in Instructions)
            {
                ReturnValue += instruction.Item1 * instruction.Item2; 
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            string Dodont = "do()" + Input+ "don't()";
            MatchCollection Matches = Regex.Matches(Dodont, @"(?<=do\(\)).*?(?=don't\(\))",RegexOptions.Singleline);
            foreach (Match m in Matches)
            {
                List<(int, int)> subSet = ParseMul(m.Value);
                foreach ((int, int) instruction in subSet)
                {
                    ReturnValue += instruction.Item1 * instruction.Item2;
                }
            }
            return ReturnValue.ToString();
        }
        List<(int, int)> ParseMul(string input)
        {
            List<(int, int)> Parsed = new List<(int, int)>();
            MatchCollection Matches = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)");
            foreach (Match m in Matches)
            {
                Parsed.Add((Int32.Parse(m.Groups[1].Value), Int32.Parse(m.Groups[2].Value)));
            }
            return Parsed;
        }
    }
}
