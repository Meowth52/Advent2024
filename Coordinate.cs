﻿using System;
using System.Collections.Generic;

namespace Advent2024
{
    public class Coordinate : IEquatable<Coordinate>, IComparable<Coordinate> //schmenum
    {
        public int x;
        public int y;
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Coordinate(Coordinate c)
        {
            x = c.x;
            y = c.y;
        }
        public void AddTo(Coordinate A)
        {
            x += A.x;
            y += A.y;
        }
        public void MoveNSteps(char c, int n = 1)
        {
            switch (c)
            {
                case 'E':
                case 'R':
                case '>':
                    this.AddTo(new Coordinate(1 * n, 0));
                    break;
                case 'W':
                case 'L':
                case '<':
                    this.AddTo(new Coordinate(-1 * n, 0));
                    break;
                case 'N':
                case 'U':
                case '^':
                    this.AddTo(new Coordinate(0, 1 * n));
                    break;
                case 'S':
                case 'D':
                case 'v':
                    this.AddTo(new Coordinate(0, -1 * n));
                    break;
            }
        }
        public Coordinate LookAheadNSteps(char c, int n = 1)
        {
            Coordinate C = new Coordinate(this);
            switch (c)
            {
                case 'E':
                case 'R':
                case '>':
                    C.AddTo(new Coordinate(1 * n, 0));
                    break;
                case 'W':
                case 'L':
                case '<':
                    C.AddTo(new Coordinate(-1 * n, 0));
                    break;
                case 'N':
                case 'U':
                case '^':
                    C.AddTo(new Coordinate(0, 1 * n));
                    break;
                case 'S':
                case 'D':
                case 'v':
                    C.AddTo(new Coordinate(0, -1 * n));
                    break;
            }
            return C;
        }
        public static int Turn(int direction, int where)
        {
            int Direction = (direction + where + 4) % 4;
            return Direction;
        }
        public List<Coordinate> GetNeihbours(bool Diagonals = false)
        {
            List<Coordinate> ReturnList = new List<Coordinate>();
            ReturnList.Add(this.GetSum(new Coordinate(1, 0)));
            ReturnList.Add(this.GetSum(new Coordinate(-1, 0)));
            ReturnList.Add(this.GetSum(new Coordinate(0, 1)));
            ReturnList.Add(this.GetSum(new Coordinate(0, -1)));
            if (Diagonals)
            {
                ReturnList.Add(this.GetSum(new Coordinate(1, 1)));
                ReturnList.Add(this.GetSum(new Coordinate(-1, -1)));
                ReturnList.Add(this.GetSum(new Coordinate(-1, 1)));
                ReturnList.Add(this.GetSum(new Coordinate(1, -1)));
            }
            return ReturnList;
        }
        public Coordinate GetSum(Coordinate A)
        {
            int x2 = x + A.x;
            int y2 = y + A.y;
            return new Coordinate(x2, y2);
        }
        public bool IsInPositiveBounds(int x2, int y2)
        {
            return (x >= 0 && y >= 0 && x <= x2 && y <= y2);
        }
        public bool IsInPositiveBounds(Coordinate c)
        {
            return (x >= 0 && y >= 0 && x <= c.x && y <= c.y);
        }
        public int ManhattanDistance(Coordinate coo)
        {
            return Math.Abs(this.x - coo.x) + Math.Abs(this.y - coo.y);
        }
        public Coordinate RelativePosition(Coordinate coo)
        {
            return new Coordinate(this.x - coo.x, this.y - coo.y);
        }
        public bool IsBetween(Coordinate first, Coordinate second)
        {
            List<int> xs = new List<int>() { first.x, second.x };
            List<int> ys = new List<int>() { first.y, second.y };
            xs.Sort();
            ys.Sort();
            return (this.x >= xs[0] && this.x <= xs[1] && this.y >= ys[0] && this.y <= ys[1]);
        }
        //public Position GetPosition()
        //{
        //    return new Position(x, y);
        //}
        public override string ToString()
        {
            return x.ToString() + "," + y.ToString();
        }

        public override int GetHashCode()
        {
            int hCode = x ^ y;
            return hCode.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            var other = obj as Coordinate;
            return other != null && other.x == x && other.y == y;
        }
        public bool Equals(Coordinate? other)
        {
            return other != null && other.x == x && other.y == y;
        }
        public int CompareTo(Coordinate other)
        {
            if (this.x == other.x)
            {
                return this.y.CompareTo(other.y);
            }
            return this.x.CompareTo(other.x);
        }
        public void Assimilate(Coordinate c)
        {
            x = c.x;
            y = c.y;
        }
    }
    class CoordinateEqualityComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate? b1, Coordinate? b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null | b2 == null)
                return false;
            else if (b1.x == b2.x && b1.y == b2.y)
                return true;
            else
                return false;
        }

        public int GetHashCode(Coordinate bx)
        {
            int hCode = bx.x ^ bx.y;
            return hCode.GetHashCode();
        }
    }
}
