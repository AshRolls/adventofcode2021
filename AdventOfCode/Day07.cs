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
        
        int min = positions.Min();
        int max = positions.Max();
        int mid = ((max - min) / 2) + min;
        
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
        string[] strs = _input[0].Split(',');
        int[] positions = Array.ConvertAll(strs, s => int.Parse(s));

        int min = positions.Min();
        int max = positions.Max();
        int mid = ((max - min) / 2) + min;

        double bestF = checkFuelUse2(positions, mid);
        double nextF = checkFuelUse2(positions, mid + 1);
        int best = mid;
        int next = mid + 1;
        bool reversed = false;

        if (nextF > bestF)
        {
            reversed = true;
            next = mid - 1;
            nextF = checkFuelUse2(positions, next);
        }

        // carry on checking next until it stops dropping
        while (nextF < bestF)
        {
            best = next;
            bestF = nextF;
            if (!reversed) next++;
            else next--;
            nextF = checkFuelUse2(positions, next);
        }

        return new(bestF.ToString());
    }

    private int checkFuelUse(int[] positions, int h)
    {
        int fuel = 0;

        for (int i = 0; i< positions.Length; i++)
        {
            if (positions[i] < h)
            {
                fuel += h - positions[i];
            }
            else
            {
                fuel += positions[i] - h;
            }
        }

        return fuel;
    }

    private double checkFuelUse2(int[] positions, int h)
    {
        double fuel = 0;
        int steps;

        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i] < h)
            {
                steps = h - positions[i];
                fuel = calcFuel(fuel, steps);
            }
            else
            {
                steps = positions[i] - h;
                fuel = calcFuel(fuel, steps);
            }
        }

        return fuel;
    }

    private static double calcFuel(double fuel, int steps)
    {
        if (steps > 0)
        {
            int pair = steps + 1;
            double t;
            if (steps % 2 == 0) t = pair * (steps / 2);
            else
            {
                t = pair * ((steps + 1) / 2);
                t -= pair / 2;
            }

            fuel += t;
        }

        return fuel;
    }
}
