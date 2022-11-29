namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;    

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);         
    }

    public override ValueTask<string> Solve_1()
    {
        int h = 0;
        int d = 0;

        foreach (string s in _input)
        {
            int v = (int)Char.GetNumericValue(s[s.Length - 1]);
            
            if (s[0] == 'f')
            {
                h += v;
            }
            else if (s[0] == 'd')
            {
                d += v;
            }
            else if (s[0] == 'u')
            {
                d -= v;
            }
        }

        int res = h * d;
        return new(res.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int h = 0;
        int d = 0;
        int a = 0;

        foreach (string s in _input)
        {
            int v = (int)Char.GetNumericValue(s[s.Length - 1]);

            if (s[0] == 'f')
            {
                h += v;
                d += (v * a);
            }
            else if (s[0] == 'd')
            {
                a += v;
            }
            else if (s[0] == 'u')
            {
                a -= v;
            }
        }

        int res = h * d;
        return new(res.ToString()); ;
    }
}
