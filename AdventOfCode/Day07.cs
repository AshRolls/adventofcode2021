namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string[] _input;    

    public Day07()
    {
        _input = File.ReadAllLines(InputFilePath);

    }

    public override ValueTask<string> Solve_1()
    {
        string[] strs = _input[0].Split(',');
        int[] positions = Array.ConvertAll(strs, s => int.Parse(s));        
        int rndAvg = (int)Math.Round(positions.Average());
        
        int min = positions.Min();
        int max = positions.Max();
        int mid = ((max - min) / 2) + min;
        //int fuelMin = checkFuelUse(positions, min);
        //int fuelMax = checkFuelUse(positions, max);
        
        int bestF = checkFuelUse(positions, mid);
        int nextF = checkFuelUse(positions, mid + 1);
        int best = mid;
        int next = mid + 1;
        bool reversed = false;

        if (nextF > bestF)
        {          
            reversed = true;                      
            next = mid - 1;
            nextF = checkFuelUse(positions, next);
        }        

        // carry on checking next until it stops dropping
        while (nextF < bestF)
        {
            best = next;
            bestF = nextF;
            if (!reversed) next++;
            else next--;
            nextF = checkFuelUse(positions, next);
        }

        return new(bestF.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("Not Solved");
    }

    private int checkFuelUse(int[] positions, int mid)
    {
        int fuel = 0;

        for (int i = 0; i< positions.Length; i++)
        {
            if (positions[i] < mid)
            {
                fuel += mid - positions[i];
            }
            else
            {
                fuel += positions[i] - mid;
            }
        }

        return fuel;
    }
}
