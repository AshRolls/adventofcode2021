﻿using Spectre.Console;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace AdventOfCode;

public class Day12 : BaseDay
{
    private readonly string[] _input;
    private Dictionary<string, Cave> _caves; 

    public Day12()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
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

        Cave cur = _caves["start"];
        List<Cave> visited = new List<Cave>();        
        List<List<Cave>> paths = new List<List<Cave>>();
        
        moveToCave(cur, visited, paths);        
        
        return new(paths.Count().ToString());
    }

    private void moveToCave(Cave cur, List<Cave> visited, List<List<Cave>> paths)
    {                
        List<Cave> clonedVisited = visited.ToList();
        Stack<Cave> options = new Stack<Cave>();
        clonedVisited.Add(cur);

        if (cur == _caves["end"])
        {
            paths.Add(clonedVisited);
        }
        else
        {
            foreach (Cave c in cur.Links)
            {
                if (c.IsBigCave)
                    options.Push(c);
                else if (!clonedVisited.Contains(c))
                    options.Push(c);
            }
            while (options.Any())
            {
                //string p = String.Empty;
                //foreach (Cave c in clonedVisited)
                //{
                //    p += c.Name + "-";
                //}
                //Console.Out.WriteLine(p);
                moveToCave(options.Pop(), clonedVisited, paths);
            }
        }       
    }

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
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

}