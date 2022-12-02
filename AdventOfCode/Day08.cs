namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly string[] _input;    

    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        int count = 0;

        foreach (string s in _input)
        {
            string output = s.Remove(0, 60);
            foreach (string s2 in output.Split(' '))
            {
                if (s2.Length == 2 || s2.Length == 3 || s2.Length == 4 || s2.Length == 7)
                    count++;
            }
        }

        return new(count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }
}
