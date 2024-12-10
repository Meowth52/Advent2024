using System;
using System.Collections.Generic;

namespace Advent2024
{
    public class Day10 : Day
    {
        Dictionary<Coordinate, int> Map;
        List<Coordinate> Starts;
        Coordinate Max;
        int Pass;
        public Day10(string _input) : base(_input)
        {
            string input = this.CheckFile(_input);
            string Input = input.Replace("\r\n", "_");
            string[] RawInstructions = Input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            Map = new Dictionary<Coordinate, int>();
            Starts = new List<Coordinate>();
            Max = new Coordinate(RawInstructions[0].Length - 1, RawInstructions.Length - 1);
            Pass = 0;
            for (int y = 0; y < RawInstructions.Length; y++)
            {
                for (int x = 0; x < RawInstructions[y].Length; x++)
                {
                    Map.Add(new Coordinate(x, RawInstructions.Length - (y + 1)), RawInstructions[y][x] - '0');
                    if (RawInstructions[y][x] - '0' == 0)
                        Starts.Add(new Coordinate(x, RawInstructions.Length - (y + 1)));
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
            foreach (Coordinate start in Starts)
            {
                HashSet<Coordinate> Nines = new HashSet<Coordinate>();
                Queue<Coordinate> Paths = new Queue<Coordinate>();
                Paths.Enqueue(start);
                while (Paths.Count > 0)
                {
                    Coordinate current = Paths.Dequeue();
                    List<Coordinate> neighbours = current.GetNeihbours();
                    foreach (Coordinate neighbour in neighbours)
                    {
                        if (neighbour.IsInPositiveBounds(Max) && Map[current] + 1 == Map[neighbour])
                            if (Map[neighbour] == 9)
                            {
                                Nines.Add(neighbour);
                                Pass++;
                            }
                            else
                                Paths.Enqueue(neighbour);
                    }
                }
                ReturnValue += Nines.Count;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = Pass;

            return ReturnValue.ToString();
        }
    }
}
