using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2024
{
    public class Day11 : Day
    {
        List<long> Instructions;
        public Day11(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseListOfLong(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            List<long> Stones = new List<long>(Instructions);
            for (int i = 0; i < 25; i++)
            {
                List<long> next = new List<long>();
                foreach (long stone in Stones)
                {
                    if (stone == 0)
                        next.Add(1);
                    else if ((int)(Math.Log10(stone) + 1) % 2 == 0)
                    {
                        long numbers = (long)(Math.Log10(stone) + 1);
                        next.Add(stone / (long)Math.Pow(10, numbers / 2));
                        next.Add(stone % (long)Math.Pow(10, numbers / 2));
                    }
                    else if (stone < 0)
                        throw new Exception(); //overflowcheck because for some reason i dont want to default to long all the time
                    else
                        next.Add(stone * 2024);
                }
                Stones = new List<long>(next);

            }
            ReturnValue = Stones.Count;
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            Dictionary<long, long> Stones = new Dictionary<long, long>();
            foreach (int i in Instructions)
            {
                if (!Stones.ContainsKey(i))
                    Stones.Add(i, 0);
                Stones[i]++;
            }
            for (int i = 0; i < 75; i++)
            {
                Dictionary<long, long> next = new Dictionary<long, long>();
                foreach (KeyValuePair<long, long> stone in Stones)
                {
                    long number = 0;
                    long otherNumber = -1;
                    if (stone.Key == 0)
                        number = 1;
                    else if ((int)(Math.Log10(stone.Key) + 1) % 2 == 0)
                    {
                        long numbers = (long)(Math.Log10(stone.Key) + 1);
                        number = stone.Key / (long)Math.Pow(10, numbers / 2);
                        otherNumber = stone.Key % (long)Math.Pow(10, numbers / 2);
                    }
                    else if (stone.Key < 0)
                        throw new Exception(); //overflowcheck because for some reason i dont want to default to long all the time
                    else
                        number = stone.Key * 2024;
                    if (!next.ContainsKey(number))
                        next.Add(number, 0);
                    next[number] += stone.Value;
                    if (otherNumber >= 0)
                    {
                        if (!next.ContainsKey(otherNumber))
                            next.Add(otherNumber, 0);
                        next[otherNumber] += stone.Value;
                    }
                }
                Stones = new Dictionary<long, long>(next);

            }
            ReturnValue = (long)Stones.Values.Sum();
            return ReturnValue.ToString();
        }
    }
}
