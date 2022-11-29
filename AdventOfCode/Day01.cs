namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;
    private readonly int[] _inputInt;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
         _inputInt = new int[_input.Length];
        for (int i = 0; i<_input.Length; i++)
        {
            Int32.TryParse(_input[i], out _inputInt[i]);
        }
    }

    public override ValueTask<string> Solve_1()
    {

        int last = _inputInt[0];        
        int count = 0;

        for (int i=1; i<_input.Length;i++)
        {
            int current = _inputInt[i];            
            if (current > last) count++;
            last = current;            
        }

        return new(count.ToString());
    }

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
}
