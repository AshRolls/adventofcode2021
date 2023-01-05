namespace AdventOfCode;

public class Day14 : BaseDay
{
    private readonly string[] _input;    

    public Day14()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        Dictionary<(char, char), long> pairCounts = parsePolymer();
        Dictionary<(char, char), char> templates = parseTemplates();

        long result = doSteps(templates, pairCounts, 10);
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Dictionary<(char, char), long> pairCounts = parsePolymer();
        Dictionary<(char, char), char> templates = parseTemplates();
                
        long result = doSteps(templates, pairCounts, 40);
        return new(result.ToString());
    }

    private static long doSteps(Dictionary<(char, char), char> templates, Dictionary<(char, char), long> pairCounts, int steps)
    {
        Dictionary<(char, char), long> toAdd;
        for (int i = 0; i < steps; i++)
        {
            toAdd = new Dictionary<(char, char), long>();
            foreach (KeyValuePair<(char, char), long> kvp in pairCounts)
            {
                if (kvp.Value > 0)
                {
                    if (!toAdd.ContainsKey((kvp.Key.Item1, templates[kvp.Key]))) toAdd.Add((kvp.Key.Item1, templates[kvp.Key]), 0);
                    toAdd[(kvp.Key.Item1, templates[kvp.Key])] += kvp.Value;
                    if (!toAdd.ContainsKey((templates[kvp.Key], kvp.Key.Item2))) toAdd.Add((templates[kvp.Key], kvp.Key.Item2), 0);
                    toAdd[(templates[kvp.Key], kvp.Key.Item2)] += kvp.Value;
                    if (!toAdd.ContainsKey(kvp.Key)) toAdd.Add(kvp.Key, 0);
                    toAdd[kvp.Key] -= kvp.Value;
                }
            }

            foreach (KeyValuePair<(char, char), long> kvp in toAdd)
            {
                if (!pairCounts.ContainsKey(kvp.Key)) pairCounts.Add(kvp.Key, 0);
                pairCounts[kvp.Key] += kvp.Value;
            }
        }

        Dictionary<char, long> counts = new Dictionary<char, long>();
        bool first = true;
        foreach (KeyValuePair<(char, char), long> kvp in pairCounts)
        {
            if (first)
            {
                if (!counts.ContainsKey(kvp.Key.Item1)) counts.Add(kvp.Key.Item1, 0);
                counts[kvp.Key.Item1] += kvp.Value;
                first = false;
            }
            if (!counts.ContainsKey(kvp.Key.Item2)) counts.Add(kvp.Key.Item2, 0);
            counts[kvp.Key.Item2] += kvp.Value;
        }

        long result = counts.Values.Max() - counts.Values.Min();
        return result;
    }

    private Dictionary<(char, char), long> parsePolymer()
    {
        Dictionary<(char, char), long> pairCounts = new Dictionary<(char, char), long>();
        for (int i = 0; i < _input[0].Length - 1; i++)
        {
            if (!pairCounts.ContainsKey((_input[0][i], _input[0][i + 1]))) pairCounts.Add((_input[0][i], _input[0][i + 1]), 0);
            pairCounts[(_input[0][i], _input[0][i + 1])]++;
        }

        return pairCounts;
    }

    private Dictionary<(char, char), char> parseTemplates()
    {
        Dictionary<(char, char), char> templates = new Dictionary<(char, char), char>();
        foreach (string line in _input.Skip(2))
        {
            templates.Add((line[0], line[1]), line[6]);
        }
        return templates;
    }
  
}
