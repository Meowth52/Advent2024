using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;

namespace Advent2024
{
    public class Day04 : Day
    {
        string[] Instructions;
        string xmas = "XMAS";
        Dictionary<Coordinate, char> Map;
        Dictionary<Coordinate, char> Starts;
        Dictionary<Coordinate, char> XCandidates;
        public Day04(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseStringArray(Input);
            Map = new Dictionary<Coordinate, char>();
            Starts = new Dictionary<Coordinate, char>();
            XCandidates = new Dictionary<Coordinate, char>();
            for (int y = 0; y < Instructions.Length; y++)
            {
                for (int x = 0; x < Instructions[0].Length; x++)
                {
                    char c = Instructions[y][x];
                    Map.Add(new Coordinate(x, y), c);
                    if (c == 'X')
                    {
                        Starts.Add(new Coordinate(x, y), c);
                    }
                    if (c == 'A')
                    {
                        XCandidates.Add(new Coordinate(x, y), c);
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
            foreach (KeyValuePair<Coordinate, char> s in Starts)
            {
                ReturnValue += checkForMatches(s.Key, xmas);
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            foreach (Coordinate x in XCandidates.Keys)
                ReturnValue += CheckForXs(x);
            return ReturnValue.ToString();
        }
        int checkForMatches(Coordinate start, string match)
        {
            int returnValue = 0;
            List<Coordinate> neighbours = start.GetNeihbours(Diagonals: true);
            List<Coordinate> directions = new List<Coordinate>();
            foreach (Coordinate n in neighbours)
            {
                if (Map.ContainsKey(n) && Map[n] == xmas[1])
                    directions.Add(n.RelativePosition(start));
            }
            if (directions.Count > 0)
                for (int i = 2; i < match.Length; i++)
                {
                    List<Coordinate> next = new List<Coordinate>();
                    foreach (Coordinate d in directions)
                    {
                        Coordinate c = new Coordinate(start.x + d.x * i, start.y + d.y * i);

                        if (Map.ContainsKey(c) && Map[c] == match[i])
                        {
                            char cc = Map[c];
                            next.Add(d);
                        }
                    }
                    if (next.Count == 0)
                        break;
                    directions = new List<Coordinate>(next);
                    if (i == match.Length - 1)
                        returnValue += directions.Count;

                }

            return returnValue;
        }
        int CheckForXs(Coordinate start)
        {
            Coordinate[] Corners =
            {
                new Coordinate(-1,1),
                new Coordinate(1,1),
                new Coordinate(-1,-1),
                new Coordinate(1,-1)
            };
            int Ms = 0;
            int Ss = 0;
            foreach(Coordinate c in Corners)
            {
                Coordinate test = start.GetSum(c);
                if (!Map.ContainsKey(test))
                    return 0;
                if (Map[test] == 'M')
                    Ms++;
                else if (Map[test] == 'S')
                    Ss++;
            }
            if(Ms == 2 && Ss ==2 && Map[start.GetSum(Corners[0])]!= Map[start.GetSum(Corners[3])])
                return 1;
            return 0;
        }
    }
}
