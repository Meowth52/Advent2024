using System;
using System.Collections.Generic;

namespace Advent2024
{
    public class Day09 : Day
    {
        Dictionary<int, int> Disk;
        int TopOne;
        public Day09(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input).TrimEnd();
            Disk = new Dictionary<int, int>();
            TopOne = Input.Length - 1;
            for (int i = 0; i < Input.Length; i++)
            {
                Disk[i] = Int32.Parse(Input[i].ToString());
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            long ReturnValue = 0;
            long position = 0;
            int topper = TopOne;
            for (int i = 0; i < TopOne; i++)
            {
                if (i % 2 == 1)
                {
                    long staph = position + Disk[i];
                    for (int p = 0; p < Disk[i]; p++)
                    {
                        for (int t = topper; t == 0; t -= 2)
                        {
                            if (Disk[t] > 0)
                            {
                                Disk[t]--;
                                ReturnValue += (t * (position + p)) / 2;
                                if (ReturnValue >= 1928)
                                    ;
                                break;
                            }
                            else
                                topper -= 2;
                        }
                    }
                    position += Disk[i];
                }
                else
                {
                    int number = Disk[i];
                    for (long p = 0; p < number; p++)
                    {
                        ReturnValue += (position + p) * i / 2;
                        Disk[i]--;
                    }

                    if (ReturnValue >= 1928)
                        ;
                    position += Disk[i];
                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
    }
}
