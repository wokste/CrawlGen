using System.Diagnostics;

namespace CrawlGen.Gen;

public class BucketTable<T>
{
    private List<(T Value, float Prob)> _pairs = new();
    float _totalOdds;

    public IEnumerable<T> Values => _pairs.Select(p => p.Value);

    public BucketTable() {}

    public BucketTable(IEnumerable<(T Value, float Odds)> elems)
    {
        foreach (var (val, odd) in elems)
            Add(val, odd);
    }

    static public BucketTable<string> FromString(string line)
    {
        return BucketTable<string>.FromString(line, s => s);
    }

    static public BucketTable<T> FromString(string line, Func<string, T> func)
    {
        var ret = new BucketTable<T>();

        foreach (var s in line.Split(','))
        {
            var t = s.Trim().Split(':');
            float odds = 1;
            if (t.Length > 1 && !float.TryParse(t[1], out odds))
                throw new FormatException($"Invalid number format in token: {t[1]}");

            ret.Add(func(t[0]), odds);
        }
        return ret;
    }

    public void Add(T elem, float odd = 1)
    {
        _pairs.Add((elem, odd));
        _totalOdds += odd;
    }

    public void Normalize()
    {
        var max = _pairs.Max(p => p.Prob);

        for (int i = 0; i < _pairs.Count; i++)
            _pairs[i] = (_pairs[i].Value, _pairs[i].Prob / max);
    }

    public T TakeOne()
    {
        const int ATTEMTS = 20;
        for (int i = 0; i < ATTEMTS; ++i)
        {
            var (val,prob) = _pairs[Rng.UniformInt(_pairs.Count)];

            if (!Rng.P(prob))
                continue;

            return val;
        }

        int fallbackIndex = Rng.UniformInt(_pairs.Count);
        return _pairs[fallbackIndex].Value;
    }

    public List<T> TakeN(int n)
    {
        float remainingOdds = _totalOdds;
        float remainingElems = n;

        List<T> ret = new List<T>();

        foreach (var p in _pairs)
        {
            float mult = remainingElems / remainingOdds;

            int count = (int)(p.Prob * mult + Rng.UniformDouble());
            for (int i = 0; i < count; ++i)
                ret.Add(p.Value);

            remainingElems -= count;
            remainingOdds -= p.Prob;
        }

        // The list ret should now contain n elements.
        Debug.Assert(ret.Count == n);
        ret.Shuffle();

        return ret;
    }
}
