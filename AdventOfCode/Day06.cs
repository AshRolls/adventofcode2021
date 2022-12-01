namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _input;    

    public Day06()
    {
        _input = File.ReadAllLines(InputFilePath);        
    }

    public override ValueTask<string> Solve_1()
    {
        string[] strs = _input[0].Split(',');
        List<short> shoal = Array.ConvertAll(strs, s => short.Parse(s)).ToList();

        for (int i = 0; i < 80; i++)
        {
            doDay(shoal);
        }

        return new(shoal.Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        string[] strs = _input[0].Split(',');
        List<short> shoal = Array.ConvertAll(strs, s => short.Parse(s)).ToList();
        long[] ages = new long[9];

        initAges(ages, shoal);

        for (int i = 0; i < 256; i++)
        {
            doDayOpt(ages);
        }

        return new(ages.Sum().ToString());
    }

    private void initAges(long[] ages, List<short> shoal)
    {
        foreach(short s in shoal)
        {
            ages[s]++;
        }
    }

    private void doDayOpt(long[] ages)
    {
        long breeders = ages[0];
        ages[0] = 0;
        for (int i = 1; i < 9; i++)
        {
            ages[i - 1] = ages[i];
        }
        ages[6] += breeders;
        ages[8] = breeders;
    }

    private void doDay(List<short> shoal)
    {
        int toAdd = 0;

        for (int i = 0; i < shoal.Count; i++)
        {
            if (shoal[i] == 0) //  breed
            {
                shoal[i] = 6;
                toAdd++;
            }
            else
            {
                shoal[i]--;
            }
        }

        for (int i = 0; i < toAdd; i++)
        {
            shoal.Add(8);
        }
    }

}
