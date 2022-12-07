namespace AdventOfCode;

public class Day13 : BaseDay
{
    private readonly string[] _input;    

    public Day13()
    {
        _input = File.ReadAllLines(InputFilePath);                
    }

    public override ValueTask<string> Solve_1()
    {
        List<Dot> dots = new List<Dot>();
        List<Fold> folds = new List<Fold>();
        parseData(dots, folds);

        Fold f = folds.First();
        doFold(f, ref dots);

        return new(dots.Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<Dot> dots = new List<Dot>();
        List<Fold> folds = new List<Fold>();
        parseData(dots, folds);

        foreach (Fold f in folds)
        {
            doFold(f, ref dots);
        }

        int maxX = dots.Select(x => x.X).Max() + 1;
        int maxY = dots.Select(x => x.Y).Max() + 1;

        bool[,] grid = new bool[maxX, maxY];
        foreach(Dot d in dots)
        {
            grid[d.X, d.Y] = true;
        }

        for (int y = 0; y < maxY; y++)
        {
            string row = String.Empty;
            for (int x = 0; x < maxX; x++)
            {
                if (grid[x, y]) row += '#';
                else row += '.';
            }
            Console.Out.WriteLine(row);
        }

        return new("CONSOLE");
    }

    private void parseData(List<Dot> dots, List<Fold> folds)
    {
        for (int i = 0; i < _input.Length; i++)
        {
            if (_input[i].Length == 0) continue;
            if (!_input[i][0].Equals('f'))
            {
                // dot
                var s = _input[i].Split(',');
                Dot d = new Dot() { X = int.Parse(s[0]), Y = int.Parse(s[1]) };
                dots.Add(d);
            }
            else
            {
                var s = _input[i].Split('=');
                // fold
                if (_input[i][11].Equals('x')) folds.Add(new Fold() { IsX = true, Val = int.Parse(s[1]) });
                else folds.Add(new Fold() { IsX = false, Val = int.Parse(s[1]) }); ;
            }
        }
    }

    private void doFold(Fold f, ref List<Dot> dots)
    {
        for (int i = 0; i < dots.Count(); i++)
        {
            Dot d = dots[i];
            if (f.IsX)
            {
                if (d.X > f.Val)
                {
                    int diff = d.X - f.Val;
                    d.X = f.Val - diff;
                }
            }
            else
            {
                if (d.Y > f.Val)
                {
                    int diff = d.Y - f.Val;
                    d.Y = f.Val - diff;
                }
            }
            dots[i] = d;
        }

        // remove duplicates after fold
        dots = dots.GroupBy(d => new { d.X, d.Y }).Select(d => d.First()).ToList();
    }


    public struct Dot
    {
        public int X;
        public int Y;
    }    

    public struct Fold
    {
        public bool IsX;
        public int Val;
    }
}
