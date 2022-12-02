using AoCHelper;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

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
        for (int i = 0; i < 10; i++)
        {            
            digits[i] = sortStr(digits[i]);
        }

        for(int j = 0; j < outputs.Length; j++)
        {
            string o = sortStr(outputs[j]);
            for (int i = 0; i < 10; i++)
            {
                if (digits[i] == o)
                {
                    output += i.ToString();
                    break;
                } 
            }
        }

        return Int32.Parse(output);
    }

    private string sortStr(string toSort)
    {
        char[] chars = toSort.ToArray();
        Array.Sort(chars);
        return new string(chars);
    }
   
}
