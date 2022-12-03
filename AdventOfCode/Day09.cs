using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode;

public class Day09 : BaseDay
{
    private readonly string[] _input;    

    public Day09()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        byte[,] map = new byte[102, 102];

        for (int i = 0; i < 102; i++)
        {
            for (int j = 0; j < 102; j++)
            {
                if (i == 0 || i == 101 || j == 0 || j == 101) map[i, j] = (byte)9;
                else map[i, j] = Byte.Parse(_input[i - 1][j - 1].ToString());                
            }
        }
 
        int sum = 0;

        for (int i = 1; i < 101; i++)
        {
            for (int j = 1; j < 101; j++)
            {
                byte cur = map[i, j];
                byte up = map[i, j - 1];
                byte right = map[i + 1, j];
                byte down = map[i, j + 1];
                byte left = map[i - 1, j];

                if (cur < up && cur < right && cur < down && cur < left) sum += (cur + 1);
            }
        }

        return new(sum.ToString());
    }

    public struct Pos
    {
        public byte x;
        public byte y;       
    }

    public class Basin
    {
        public Dictionary<Tuple<byte,byte>,byte> Points;
        public Basin(byte lowx, byte lowy, byte lowHeight)
        {
            Points = new Dictionary<Tuple<byte, byte>, byte>();
            Points.Add(new Tuple<byte,byte>(lowx,lowy), lowHeight);
        }    
        
        public bool getBasinContainsPos(byte x, byte y)
        {
            return Points.ContainsKey(new Tuple<byte,byte>(x,y));
        }

        public int getSize()
        {
            return Points.Count();
        }
    }

    public override ValueTask<string> Solve_2()
    {
        byte[,] map = new byte[102, 102];

        for (int i = 0; i < 102; i++)
        {
            for (int j = 0; j < 102; j++)
            {
                if (i == 0 || i == 101 || j == 0 || j == 101) map[i, j] = (byte)9;
                else map[i, j] = Byte.Parse(_input[i - 1][j - 1].ToString());
            }
        }

        List<Pos> lowPoints = new List<Pos>();
        for (byte i = 1; i < 101; i++)
        {
            for (byte j = 1; j < 101; j++)
            {
                byte cur = map[i, j];
                byte up = map[i, j - 1];
                byte right = map[i + 1, j];
                byte down = map[i, j + 1];
                byte left = map[i - 1, j];

                if (cur < up && cur < right && cur < down && cur < left) lowPoints.Add(new Pos() { x = i, y = j });
            }
        }

        List<int> sizes = new List<int>();
        List<Basin> basins = new List<Basin>();
        foreach (Pos p in lowPoints)
        {
            Basin b = new Basin(p.x, p.y, map[p.x, p.y]);
            basins.Add(b);

            int pointsAdded = 0;
            int pointsAddedLast;

            do
            {
                pointsAddedLast = pointsAdded;
                byte upH = map[p.x, p.y - 1];
                if (upH != 9 && b.Points.TryAdd(new Tuple<byte,byte>(p.x, (byte)(p.y - 1)),upH))
                {
                    pointsAdded++;
                }
                byte rightH = map[p.x + 1, p.y];
                if (rightH != 9 && b.Points.TryAdd(new Tuple<byte, byte>((byte)(p.x + 1), p.y), rightH))
                {
                    pointsAdded++;
                }
                byte downH = map[p.x, p.y + 1];
                if (downH != 9 && b.Points.TryAdd(new Tuple<byte, byte>(p.x, (byte)(p.y + 1)), downH))
                {
                    pointsAdded++;
                }
                byte leftH = map[p.x - 1, p.y];
                if (leftH != 9 && b.Points.TryAdd(new Tuple<byte, byte>((byte)(p.x - 1), p.y), leftH))
                {
                    pointsAdded++;
                }

            } while (pointsAdded > pointsAddedLast);

            sizes.Add(b.getSize());
        }

        sizes.Sort();
        sizes.Reverse();
        int top3 = sizes.Take(3).Sum();
        return new(top3.ToString());
    }
}
