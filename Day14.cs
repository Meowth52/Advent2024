using System;
using System.Collections.Generic;

namespace Advent2024
{
    public class Day14 : Day
    {
        List<List<int>> Instructions;
        public int Xmax = 101;
        public int Ymax = 103;
        public Day14(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseListOfIntegerLists(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 1;
            List<Robot> Robots = new List<Robot>();
            int[] quadrants = new int[5];
            foreach (List<int> bot in Instructions)
                Robots.Add(new Robot(bot, Xmax, Ymax));
            foreach (Robot robot in Robots)
            {
                robot.MoveNSteps(100);
                quadrants[robot.GetQuadrant()]++;
            }
            for (int i = 0; i < 4; i++)
                ReturnValue *= quadrants[i];
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            List<Robot> Robots = new List<Robot>();
            Coordinate upRight = new Coordinate(1, 1);
            foreach (List<int> bot in Instructions)
                Robots.Add(new Robot(bot, Xmax, Ymax));
            while (true)
            {
                HashSet<Coordinate> others = new HashSet<Coordinate>();
                ReturnValue++;
                foreach (Robot robot in Robots)
                {
                    robot.MoveNSteps(1);
                    others.Add(robot.Position);
                }
                foreach (Coordinate robot in others)
                {
                    int likelyhood = 0;
                    Coordinate maybe = robot.GetSum(upRight);
                    while (others.Contains(maybe))
                    {
                        likelyhood++;
                        if (likelyhood > 6)
                            return ReturnValue.ToString();
                        maybe = maybe.GetSum(upRight);
                    }

                }
            }
            return ReturnValue.ToString();
        }
    }
    public class Robot
    {
        public Coordinate Position;
        Coordinate Vektor;
        Coordinate Bounds;
        public Robot(List<int> parse, int xmax, int ymax)
        {
            Position = new Coordinate(parse[0], parse[1]);
            Vektor = new Coordinate(parse[2], parse[3]);
            Bounds = new Coordinate(xmax, ymax);
        }
        public void MoveNSteps(int n)
        {
            int x = ((Vektor.x * n) + Position.x) % Bounds.x;
            if (x < 0)
                x = (x + Bounds.x);
            Position.x = x;
            int y = ((Vektor.y * n) + Position.y) % Bounds.y;
            if (y < 0)
                y = (y + Bounds.y);
            Position.y = y;
        }
        public int GetQuadrant()
        {
            int q = 0;
            if (Position.x == Bounds.x / 2 || Position.y == Bounds.y / 2)
                return 4;
            if (Position.x > Bounds.x / 2)
                q += 1;
            if (Position.y > Bounds.y / 2)
                q += 2;
            return q;
        }
    }
}
