using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2024
{
    public class Day01 : Day
    {
        List<int> Left;
        List<int> Right;
        public Day01(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input); 
            List<int> instructions;
            instructions = this.ParseListOfInteger(Input);
            Left = new List<int>();
            Right = new List<int>();
            for(int i = 0; i < instructions.Count;i++)
            {
                if (i % 2 == 0)
                {
                    Left.Add(instructions[i]);
                }
                else
                {
                    Right.Add(instructions[i]);
                }
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            Left.Sort();
            Right.Sort();
            int ReturnValue = 0;
            for(int i = 0; i<Left.Count;i++)
            {
                ReturnValue += Math.Abs(Left[i] - Right[i]);
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            for (int i = 0; i < Left.Count(); i++)
            {
                ReturnValue += Right.FindAll(x=> x== Left[i]).Count * Left[i];
            }

            return ReturnValue.ToString();
        }
    }
}
