namespace AdventOfCode;
using System.Diagnostics;

public class Day04 : BaseDay
{
    private readonly string[] _input;
    private List<int> _draw;
    private List<BingoBoard> _boards;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);                
    }


    public override ValueTask<string> Solve_1()
    {
        _draw = parseDraw(_input[0]);
        _boards = parseAllBoards();
            
        // process draw
        foreach (int n in _draw)
        {
            foreach (BingoBoard b in _boards)
            {
                if (b.updateBoard(n))
                {
                    // bingo
                    int unmarkedTotal = b.getUnmarkedTotal();
                    return new ((n*unmarkedTotal).ToString());
                }
            }
        }

        return new("Not Solved");
    }

    public override ValueTask<string> Solve_2()
    {
        _draw = parseDraw(_input[0]);
        _boards = parseAllBoards();

        int c = 0;

        // process draw
        foreach (int n in _draw)
        {
            foreach (BingoBoard b in _boards)
            {                
                if (!b.bingo && b.updateBoard(n))
                {
                    // bingo on this board
                    c++;
                    if (c == _boards.Count)
                    {
                        int unmarkedTotal = b.getUnmarkedTotal();
                        return new((n * unmarkedTotal).ToString());
                    }                        
                }
            }
        }

        return new("Not Solved");
    }

    public List<BingoBoard> parseAllBoards()
    {
        List<BingoBoard> boards = new List<BingoBoard>();
        bool done = false;
        int i = 2;
        while (!done)
        {
            string[] data = new string[5];
            for (int j = 0; j < 5; j++)
            {
                data[j] = _input[i];
                i++;
            }
            BingoBoard b = new BingoBoard(data);
            boards.Add(b);
            if (i >= _input.Length) done = true;
            i++; // for blank line
        }
        return boards;
    }

    public List<int> parseDraw(string s)
    {
        List<int> draw = new List<int>();
        foreach (string str in s.Split(','))
        {
            int val;
            Int32.TryParse(str, out val);
            draw.Add(val);
        }
        return draw;
    }
}

public struct BingoCell
{
    public bool found;
    public int val;
}
public class BingoBoard
{
    BingoCell[,] board = new BingoCell[5,5];
    public bool bingo = false;

    public BingoBoard(string[] data) 
    {
        Debug.Assert(data.Length == 5);
        for (int i=0; i<5;i++)
        {
            int j = 0;
            foreach (string str in data[i].Split(' '))
            {
                if (str.Length > 0) // split will include empty strings if more than one whitespace
                {
                    int val;
                    Int32.TryParse(str, out val);
                    board[i, j].val = val;
                    board[i, j].found = false;
                    j++;
                }
            }
        }
    }

    public bool updateBoard(int n)
    {
        bool match = false;
        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (board[i,j].val == n)
                {
                    board[i,j].found = true;
                    match = true;
                    break;
                }
            }
            if (match) { break; }
        }

        if (match)
        {
            // check all columns            
            for (int i = 0; i < 5; i++)
            {
                BingoCell[] data = new BingoCell[5];
                for (int j = 0; j < 5; j++)
                {
                    data[j] = board[i, j];
                }
                if (checkAllFound(data)) {                
                    bingo = true;
                    return true;
                }
            }

            // check all rows            
            for (int j = 0; j < 5; j++)
            {
                BingoCell[] data = new BingoCell[5];
                for (int i = 0; i < 5; i++)
                {
                    data[i] = board[i, j];
                }
                if (checkAllFound(data))
                {
                    bingo = true;
                    return true;
                }
            }
        }
        
        return false;
    }

    public bool checkAllFound(BingoCell[] data)
    {
        Debug.Assert(data.Length == 5);
        bool all = true;
        for (int i =0; i < 5; i++)
        {
            if (!data[i].found)
            {
                all = false; 
                break;
            }
        }
        return all;
    }

    public int getUnmarkedTotal()
    {
        int total = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (!board[i, j].found)
                {
                    total += board[i, j].val;
                }
            }
        }
        return total;
    }
}