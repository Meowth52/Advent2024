using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Intrinsics.X86;

namespace Advent2024
{
    public class Day06 : Day
    {
        Dictionary<Coordinate, char> Lab;
        Guard StartNisse;
        int Xmax = 0;
        int Ymax = 0;
        char StartChar;
        HashSet<Coordinate> FirstPath;
        public Day06(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Lab = this.ParseCoordinateCharDic(Input);
            StartNisse = null;
            FirstPath= null;
            foreach (KeyValuePair<Coordinate, char> position in Lab)
            {
                if (position.Key.x > Xmax)
                    Xmax = position.Key.x;
                if (position.Key.y > Ymax)
                    Ymax = position.Key.y;
                if (position.Value != '.' && position.Value != '#')
                {
                    Coordinate c = new Coordinate(position.Key);
                    switch (position.Value)
                    {
                        case '<':
                            StartNisse = new Guard(c, 'W');
                            break;
                        case '^':
                            StartNisse = new Guard(c, 'N');
                            break;
                        case '>':
                            StartNisse = new Guard(c, 'E');
                            break;
                        case 'v':
                            StartNisse = new Guard(c, 'S');
                            break;
                        default:
                            break;
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
            ReturnValue = CountPath(new Dictionary<Coordinate, char>(Lab));
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            foreach(Coordinate position in FirstPath)
            {
                if (Lab[position] == '.')
                {
                    Dictionary<Coordinate, char> ModifiedLab = new Dictionary<Coordinate, char>(Lab);
                    ModifiedLab[position] = '#';
                    if (CountPath(ModifiedLab) < 0)
                        ReturnValue++;
                }
            }
            return ReturnValue.ToString();
        }
        int CountPath(Dictionary<Coordinate, char> Labbb)
        {
            Guard Nisse = new Guard(StartNisse.Position, StartNisse.Direction);
            HashSet<Coordinate> path = new HashSet<Coordinate>();
            path.Add(new Coordinate( Nisse.Position));
            int PathRepeats = 0;
            while (Nisse.Position.IsInPositiveBounds(Xmax, Ymax))
            {
                Nisse.Move();
                if (!Nisse.Position.IsInPositiveBounds(Xmax, Ymax))
                    break;

                if (Labbb[Nisse.Position] == '#')
                    Nisse.Turn();
                if (Labbb[Nisse.Position] == '.' && !path.Contains(Nisse.Position)) 
                {
                    path.Add(new Coordinate(Nisse.Position));
                    PathRepeats = 0;
                }
                if (path.Contains(Nisse.Position))
                {
                    PathRepeats++;
                    if(PathRepeats>=100)
                        return -1;
                }
            }
            if (FirstPath == null)
                FirstPath = path;
            return path.Count;
        }
    }
    public class Guard
    {
        public Coordinate Position;
        public char Direction;
        public Guard(Coordinate position, char direction)
        {
            Position = new Coordinate( position);
            Direction = direction;
        }
        public void Move()
        {
            Position.MoveNSteps(Direction);
        }
        public void Turn()
        {
            Position.MoveNSteps(Direction, -1);
            switch (Direction)
            {
                case 'N':
                    Direction = 'E';
                    break;
                case 'E':
                    Direction = 'S';
                    break;
                case 'S':
                    Direction = 'W';
                    break;
                case 'W':
                    Direction = 'N';
                    break;
                default:
                    break;
            }
        }
    }
}
