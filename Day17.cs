using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2024
{
    public class Day17 : Day
    {
        long A;
        long B;
        long C;
        List<int> Instructions;
        public Day17(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            List<int> list = this.ParseListOfInteger(Input);
            A = list[0];
            B = list[1];
            C = list[2];
            Instructions = list.GetRange(3, list.Count - 3);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            List<long> ReturnValue = new List<long>();
            int pointer = 0;
            while (pointer >= 0 && pointer < Instructions.Count)
            {
                if (A < 0 || B < 0 || C < 0)
                    ;
                int operand = Instructions[pointer + 1];
                bool jump = false;

                switch (Instructions[pointer])
                {
                    case 0: //adv
                        A = A / ((long)Math.Pow(2, Combo(operand)));
                        break;
                    case 1:
                        B = B | operand;
                        break;
                    case 2:
                        B = Combo(operand) % 8;
                        break;
                    case 3:
                        if (A != 0)
                        {
                            pointer = operand;
                            jump = true;
                        }
                        break;
                    case 4:
                        B = B ^ C;
                        break;
                    case 5:
                        long i = Combo(operand) % 8;
                        ReturnValue.Add(i);
                        break;
                    case 6:
                        B = A / ((long)Math.Pow(2, Combo(operand)));
                        break;
                    case 7:
                        C = A / ((long)Math.Pow(2, Combo(operand)));
                        break;
                    default:
                        break;

                }
                if (!jump)
                    pointer += 2;
            }

            StringBuilder out1 = new StringBuilder();
            foreach (long i in ReturnValue)
            {
                out1.Append(i);
                out1.Append(',');
            }
            //not 7,1,4,3,5,5,2,7,7
            return out1.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
        public long Combo(long value)
        {
            long ReturnValue = value;
            switch (value)
            {
                case 4:
                    ReturnValue = A;
                    break;
                case 5:
                    ReturnValue = B;
                    break;
                case 6:
                    ReturnValue = C;
                    break;
                case 7:
                    throw new Exception();
                    break;
                default:
                    break;
            }
            return ReturnValue;
        }
    }
}
