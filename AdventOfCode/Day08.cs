using AoCHelper;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly string[] _input;
    private readonly string _allChars;
    Digit[] _digits = new Digit[10];

    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath);
        _allChars = "abcdefg";
        _digits[0] = new Digit { lit = 6, a = true, b = true, c = true, d = false, e = true, f = true, g = true };
        _digits[1] = new Digit { lit = 2, a = false, b = false, c = true, d = false, e = false, f = true, g = false };
        _digits[2] = new Digit { lit = 5, a = true, b = false, c = true, d = false, e = false, f = true, g = false };
        _digits[3] = new Digit { lit = 5, a = true, b = false, c = true, d = true, e = false, f = true, g = true };
        _digits[4] = new Digit { lit = 4, a = false, b = true, c = true, d = true, e = false, f = true, g = false };
        _digits[5] = new Digit { lit = 5, a = true, b = true, c = false, d = true, e = false, f = true, g = true };
        _digits[6] = new Digit { lit = 6, a = true, b = true, c = false, d = true, e = true, f = true, g = true };
        _digits[7] = new Digit { lit = 3, a = true, b = false, c = true, d = false, e = false, f = true, g = false };
        _digits[8] = new Digit { lit = 7, a = true, b = true, c = true, d = true, e = true, f = true, g = true };
        _digits[9] = new Digit { lit = 6, a = true, b = true, c = true, d = true, e = false, f = true, g = true };
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

    private struct Digit
    {
        public int lit;
        public bool a;
        public bool b;
        public bool c;
        public bool d;
        public bool e;
        public bool f;
        public bool g;        
    }

    private class PossibleDigit
    {
        public PossibleDigit()
        {
            pa = String.Empty;
            pb = String.Empty;
            pc = String.Empty;
            pd = String.Empty;
            pe = String.Empty;
            pf = String.Empty;
            pg = String.Empty;
            na = String.Empty;
            nb = String.Empty;
            nc = String.Empty;
            nd = String.Empty;
            ne = String.Empty;
            nf = String.Empty;
            ng = String.Empty;
        }

        public string pa;
        public string pb;
        public string pc;
        public string pd;
        public string pe;
        public string pf;
        public string pg;
        public string na;
        public string nb;
        public string nc;
        public string nd;
        public string ne;
        public string nf;
        public string ng;
    }


    public override ValueTask<string> Solve_2()
    {        
        int total = 0;
        foreach (string s in _input)
        {
            string[] outputs = s.Remove(0, 60).Split(' ');
            string[] inputs = s.Remove(58, s.Length - 58).Split(' ');
            total += getTotal(inputs, outputs);
        }
      
        return new(total.ToString());
    }

    private int getTotal(string[] inputs, string[] outputs)
    {
        List<string>[] possibles = new List<string>[10];
        PossibleDigit masterPossible = new PossibleDigit();
        for (int i = 0; i < 10; i++) possibles[i] = new List<string>();

        solve(inputs, possibles, masterPossible);
        

        throw new NotImplementedException();
    }
    private void solve(string[] inputs, List<String>[] possibles, PossibleDigit master)
    {
        foreach (string s in inputs)
        {
            for (int i = 0; i < 10; i++)
            {
                if (s.Length == _digits[i].lit)
                {
                    possibles[i].Add(s);
                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (possibles[i].Count == 1)
            {
                updatePossible(_digits[i], possibles[i][0], master);
                
            }
        }
    }

    private void updatePossible(Digit digit, string s, PossibleDigit master)
    {
        if (digit.a) master.pa += getMissingChars(s, master.pa);
        else master.na += addNotPossible(s);

        if (digit.b) master.pb += getMissingChars(s, master.pb);
        else master.nb += addNotPossible(s);

        if (digit.c) master.pc += getMissingChars(s, master.pc);
        else master.nc += addNotPossible(s);

        if (digit.d) master.pd += getMissingChars(s, master.pd);
        else master.nd += addNotPossible(s);

        if (digit.a) master.pe += getMissingChars(s, master.pe);
        else master.ne += addNotPossible(s);

        if (digit.f) master.pf += getMissingChars(s, master.pf);
        else master.nf += addNotPossible(s);

        if (digit.g) master.pg += getMissingChars(s, master.pg);
        else master.ng += addNotPossible(s);

        //if (_digits[i].b) master.b += getMissingChars(possibles[i][0], master.b);
        //if (_digits[i].c) master.c += getMissingChars(possibles[i][0], master.c);
        //if (_digits[i].d) master.d += getMissingChars(possibles[i][0], master.d);
        //if (_digits[i].e) master.e += getMissingChars(possibles[i][0], master.e);
        //if (_digits[i].f) master.f += getMissingChars(possibles[i][0], master.f);
        //if (_digits[i].g) master.g += getMissingChars(possibles[i][0], master.g);
    }

    private string addNotPossible(string s)
    {
        string newS = String.Empty;
        foreach (char c in s)
        {
            newS += _allChars.Replace(c.ToString(), String.Empty);
        }
        return newS;
    }

    private string getMissingChars(string s, string filter)
    {
        string newS = String.Empty;
        foreach(char c in s)
        {
            if (!filter.Contains(c)) newS += c;
        }
        return newS;
    }

}
