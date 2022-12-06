using Spectre.Console;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace AdventOfCode;

public class Day12 : BaseDay
{
    private readonly string[] _input;
    private Dictionary<string, Cave> _caves;
    private long _paths;

    public Day12()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        extractLinks();

        Cave cur = _caves["start"];
        _paths = 0;

        Dictionary<string, int> visited = new Dictionary<string, int>();
        foreach (String s in _caves.Keys)
        {
            visited.Add(s, 0);
        }

        moveToCave(cur, visited, 1);

        return new(_paths.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        extractLinks();

        Cave cur = _caves["start"];
        _paths = 0;

        Dictionary<string,int> visited = new Dictionary<string,int>();
        foreach(String s in _caves.Keys)
        {
            visited.Add(s, 0);
        }

        moveToCave(cur, visited, 2);
        
        return new(_paths.ToString());
    }

    private void extractLinks()
    {
        _caves = new Dictionary<string, Cave>();
        foreach (string s in _input)
        {
            var link = s.Split('-');
            if (!_caves.ContainsKey(link[0])) _caves.Add(link[0], new Cave(link[0]));
            if (!_caves.ContainsKey(link[1])) _caves.Add(link[1], new Cave(link[1]));
            _caves[link[0]].Links.Add(_caves[link[1]]);
            _caves[link[1]].Links.Add(_caves[link[0]]);
        }
    }

    private void moveToCave(Cave cur, Dictionary<string,int> visited, int maxSmallCaves)
    {
        Dictionary<string,int> clonedVisited = new Dictionary<string, int>(visited);
        Stack<Cave> options = new Stack<Cave>();
        clonedVisited[cur.Name]++;
        if (!cur.IsBigCave && maxSmallCaves == 2 && clonedVisited[cur.Name] >= maxSmallCaves) maxSmallCaves = 1;

        if (cur == _caves["end"])
        {
            //Console.Out.WriteLine(ToDebugString<string,int>(clonedVisited) + " " + maxSmallCaves);
            _paths++;
        }
        else
        {            
            foreach (Cave c in cur.Links)
            {                
                if (c.IsBigCave)
                    options.Push(c);
                else                 
                {
                    if (clonedVisited[c.Name] <= maxSmallCaves - 1 && c != _caves["start"]) options.Push(c);
                }
            }
            while (options.Any())
            {                                             
                moveToCave(options.Pop(), clonedVisited, maxSmallCaves);
            }
        }
    }

    private class Cave
    {
        public Cave(string name)
        {
            Name = name;
            IsBigCave = Char.IsUpper(name[0]);
            Links = new List<Cave>();
        }

        public string Name { get; }
        public bool IsBigCave { get; }
        public List<Cave> Links { get; set; }
    }

    public string ToDebugString<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
    {
        return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
    }
}



