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
        return new("Not Solved");
    }
}
