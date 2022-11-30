namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;    

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        char[] gamma = new char[12];
        char[] epsilon = new char[12];
        int g;
        int e;
        int zeroC;
        int oneC;

        for (int i = 0; i < 12; i++)
        {
            zeroC = 0;
            oneC = 0;
            for (int j = 0; j < _input.Length; j++)
            {
                if (_input[j][i] == '0') { zeroC++; }
                else { oneC++; }
            }
            if (zeroC > oneC)
            {
                gamma[i] = '0';
                epsilon[i] = '1';
            }
            else
            {
                gamma[i] = '1';
                epsilon[i] = '0';
            }
        }
        
        g = Convert.ToInt32(new string(gamma),2);
        e = Convert.ToInt32(new string(epsilon),2);

        return new((e*g).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<string> most = new List<string>();
        List<string> least = new List<string>();
        foreach (string s in _input)
        {
            most.Add(s);
            least.Add(s);
        }
        
        int i = 0;
        bool done = false;

        while(!done)
        {
            most = getCommon(most, i, true);
            i++;
            if (most.Count == 1) done = true;
        }

        i = 0;
        done = false;

        while (!done)
        {
            least = getCommon(least, i, false);
            i++;
            if (least.Count == 1) done = true;
        }

        int oxy = Convert.ToInt32(new string(most.First()),2);
        int co2 = Convert.ToInt32(new string(least.First()), 2);

        return new((oxy*co2).ToString());
    }

    private List<string> getCommon(List<string> data, int i, bool most)
    {        
        List<string> zero = new List<string>();
        List<string> one = new List<string>();

        foreach (string s in data)
        {
            if (s[i] == '0') zero.Add(s);
            else { one.Add(s); }
        }

        if (most)
        {
            if (one.Count >= zero.Count)
            {
                return one;
            }
            else
            {
                return zero;
            }
        }
        else
        {
            if (zero.Count <= one.Count)
            {
                return zero;
            }
            else
            {
                return one;
            }
        }        

    }
}
