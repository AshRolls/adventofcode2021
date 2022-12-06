namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly string[] _input;
    private byte[,] _grid;

    public Day11()
    {
        _input = File.ReadAllLines(InputFilePath);       
    }

    public override ValueTask<string> Solve_1()
    {
        createGrid();
       
        int flashes = 0;
        for (int i = 0; i < 100; i++)
        {
            flashes += doStep();
        }

        return new(flashes.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        createGrid();

        int steps = 0;
        do
        {
            steps++;
        } while (doStep() != 100);

        return new(steps.ToString());
    }

    private int doStep()
    {
        bool[,] flashed = new bool[12,12];
        for (int i = 1; i < 11; i++)
        {
            for (int j = 1; j < 11; j++)
            {
                _grid[i, j]++;
                flashed[i,j] = false;                
            }
        }

        int flashes = 0;
        int lastFlashes;
        do
        {
            lastFlashes = flashes;
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (_grid[i, j] > 9)
                    {
                        if (!flashed[i, j])
                        {
                            flashed[i, j] = true;
                            flashes++;
                            _grid[i - 1, j - 1]++;
                            _grid[i, j - 1]++;
                            _grid[i + 1, j - 1]++;
                            _grid[i - 1, j]++;
                            _grid[i + 1, j]++;
                            _grid[i - 1, j + 1]++;
                            _grid[i, j + 1]++;
                            _grid[i + 1, j + 1]++;
                        }
                    }
                }
            }
        } while (lastFlashes != flashes);

        for (int i = 1; i < 11; i++)
        {
            for (int j = 1; j < 11; j++)
            {
                if (_grid[i, j] > 9) _grid[i, j] = 0;
            }
        }

       return flashes;
    }

    private void createGrid()
    {
        _grid = new byte[12, 12];
        for (int i = 1; i < 11; i++)
        {
            for (int j = 1; j < 11; j++)
            {
                _grid[i, j] = (byte)int.Parse(_input[i-1][j-1].ToString());
            }
        }
    }
}
