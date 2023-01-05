using System.Text;

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
            for (int x = 0; x < w; x++)            
                grid[x, y] = int.Parse(_input[y][x].ToString());         

        int p = findpath(w, h, grid);

        return new(p.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int w = _input[0].Length;
        int h = _input.Length;
        int[,] grid = new int[w * 5, h * 5];
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                int v = int.Parse(_input[y][x].ToString());
                int newV;
                for (int yV = 0; yV <= 4; yV++)
                {
                    newV = v;
                    for (int i = 0; i < yV; i++)
                    {
                        newV++;
                        if (newV == 10) newV = 1;
                    }
                    for (int xV = 0; xV <= 4; xV++)
                    {                                                
                        grid[x + xV * w, y + yV * h] = newV;
                        newV++;
                        if (newV == 10) newV = 1;
                    }                    
                }
            }
        }

        //StringBuilder sb = new StringBuilder();
        //for (int y = 0; y < h * 5; y++)
        //{
        //    for (int x = 0; x < w * 5; x++) sb.Append(grid[x, y]);
        //    Console.Out.WriteLine(sb.ToString());
        //    sb.Clear();
        //}

        int p = findpath(w * 5, h * 5, grid);

        return new(p.ToString());
    }

    private static int findpath(int w, int h, int[,] grid)
    {
        int p;
        PriorityQueue<(int, int), int> queue = new PriorityQueue<(int, int), int>();
        Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();
        (int X, int Y) end = (w - 1, h - 1);
        (int X, int Y) cur;
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

        return p;
    }    
}
