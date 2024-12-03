using System;
using System.Collections.Generic;

namespace Advent2024
{
    public class Cooooordinate : IEquatable<Cooooordinate>, IComparable<Cooooordinate> //schmenum
    {
        public long x;
        public long y;
        public Cooooordinate(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
        public Cooooordinate(Cooooordinate c)
        {
            x = c.x;
            y = c.y;
        }
        public void AddTo(Cooooordinate A)
        {
            x += A.x;
            y += A.y;
        }
        public void MoveNSteps(char c, long n = 1)
        {
            switch (c)
            {
                case 'E':
                case 'R':
                    this.AddTo(new Cooooordinate(1 * n, 0));
                    break;
                case 'W':
                case 'L':
                    this.AddTo(new Cooooordinate(-1 * n, 0));
                    break;
                case 'N':
                case 'U':
                    this.AddTo(new Cooooordinate(0, 1 * n));
                    break;
                case 'S':
                case 'D':
                    this.AddTo(new Cooooordinate(0, -1 * n));
                    break;
            }
        }
        public List<Cooooordinate> GetNeihbours(bool Diagonals = false)
        {
            List<Cooooordinate> ReturnList = new List<Cooooordinate>();
            ReturnList.Add(this.GetSum(new Cooooordinate(1, 0)));
            ReturnList.Add(this.GetSum(new Cooooordinate(-1, 0)));
            ReturnList.Add(this.GetSum(new Cooooordinate(0, 1)));
            ReturnList.Add(this.GetSum(new Cooooordinate(0, -1)));
            if (Diagonals)
            {
                ReturnList.Add(this.GetSum(new Cooooordinate(1, 1)));
                ReturnList.Add(this.GetSum(new Cooooordinate(-1, -1)));
                ReturnList.Add(this.GetSum(new Cooooordinate(-1, 1)));
                ReturnList.Add(this.GetSum(new Cooooordinate(1, -1)));
            }
            return ReturnList;
        }
        public Cooooordinate GetSum(Cooooordinate A)
        {
            long x2 = x + A.x;
            long y2 = y + A.y;
            return new Cooooordinate(x2, y2);
        }
        public bool IsInPositiveBounds(long x2, long y2)
        {
            return (x >= 0 && y >= 0 && x <= x2 && y <= y2);
        }
        public long ManhattanDistance(Cooooordinate coo)
        {
            return Math.Abs(this.x - coo.x) + Math.Abs(this.y - coo.y);
        }
        public Cooooordinate RelativePosition(Cooooordinate coo)
        {
            return new Cooooordinate(this.x - coo.x, this.y - coo.y);
        }
        public bool IsBetween(Cooooordinate first, Cooooordinate second)
        {
            List<long> xs = new List<long>() { first.x, second.x };
            List<long> ys = new List<long>() { first.y, second.y };
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
            long hCode = x ^ y;
            return hCode.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Cooooordinate);
        }
        public bool Equals(Cooooordinate? obj)
        {
            return obj != null && obj.x == x && obj.y == y;
        }
        public int CompareTo(Cooooordinate other)
        {
            if (this.x == other.x)
            {
                return this.y.CompareTo(other.y);
            }
            return this.x.CompareTo(other.x);
        }
        public void Assimilate(Cooooordinate c)
        {
            x = c.x;
            y = c.y;
        }
    }
    class CooooordinateEqualityComparer : IEqualityComparer<Cooooordinate>
    {
        public bool Equals(Cooooordinate? b1, Cooooordinate?  b2)
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

        public int GetHashCode(Cooooordinate bx)
        {
            unchecked // Allow overflow
            {
                long hCode = bx.x * 31 ^ bx.y * 31;
                return hCode.GetHashCode();
            }
        }
    }
}
