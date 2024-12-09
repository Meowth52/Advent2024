using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2024
{
    public class Day08 : Day
    {
        Dictionary<char, List<Coordinate>> Map;
        int Xmax;
        int Ymax;
        public Day08(string _input) : base(_input)
        {
            string input = this.CheckFile(_input);
            Map = new Dictionary<char, List<Coordinate>>();
            string Input = input.Replace("\r\n", "_");
            string[] RawInstructions = Input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            Xmax = RawInstructions[0].Length-1;
            Ymax = RawInstructions.Length-1;
            for (int y = 0; y < RawInstructions.Length; y++)
            {
                for (int x = 0; x < RawInstructions[y].Length; x++)
                {
                    if (RawInstructions[y][x] != '.')
                    {
                        if (!Map.ContainsKey(RawInstructions[y][x]))
                            Map.Add(RawInstructions[y][x], new List<Coordinate>());
                        Map[RawInstructions[y][x]].Add(new Coordinate(x, RawInstructions.Length - (y + 1)));
                    }
                }
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            HashSet<Coordinate> resonances = new HashSet<Coordinate>();
            foreach(KeyValuePair<char, List<Coordinate>> nodes in Map)
            {
                foreach(Coordinate node in nodes.Value)
                {
                    foreach (Coordinate other in nodes.Value)
                    {
                        if (node != other)
                        {
                            Coordinate differance = new Coordinate( node.RelativePosition(other));
                            Coordinate resonance = new Coordinate((node.x+ differance.x), (node.y+ differance.y ) );
                            if (resonance.IsInPositiveBounds(Xmax, Ymax))
                                resonances.Add(resonance); 
                        }
                    }

                }
            }
            ReturnValue = resonances.Count;
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            HashSet<Coordinate> resonances = new HashSet<Coordinate>();
            foreach (KeyValuePair<char, List<Coordinate>> nodes in Map)
            {
                foreach (Coordinate node in nodes.Value)
                {
                    foreach (Coordinate other in nodes.Value)
                    {
                        if (node != other)
                        {
                            Coordinate resonance = new Coordinate(other);
                            Coordinate differance = new Coordinate(node.RelativePosition(other));
                            int iterator = 1;
                            while (resonance.IsInPositiveBounds(Xmax, Ymax))
                            {
                                resonances.Add(resonance);
                                resonance = new Coordinate((node.x + iterator*differance.x), (node.y + iterator * differance.y));
                                iterator++;
                            }
                        }
                    }

                }
            }
            ReturnValue = resonances.Count;
            return ReturnValue.ToString();
        }
    }
}
