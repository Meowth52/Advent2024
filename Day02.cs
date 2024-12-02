using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2024
{
    public class Day02 : Day
    {
        List<List<int>> Instructions;
        public Day02(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseListOfIntegerLists(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach(List<int> report in Instructions)
            {
                if (SafetyCheck(report))
                {
                    ReturnValue++;
                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            foreach (List<int> report in Instructions)
            {
                for(int i = 0; i < report.Count; i++)
                {
                    List<int> reprt = new List<int>(report);
                    reprt.RemoveAt(i);
                    if (SafetyCheck(reprt))
                    {
                        ReturnValue++;
                        break;
                    }
                }
            }
            return ReturnValue.ToString();
        }
        bool SafetyCheck(List<int> report)
        {
            int previos = report[0];
            bool safe = true;
            int theOne = 1;
            if (report[1] < previos)
                theOne = -1;
            for (int i = 1; i < report.Count; i++)
            {
                int diff = (report[i] - previos) * theOne;
                if (diff != 1 && diff != 2 && diff != 3)
                {
                    safe = false;
                    break;
                }
                previos = report[i];
            }
            return safe;
        }
    }
}
