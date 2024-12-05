using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace Advent2024
{
    public class Day05 : Day
    {
        List<List<int>> Ordering;
        List<List<int>> Production;
        List<Dictionary<int, int>> BadProduction;
        int PartOne;

        public Day05(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input).Replace("\r\n\r\n", "_");
            string[] split = Input.Split('_');
            Ordering = this.ParseListOfIntegerLists(split[0]);
            Production = this.ParseListOfIntegerLists(split[1]);
            BadProduction = new List<Dictionary<int, int>>();
            PartOne = 0;
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            List<Dictionary<int, int>> HashProduction = new List<Dictionary<int, int>>();
            foreach (List<int> list in Production)
            {
                Dictionary<int, int> hashis = new Dictionary<int, int>();
                for (int i = 0; i < list.Count; i++)
                    hashis.Add(list[i], i);
                HashProduction.Add(hashis);
            }
            foreach (Dictionary<int, int> hashis in HashProduction)
            {
                bool yep = true;
                foreach (List<int> order in Ordering)
                {
                    if (hashis.ContainsKey(order[0]) && hashis.ContainsKey(order[1]))
                    {
                        if (hashis[order[0]] > hashis[order[1]])
                        {
                            yep = false;
                            break;
                        }
                        if (!yep)
                            break;
                    }
                }
                if (yep)
                {
                    ReturnValue += hashis.First(x => x.Value == (hashis.Count / 2)).Key;
                }
                else
                {
                    BadProduction.Add(hashis);
                }
            }
            PartOne = ReturnValue;
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            List<Dictionary<int, int>> BetterProduction = new List<Dictionary<int, int>>();
            bool yep = true;
            while (yep)
            {
                yep = false;
                foreach (Dictionary<int, int> bad in BadProduction)
                {
                    foreach (List<int> order in Ordering)
                    {
                        if (bad.ContainsKey(order[0]) && bad.ContainsKey(order[1]))
                        {
                            if (bad[order[0]] > bad[order[1]])
                            {
                                int hold = bad[order[0]];
                                bad[order[0]] = bad[order[1]];
                                bad[order[1]] = hold;
                                yep = true;
                            }
                        }
                    }

                }
            }
            foreach (var better in BadProduction)
            {
                ReturnValue += better.First(x => x.Value == (better.Count / 2)).Key;
            }
            return ReturnValue.ToString();
        }
    }
}
