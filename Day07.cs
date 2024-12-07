using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2024
{
    public class Day07 : Day
    {
        List<List<long>> Instructions;
        public Day07(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseListOfLongerLists(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            long ReturnValue = 0;
            foreach (List<long> equation in Instructions)
            {
                List<long> Sums = new List<long>();
                Sums.Add(equation[1]);
                bool Break = false;
                for (int i = 2; i < equation.Count; i++)
                {
                    if (Sums.Count == 0)
                        break;
                    List<long> NextSums = new List<long>();
                    foreach (long sum in Sums)
                    {
                        long plus = sum + equation[i];
                        if (plus == equation[0] && i == equation.Count - 1)
                        {
                            ReturnValue += equation[0];
                            Break = true;
                            break;
                        }
                        long mul = sum * equation[i];
                        if (mul == equation[0] && i == equation.Count - 1)
                        {
                            ReturnValue += equation[0];
                            Break = true;
                            break;
                        }

                        if (plus <= equation[0])
                            NextSums.Add(plus);
                        if (mul <= equation[0])
                            NextSums.Add(mul);
                    }
                    Sums = new List<long>(NextSums);

                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            foreach (List<long> equation in Instructions)
            {
                List<long> Sums = new List<long>();
                Sums.Add(equation[1]);
                bool Break = false;
                for (int i = 2; i < equation.Count; i++)
                {
                    if (Sums.Count == 0)
                        break;
                    List<long> NextSums = new List<long>();
                    foreach (long sum in Sums)
                    {
                        long plus = sum + equation[i];
                        if (plus == equation[0] && i == equation.Count - 1)
                        {
                            ReturnValue += equation[0];
                            Break = true;
                            break;
                        }
                        long mul = sum * equation[i];
                        if (mul == equation[0] && i == equation.Count - 1)
                        {
                            ReturnValue += equation[0];
                            Break = true;
                            break;
                        }
                        long fuu = sum*(long)Math.Pow(10,(long)Math.Log10( equation[i])+1)+ equation[i];
                        if (fuu == equation[0] && i == equation.Count - 1)
                        {
                            ReturnValue += equation[0];
                            Break = true;
                            break;
                        }
                        if (plus <= equation[0])
                            NextSums.Add(plus);
                        if (mul <= equation[0])
                            NextSums.Add(mul);
                        if (fuu <= equation[0])
                            NextSums.Add(fuu);
                    }
                    Sums = new List<long>(NextSums);

                }
            }
            return ReturnValue.ToString();
        }
    }
}
