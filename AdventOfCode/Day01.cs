namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {

        int last;
        Int32.TryParse(_input[0], out last);

        int count = 0;
        for (int i=1; i<_input.Length;i++)
        {
            int current;
            Int32.TryParse(_input[i], out current);
            if (current > last) count++;
            Int32.TryParse(_input[i], out last);
        }

        return new(count.ToString());
    }

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
}
