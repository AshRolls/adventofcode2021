using System.Globalization;

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

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }
}
