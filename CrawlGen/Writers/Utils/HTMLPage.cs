using System.Reflection;

namespace CrawlGen.Out.Utils
{
    public class HTMLPage
    {
        public void WriteElem(string type, string text, object? values = null)
        {
            using var dom = MakeDom(type, values);
            Console.Write(text);
        }

        internal HTMLDom MakeDom(string type, object? values = null)
        {
            return new HTMLDom(type, values);
        }
    }

    public class HTMLDom : IDisposable
    {
        readonly string Type;

        public HTMLDom(string type, object? values = null)
        {
            Type = type;
            Console.Write($"<{type}");
            if (values != null)
            {
                foreach (FieldInfo member in values.GetType().GetFields())
                {
                    string? txt = member.GetValue(values).ToString();
                    txt = System.Net.WebUtility.HtmlEncode(txt);
                    Console.Write($" {member.Name}=\"{txt ?? ""}\"");
                }
            }
            Console.Write(">");
            Type = type;
        }

        public void Dispose()
        {
            Console.WriteLine($"</{Type}>");
        }
    }
}
