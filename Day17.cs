using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2024
{
    public class Day17 : Day
    {
        int AStart;
        int BStart;
        int CStart;
        long A;
        int B;
        int C;
        List<int> Instructions;
        public Day17(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            List<int> list = this.ParseListOfInteger(Input);
            AStart = list[0];
            BStart = list[1];
            CStart = list[2];
            Instructions = list.GetRange(3, list.Count - 3);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            List<int> ReturnValue = RunProgram(AStart);

            StringBuilder out1 = new StringBuilder();
            foreach (int i in ReturnValue)
            {
                out1.Append(i);
                out1.Append(',');
            }
            return out1.ToString();
        }
        public string GetPartTwo()
        {
            List<int> ReturnValue = new List<int>();
            long i = 0;
            while (true)
            {
                i++;
                ReturnValue = new List<int>(RunProgram(i));
                if (ReturnValue.Count == Instructions.Count)
                    for (int j = 0; j < Instructions.Count; j++)
                    {
                        if (ReturnValue[j] != Instructions[j])
                            break;
                        if (j == Instructions.Count - 1)
                            return i.ToString();
                    }
            }

            return i.ToString();
        }
        public List<int> RunProgram(long a)
        {
            List<int> ReturnValue = new List<int>();
            int pointer = 0;
            A = a;
            B = BStart;
            C = CStart;
            while (pointer >= 0 && pointer < Instructions.Count)
            {
                int operand = Instructions[pointer + 1];
                bool jump = false;

                switch (Instructions[pointer])
                {
                    case 0: //adv
                        A = A / ((int)Math.Pow(2, Combo(operand)));
                        break;
                    case 1:
                        B = B ^ operand;
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
                        int i = Combo(operand) % 8;
                        ReturnValue.Add(i);
                        break;
                    case 6:
                        B = (int)A / ((int)Math.Pow(2, Combo(operand)));
                        break;
                    case 7:
                        C = (int)A / ((int)Math.Pow(2, Combo(operand)));
                        break;
                    default:
                        break;

                }
                if (!jump)
                    pointer += 2;
            }
            return ReturnValue;

        }
        public int Combo(int value)
        {
            int ReturnValue = value;
            switch (value)
            {
                case 4:
                    ReturnValue = (int)A;
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
