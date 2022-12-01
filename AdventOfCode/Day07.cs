namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string[] _input;    

    public Day07()
    {
        _input = File.ReadAllLines(InputFilePath);

    }

    public override ValueTask<string> Solve_1()
    {
        string[] strs = _input[0].Split(',');
        int[] positions = Array.ConvertAll(strs, s => int.Parse(s));
        double avg = positions.Average();
        int rndAvg = (int)Math.Round(avg);

        return new(rndAvg.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }
}
