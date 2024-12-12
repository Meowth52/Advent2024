using System;
using System.Collections.Generic;

namespace Advent2024
{
    public class Day12 : Day
    {
        Dictionary<Coordinate, char> Map = new Dictionary<Coordinate, char>();
        public Day12(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Map = this.ParseCoordinateCharDic(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
    }
    public class Area
    {
        Coordinate Corner1;
        Coordinate Corner2;
        HashSet<Coordinate> IncludedCoordinates;
        HashSet<Coordinate> ExludedCoordinates;
        public Area(Coordinate First)
        {
            Corner1 = First;
            Corner2 = First;
            IncludedCoordinates = new HashSet<Coordinate>();
            IncludedCoordinates.Add(First);
            ExludedCoordinates = new HashSet<Coordinate>();
        }
    }
}
