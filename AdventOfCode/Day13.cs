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
        return new("Not Solved");
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
