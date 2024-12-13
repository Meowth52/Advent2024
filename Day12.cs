using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2024
{
    public class Day12 : Day
    {
        Dictionary<Coordinate, char> Map = new Dictionary<Coordinate, char>();
        List<Area> AreaList;
        public Day12(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Map = this.ParseCoordinateCharDic(Input);
            AreaList = new List<Area>();
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            Dictionary<Coordinate, char> mapCopy = new Dictionary<Coordinate, char>(Map);
            while (mapCopy.Count > 0)
            {
                KeyValuePair<Coordinate, char> place = mapCopy.First();
                Area area = new Area(place.Key, place.Value);
                AreaList.Add(area);
                Queue<Coordinate> coordinateQueue = new Queue<Coordinate>();
                coordinateQueue.Enqueue(place.Key);
                while (coordinateQueue.Count > 0)
                {
                    Coordinate coordinate = coordinateQueue.Dequeue();
                    mapCopy.Remove(coordinate);
                    List<Coordinate> neighobours = coordinate.GetNeihbours();
                    foreach (Coordinate neigbour in neighobours)
                    {
                        if (mapCopy.ContainsKey(neigbour) && mapCopy[neigbour] == area.Type)
                        {
                            area.AddCoordinate(neigbour);
                            if (!coordinateQueue.Contains(neigbour))
                                coordinateQueue.Enqueue(neigbour);
                        }
                        else
                        {
                            ;
                        }
                    }
                }
                foreach (Coordinate c in area.IncludedCoordinates)
                    foreach (Coordinate neighbours in c.GetNeihbours())
                        if (!area.IncludedCoordinates.Contains(neighbours))
                        {
                            area.Perimiter++;
                            area.ExludedCoordinates.Add(neighbours);
                        }
            }
            foreach (Area area in AreaList)
            {
                ReturnValue += area.Perimiter * area.IncludedCoordinates.Count;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            foreach (Area area in AreaList)
            {
                int sides = 0;
                int inner = 0;
                int outer = 0;
                foreach (Coordinate ex in area.ExludedCoordinates)
                {
                    bool corner = false;
                    int corners = 0;
                    List<Coordinate> straightNeighbours = ex.GetNeihbours();
                    List<Coordinate> lessNeighbours = new List<Coordinate>();
                    foreach (Coordinate neighbour in straightNeighbours)
                    {
                        if (area.IncludedCoordinates.Contains(neighbour))
                        {
                            lessNeighbours.Add(neighbour);
                        }
                    }
                    if (lessNeighbours.Count <= 1)
                        ;
                    else if (lessNeighbours.Count == 3)
                    {
                        corners = 2;
                    }
                    else if (lessNeighbours.Count == 4)
                    {
                        corners = 4;
                    }
                    else if ( lessNeighbours.Count==2)
                    {
                        foreach (Coordinate one in lessNeighbours)
                        {
                            foreach (Coordinate other in lessNeighbours)
                            {
                                if (Math.Abs(one.x - other.x) == 1 && Math.Abs(one.y - other.y) == 1)
                                {
                                    corner = true;
                                    corners=1;
                                    break;
                                }
                            }
                            if (corner)
                                break;
                        }
                    }
                    inner += corners;
                }
                foreach (Coordinate inc in area.IncludedCoordinates)
                {
                    List<Coordinate> queerNeighbours = inc.GetNeihbours(Diagonals: true);
                    List<Coordinate> straightNeighbours = inc.GetNeihbours();
                    foreach (Coordinate neighbour in queerNeighbours)
                    {
                        if(!area.IncludedCoordinates.Contains(neighbour)&& !straightNeighbours.Contains(neighbour))
                        {
                            if(!(area.IncludedCoordinates.Contains(new Coordinate(inc.x, neighbour.y)) || area.IncludedCoordinates.Contains(new Coordinate(neighbour.x, inc.y))))
                                outer++;
                        }
                    }

                }
                sides = inner  + outer;
                ReturnValue += (sides) * area.IncludedCoordinates.Count;
            }
            return ReturnValue.ToString();
        }
    }
    public class Area
    {
        public char Type;
        public Coordinate Corner1;
        public Coordinate Corner2;
        public HashSet<Coordinate> IncludedCoordinates;
        public HashSet<Coordinate> ExludedCoordinates;
        public int Perimiter;
        public Area(Coordinate First, char type)
        {
            Corner1 = new Coordinate(First);
            Corner2 = new Coordinate(First);
            IncludedCoordinates = new HashSet<Coordinate>();
            IncludedCoordinates.Add(First);
            ExludedCoordinates = new HashSet<Coordinate>();
            Type = type;
            Perimiter = 0;
        }
        public void AddCoordinate(Coordinate c)
        {
            IncludedCoordinates.Add((Coordinate)c);
            if (c.x < Corner1.x)
                Corner1.x = c.x;
            if (c.y < Corner1.y)
                Corner1.y = c.y;
            if (c.x > Corner2.x)
                Corner2.x = c.x;
            if (c.y > Corner2.y)
                Corner2.y = c.y;
        }
    }
}
