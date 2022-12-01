namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _input;    

    public Day06()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        string[] strs = _input[0].Split(',');
        List<short> shoal = Array.ConvertAll(strs, s => short.Parse(s)).ToList();

        for (int i = 0; i < 80; i++)
        {
            doDay(shoal);
        }

        return new(shoal.Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }

    private void doDay(List<short> shoal)
    {
        int toAdd = 0;

        for (int i = 0; i < shoal.Count; i++)
        {
            if (shoal[i] == 0) //  breed
            {
                shoal[i] = 6;
                toAdd++;
            }
            else
            {
                shoal[i]--;
            }
        }

        for (int i = 0; i < toAdd; i++)
        {
            shoal.Add(8);
        }
    }

}
