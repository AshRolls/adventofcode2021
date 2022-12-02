using AoCHelper;
using System.Collections.Immutable;
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

    public override ValueTask<string> Solve_2()
    {        
        int total = 0;
        foreach (string s in _input)
        {
            string[] outputs = s.Remove(0, 61).Split(' ');
            string[] inputs = s.Remove(58, s.Length - 58).Split(' ');
            total += getTotal(inputs, outputs);
        }
      
        return new(total.ToString());
    }

    private int getTotal(string[] inputs, string[] outputs)
    {
        string[] digits = new string[10];
        digits[0] = String.Empty;
        digits[1] = inputs.Where(s => s.Length == 2).First().ToString();
        digits[2] = String.Empty;
        digits[3] = String.Empty;
        digits[4] = inputs.Where(s => s.Length == 4).First().ToString();
        digits[5] = String.Empty;
        digits[6] = String.Empty;
        digits[7] = inputs.Where(s => s.Length == 3).First().ToString();
        digits[8] = inputs.Where(s => s.Length == 7).First().ToString();
        digits[9] = String.Empty;

        string[] fives = inputs.Where(s => s.Length == 5).ToArray();
        string[] sixes = inputs.Where(s => s.Length == 6).ToArray();

        string diff = digits[4];
        foreach (char c in digits[1]) 
        {
            diff = diff.Replace(c.ToString(), String.Empty);
        }

        foreach(string s in fives)
        {
            int count = 0;
            foreach (char c in digits[1])
            {
                if (s.Contains(c)) count++;
            }
            int diffCnt = 0;
            foreach (char c in diff)
            {
                if (s.Contains(c)) diffCnt++;
            }

            if (count == 2)
            {
                digits[3] = s;
            }
            else if (diffCnt == 2)
            {
                digits[5] = s;
            }
            else
            {
                digits[2] = s;
            }
        }

        foreach (string s in sixes)
        {
            int fourCnt = 0;
            foreach (char c in digits[4])
            {
                if (s.Contains(c)) fourCnt++;
            }
            int diffCnt = 0;
            foreach (char c in diff)
            {
                if (s.Contains(c)) diffCnt++;
            }

            if (fourCnt == 4)
            {
                digits[9] = s;
            }
            else if (diffCnt == 2)
            {
                digits[6] = s;
            }
            else
            {
                digits[0] = s;
            }
        }

        string output = String.Empty;
        foreach (string o in outputs)
        {
            for (int i = 0; i < 10; i++)
            {
                if (!digits[i].Except(o).Any() && !o.Except(digits[i]).Any())
                {
                    output += i.ToString();
                    break;
                } 
            }
        }

        return Int32.Parse(output);
    }
   
}
