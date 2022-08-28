using System.Diagnostics;

namespace CrawlGen.Gen;

internal static class Names
{
    public static string Make(params BucketTable<string>[] p)
    {
#if DEBUG
        for (int i = 0; i < p.Length; i++)
        {
            foreach (string elem in p[i].Values)
            {
                Debug.Assert(elem.Contains('@') == (i > 0));
            }
        }
#endif

        string ret = "";
        foreach (var elem in p)
        {
            ret = elem.TakeOne().Replace("@", ret);
        }
        return ret;
    }
}
