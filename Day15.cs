using System;
using System.Collections.Generic;

namespace Advent2024
{
    public class Day15 : Day
    {
        Dictionary<Coordinate, char> Map;
        string Instructions;
        int YMax;
        int XMax;
        public Day15(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input).TrimEnd().TrimStart();
            string[] split = Input.Split("\r\n\r\n");
            Map = this.ParseCoordinateCharDic(split[0]);
            Instructions = split[1].Replace("\r\n", "");
            YMax = 0;
            foreach (Coordinate c in Map.Keys)
            {
                if (c.y > YMax)
                    YMax = c.y;//bloody bacwards scoring
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            Dictionary<Coordinate, char> map = new Dictionary<Coordinate, char>(Map);
            Coordinate player = null;
            foreach (KeyValuePair<Coordinate, char> place in map)
                if (place.Value == '@')
                {
                    player = new Coordinate(place.Key);
                    break;
                }
            foreach (char c in Instructions)
            {
                int n = 1;
                while (true)
                {
                    Coordinate IsThisIt = player.LookAheadNSteps(c, n);
                    if (map[IsThisIt] == '#')
                        break;
                    if (map[IsThisIt] != 'O')
                    {
                        map[player] = '.';
                        player.MoveNSteps(c);
                        if (!player.Equals(IsThisIt))
                        {
                            map[IsThisIt] = 'O';
                        }
                        break;
                    }
                    n++;
                }
            }
            map[player] = '.';
            foreach (KeyValuePair<Coordinate, char> place in map)
            {
                if (place.Value == 'O')
                {
                    ReturnValue += place.Key.x + 100 * (YMax - place.Key.y);
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
