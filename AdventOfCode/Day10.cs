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

    private static readonly Dictionary<char, int> _scores2 = new Dictionary<char, int>() {
        { '(', 1 },
        { '[', 2 },
        { '{', 3 },
        { '<', 4 } };

    public Day10()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int p = 0;
        foreach (string s in _input)
            p += openChunk(0, 0, new Stack<char>(), s);

        return new(p.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<string> incomplete = new List<string>();
        Stack<char> openC = new Stack<char>();
        foreach (string s in _input)
        {
            if (openChunk(0, 0, new Stack<char>(), s) == 0) incomplete.Add(s);
        }

        List<long> scores = new List<long>();
        foreach (string s in incomplete)
        {
            openC = openChunkStack(0, 0, new Stack<char>(), s);
            scores.Add(calcScore(openC));
        }
        scores.Sort();
        long score = scores[scores.Count / 2];

        return new(score.ToString());
    }

    private long calcScore(Stack<char> openC)
    {
        long score = 0;
        while (openC.Any())
        {
            score *= 5;
            score += _scores2[openC.Pop()];
        }
        return score;
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

    private static Stack<char> openChunkStack(int p, int i, Stack<char> openC, string line)
    {
        char c = line[i];
        if (_pairs.ContainsKey(c))
        {
            //Console.Out.WriteLine("Open " + c);
            openC.Push(c);
            if (i < line.Length - 1) return openChunkStack(p, i + 1, openC, line);            
        }
        else 
        {
            //Console.Out.WriteLine("Close " + c);
            openC.Pop();
            if (i < line.Length - 1) return openChunkStack(p, i + 1, openC, line);
        }
        return openC;
    }
}
