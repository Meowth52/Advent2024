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
            string Input = this.CheckFile(_input).TrimEnd().TrimStart();
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
            Dictionary<int, int> Diskette = new Dictionary<int, int>(Disk);
            long ReturnValue = 0;
            long position = 0;
            int topper = TopOne;
            for (int i = 0; i < TopOne; i++)
            {
                if (i % 2 == 1)
                {
                    for (int p = 0; p < Diskette[i]; p++)
                    {
                        for (int t = topper; t >= i; t -= 2)
                        {
                            if (Diskette[t] > 0)
                            {
                                Diskette[t]--;
                                ReturnValue += (t * (position)) / 2;
                                position++;
                                break;
                            }
                            else
                                topper -= 2;
                        }
                    }
                }
                else
                {
                    int number = Diskette[i];
                    for (long p = 0; p < number; p++)
                    {
                        ReturnValue += (position) * i / 2;
                        position++;
                        Diskette[i]--;
                    }
                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            Dictionary<int, Space> data = new Dictionary<int, Space>();
            Dictionary<int, Space> spaace = new Dictionary<int, Space>();
            long position = 0;
            for (int i = 0; i <= TopOne; i++)
            {
                if (i % 2 == 1)
                    spaace.Add(i, new Space(i, position, Disk[i]));
                else
                    data.Add(i, new Space(i, position, Disk[i]));
                position += Disk[i];
            }
            for (int i = (data.Count * 2) - 2; i >= 0; i -= 2)
            {
                for (int s = 1; s < i; s += 2)
                {
                    if (data[i].Amount <= spaace[s].Amount)
                    {
                        spaace[s].Amount -= data[i].Amount;
                        data[i].DiskPosition = spaace[s].DiskPosition;
                        spaace[s].DiskPosition += data[i].Amount;
                        break;
                    }
                }
            }
            foreach (Space d in data.Values)
            {
                for (long i = d.DiskPosition; i < d.DiskPosition + d.Amount; i++)
                {
                    ReturnValue += i * d.InstructionIndex / 2;
                }

            }
            return ReturnValue.ToString();
        }
    }
    class Space
    {
        public int InstructionIndex;
        public long DiskPosition;
        public int Amount;
        public Space(int instructionIndex, long diskposition, int amount)
        {
            InstructionIndex = instructionIndex;
            DiskPosition = diskposition;
            Amount = amount;
        }
    }
}