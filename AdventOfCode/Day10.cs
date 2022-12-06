namespace AdventOfCode;

public class Day10 : BaseDay
{
    private readonly string[] _input;
    private static readonly Dictionary<char, char> _pairs = new Dictionary<char, char>() {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
        { '<', '>' } };

    private static readonly Dictionary<char, int> _scores = new Dictionary<char, int>() {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 } };



    public Day10()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int p = 0;
        foreach(string s in _input)
            p += openChunk(0, 0, new Stack<char>(), s);
        //p += openChunk(0, 0, new Stack<char>(), _input[0]);

        return new(p.ToString());
    }    

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }

    private static int openChunk(int p, int i, Stack<char> openC, string line)
    {
        int points = p;
        char c = line[i];       
        if (_pairs.ContainsKey(c))
        {
            //Console.Out.WriteLine("Open " + c);
            openC.Push(c);
            if (i + 1 < line.Length) points += openChunk(p, i + 1, openC, line);            
        }
        else if (_pairs[openC.Peek()] != c)
        {
            //Console.Out.WriteLine("Fault " + c);
            openC.Pop();            
            points += _scores[c];            
        }        
        else if (i + 1 < line.Length)
        {
            //Console.Out.WriteLine("Close " + c);
            openC.Pop();
            points += openChunk(p, i + 1, openC, line);
        }
        return points;
    }
}
