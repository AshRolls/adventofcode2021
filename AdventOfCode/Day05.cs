namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string[] _input; 
    private int[,] _floor;
 
    public Day05()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        _floor = new int[1000, 1000];
        for (int i=0; i<1000; i++)
        {
            for (int j=0; j<1000;j++)
            {
                _floor[i, j] = 0;
            }
        }

        foreach (string s in _input)
        {
            addLine(s);
        }

        int n = checkOverlaps();

        return new(n.ToString());
    }   

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }

    private struct Point
    {
        public int x;
        public int y;
    }

    private void addLine(string line)
    {
        line = line.Replace(" ->", string.Empty);
        var vals = line.Split(' ');
        
        Point first;
        var f = vals[0].Split(',');
        first.x = int.Parse(f[0]);
        first.y = int.Parse(f[1]);

        Point second;
        var s = vals[1].Split(",");
        second.x = int.Parse(s[0]);
        second.y = int.Parse(s[1]);
       
        // vertical
        if (first.x == second.x)
        {
            if (first.y > second.y)
            {
                addToFloorVertical(second, first);
            }
            else
            {
                addToFloorVertical(first, second);
            }
        }
        else if (first.y == second.y) // horizontal 
        {
            if (first.x > second.x)
            {
                addToFloorHorizontal(second, first);
            }
            else
            {
                addToFloorHorizontal(first, second);
            }
        }
    }

    private void addToFloorVertical(Point start, Point end)
    {            
        for (int i = start.y; i <= end.y; i++)
        {
            _floor[start.x, i]++;
        }
    }

    private void addToFloorHorizontal(Point start, Point end)
    {                
        for (int i = start.x; i <= end.x; i++)
        {
            _floor[i, start.y]++;
        }
    }

    private int checkOverlaps()
    {
        int overlaps = 0;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                if (_floor[i, j] > 1) overlaps++;
            }
        }

        return overlaps;
    }
}
