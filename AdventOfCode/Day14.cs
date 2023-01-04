using Spectre.Console;
using System.Diagnostics.Metrics;

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
        Element first, cur;        
        parseElements(out first, out cur);
        
        Dictionary<(char, char), char> templates;
        parseTemplates(out templates);

        int steps = 10;
        Dictionary<char, long> counts;
        doSteps(first, out cur, templates, steps, out counts);

        long result = counts.Values.Max() - counts.Values.Min();

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Dictionary<(char, char), char> templates;
        parseTemplates(out templates);

        Dictionary <(char,char),long> pairCounts = new Dictionary<(char, char), long>();
        for (int i = 0; i < _input[0].Length - 1; i++)
        {
            if (!pairCounts.ContainsKey((_input[0][i], _input[0][i + 1]))) pairCounts.Add((_input[0][i], _input[0][i + 1]), 0);
            pairCounts[(_input[0][i], _input[0][i + 1])]++;
        }

        Dictionary<(char, char), long> toAdd;
        for (int i = 0; i < 40; i++)
        {
            toAdd = new Dictionary<(char, char), long>();
            foreach(KeyValuePair<(char, char), long> kvp in pairCounts)
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

            foreach(KeyValuePair<(char, char), long> kvp in toAdd)
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

        return new(result.ToString());
    }

    private static void doSteps(Element first, out Element cur, Dictionary<(char, char), char> templates, int steps, out Dictionary<char, long> counts)
    {
        for (int i = 0; i < steps; i++)
        {
            cur = first;
            char c;
            while (cur.Next != null)
            {
                Element next = cur.Next;
                if (templates.TryGetValue((cur.E, cur.Next.E), out c))
                {
                    Element e = new Element(c);
                    e.Next = cur.Next;
                    cur.Next = e;
                }
                cur = next;
            }
        }

        counts = new Dictionary<char, long>();
        cur = first;
        while (cur.Next != null)
        {
            if (!counts.ContainsKey(cur.E)) counts.Add(cur.E, 0);
            counts[cur.E]++;
            cur = cur.Next;
        }
        counts[cur.E]++;
    }

    private void parseElements(out Element first, out Element cur)
    {
        first = new Element(_input[0][0]);
        cur = first;
        for (int i = 1; i < _input[0].Length; i++)
        {
            Element e = new Element(_input[0][i]);
            cur.Next = e;
            cur = e;
        }       
    }

    private void parseTemplates(out Dictionary<(char, char), char> templates)
    {
        templates = new Dictionary<(char, char), char>();
        foreach (string line in _input.Skip(2))
        {
            templates.Add((line[0], line[1]), line[6]);
        }
    }

    public class Element
    {
        public char E;
        public Element Next;

        public Element(char e)
        {
            E = e;            
        }
    }
  
}
