using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2024
{
    public class Day06 : Day
    {
        Dictionary<Coordinate, char> Lab;
        public Day06(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Lab = this.ParseCoordinateCharDic(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            Guard? Nisse = null;
            int Xmax = 0;
            int Ymax = 0;
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
                            Nisse = new Guard(c, 'W');
                            break;
                        case '^':
                            Nisse = new Guard(c, 'N');
                            break;
                        case '>':
                            Nisse = new Guard(c, 'E');
                            break;
                        case 'v':
                            Nisse = new Guard(c, 'S');
                            break;
                        default:
                            break;
                    }
                }
            }
            Lab[Nisse.Position] = 'X';
            ReturnValue++;
            while (Nisse.Position.IsInPositiveBounds(Xmax, Ymax))
            {
                Nisse.Move();
                if (!Nisse.Position.IsInPositiveBounds(Xmax,Ymax))
                    break;

                if (Lab[Nisse.Position] == '#')
                    Nisse.Turn();
                if(Lab[Nisse.Position] == '.')
                {
                    Lab[Nisse.Position] = 'X';
                    ReturnValue++;
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
    public class Guard
    {
        public Coordinate Position;
        char Direction;
        public Guard(Coordinate position, char direction)
        {
            Position = position;
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
