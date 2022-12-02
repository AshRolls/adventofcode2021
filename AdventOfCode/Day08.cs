using AoCHelper;
using System.ComponentModel;
using System.Diagnostics;

namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly string[] _input;
    private readonly string _allChars;

    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath);
        _allChars = "abcdefg";
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
        //bool[] zero = new bool[7] {true,true,true,false,true,true,true};
        //bool[] one = new bool[7] {false,false,true,false,false,true,false};

        // missing value from 1 to 7 is A
        // 3 is the 5 letter value that contains 1
        // missing value from 4 to 9 (with A) is G
        // the missing values from the 6 letter values is D or E

        // common value in 1 and 5 is F
        // missing value from 8 to 6 is A
        // missing value from 8 to 0 is D
        // missing value from 8 to 9 is E
        

        //ABCDEFG
        //0123456

        foreach (string s in _input)
        {
            string output = s.Remove(0, 60);
            string input = s.Remove(58, s.Length - 58);
            string[] inputs = input.Split(' ');
            string[] solved = new string[7];
            string[] possible = new string[7];            
            string[] notPossible = new string[7];
            for (int i = 0; i < 7; i++)
            {
                possible[i] = String.Empty;
                notPossible[i] = String.Empty;
            }

            // 1
            string one = inputs.Where(s => s.Length == 2).First().ToString();
            addNotPossible(ref solved, ref notPossible, ref possible, 0, one);
            addNotPossible(ref solved, ref notPossible, ref possible, 1, one);
            addPossible(ref solved, ref possible, ref notPossible, 2, one);
            addNotPossible(ref solved, ref notPossible, ref possible, 3, one);
            addNotPossible(ref solved, ref notPossible, ref possible, 4, one);
            addPossible(ref solved, ref possible, ref notPossible, 5, one);
            addNotPossible(ref solved, ref notPossible, ref possible, 6, one);

            // 7 
            string seven = inputs.Where(s => s.Length == 3).First().ToString();
            addPossible(ref solved, ref possible, ref notPossible, 0, seven);
            addNotPossible(ref solved, ref notPossible, ref possible, 1, seven);
            addPossible(ref solved, ref possible, ref notPossible, 2, seven);
            addNotPossible(ref solved, ref notPossible, ref possible, 3, seven);
            addNotPossible(ref solved, ref notPossible, ref possible, 4, seven);
            addPossible(ref solved, ref possible, ref notPossible, 5, seven);
            addNotPossible(ref solved, ref notPossible, ref possible, 6, seven);            

            // 4
            string four = inputs.Where(s => s.Length == 4).First().ToString();
            addNotPossible(ref solved, ref notPossible, ref possible, 0, four);
            addPossible(ref solved, ref possible, ref notPossible, 1, four);
            addPossible(ref solved, ref possible, ref notPossible, 2, four);
            addPossible(ref solved, ref possible, ref notPossible, 3, four);
            addNotPossible(ref solved, ref notPossible, ref possible, 4, four);
            addPossible(ref solved, ref possible, ref notPossible, 5, four);
            addNotPossible(ref solved, ref notPossible, ref possible, 6, four);

            // 8
            string eight = inputs.Where(s => s.Length == 7).First().ToString();
            addPossible(ref solved, ref possible, ref notPossible, 0, eight);
            addPossible(ref solved, ref possible, ref notPossible, 1, eight);
            addPossible(ref solved, ref possible, ref notPossible, 2, eight);
            addPossible(ref solved, ref possible, ref notPossible, 3, eight);
            addPossible(ref solved, ref possible, ref notPossible, 4, eight);
            addPossible(ref solved, ref possible, ref notPossible, 5, eight);
            addPossible(ref solved, ref possible, ref notPossible, 6, eight);


            //if (inp.Length == 2) // 1
            //{
            //    possible[2] += inp;
            //    possible[5] += inp;
            //}
            //else if (inp.Length == 3) // 7
            //{
            //    possible[0] += inp;
            //    possible[2] += inp;
            //    possible[5] += inp;
            //}
            //else if (inp.Length == 4) // 4
            //{
            //    possible[1] += inp;
            //    possible[2] += inp;
            //    possible[3] += inp;
            //    possible[5] += inp;
            //}
            //else if (inp.Length == 7) // 8
            //{
            //    possible[0] += inp;
            //    possible[1] += inp;
            //    possible[2] += inp;
            //    possible[3] += inp;
            //    possible[4] += inp; 
            //    possible[5] += inp;
            //    possible[6] += inp;                    
            //}


            //string[] result = solvePossibles(possible);
        }

        return new("Not Solved");
    }

    private void addPossible(ref string[] solved, ref string[] possible, ref string[] notPossible, int idx, string str)
    {
        List<string> pList = new List<string>();

        foreach (char c in str)
        {
            if (!notPossible[idx].Contains(c))
            {
                pList.Add(c.ToString());
            }
        }

        if (pList.Count == 1) // solved
        {
            string val = pList.First();
            solve(solved, notPossible, idx, val);

        }
        else if (pList.Count > 1) 
        {
            foreach (string s in pList)
            {
                foreach (char c in s)
                {
                    if (!possible[idx].Contains(c)) possible[idx] += s;
                }
            }
        }
    }

    private void solve(string[] solved, string[] notPossible, int idx, string val)
    {
        solved[idx] = val;
        for (int i = 0; i < 7; i++)
        {
            if (i != idx)
            {
                if (!notPossible[i].Contains(val)) notPossible[i] += val;
            }
            else
            {                                
                notPossible[i] = _allChars.Replace(val, String.Empty);
            }
        }
    }

    private void addNotPossible(ref string[] solved, ref string[] notPossible, ref string[] possible, int idx, string str)
    {
        foreach (char c in str)
        {
            if (!notPossible[idx].Contains(c)) notPossible[idx] += c;
            if (possible[idx].Contains(c))
            {
                possible[idx] = possible[idx].Replace(c.ToString(), String.Empty);
            }              
        }
        if (notPossible[idx].Length >= 6)
        {
            // solved
            string val = _allChars;
            foreach(char c in notPossible[idx])
            {
                val = val.Replace(c.ToString(), String.Empty);
            }           
            solve(solved,notPossible,idx,val); 
        }
    }

    //private string[] solvePossibles(string[] possible)
    //{
    //    string[] result = new string[7];
    //    // remove duplicates
    //    //for (int i = 0; i<possible.Length; i++)
    //    //{
    //    //    string s = possible[i];
    //    //    string s2 = String.Empty;
    //    //    foreach (char c in s)
    //    //    {
    //    //        if (s2.Contains(c)) continue;
    //    //        s2 += c;
    //    //    }
    //    //    result[i] = s2;
    //    //}

    //    return result;
    //}
}
