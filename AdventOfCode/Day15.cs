namespace AdventOfCode;

public class Day15 : BaseDay
{
    private readonly string[] _input;    

    public Day15()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        int w = _input[0].Length;
        int h = _input.Length;
        int[,] grid = new int[w, h];
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                grid[x, y] = int.Parse(_input[y][x].ToString());
            }
        }

        PriorityQueue<(int, int), int> queue = new PriorityQueue<(int, int), int>();        
        Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();
        (int X, int Y) end = (w - 1, h - 1);
        (int X, int Y) cur;
        int p;
        int vP;
        queue.Enqueue((0, 0), 0);
        while (queue.TryDequeue(out cur, out p))
        {            
            if (cur == end) break;
            
            if (visited.TryGetValue(cur, out vP))
            {
                if (p >= vP) continue;
                else visited[cur] = p;
            }           
            else visited.Add(cur, p);

            if (cur.Y - 1 >= 0) queue.Enqueue((cur.X, cur.Y - 1), p + grid[cur.X, cur.Y - 1]);
            if (cur.Y + 1 <= h - 1) queue.Enqueue((cur.X, cur.Y + 1), p + grid[cur.X, cur.Y + 1]);
            if (cur.X - 1 >= 0) queue.Enqueue((cur.X - 1, cur.Y), p + grid[cur.X - 1, cur.Y]);
            if (cur.X + 1 <= w - 1) queue.Enqueue((cur.X + 1, cur.Y), p + grid[cur.X + 1, cur.Y]);
        }

        return new(p.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }
}
