using System.Reflection;

namespace CrawlGen.Writers.Utils;

public class HTMLPage : IDisposable
{
    Stream Stream;
    StreamWriter Writer;

    public HTMLPage(string FileName)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FileName));
        Stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        Writer = new StreamWriter(Stream);
    }

    public void WriteElem(string type, string text, object? values = null)
    {
        using var dom = MakeDom(type, values);
        Writer.Write(text);
    }

    public HTMLDom MakeDom(string type, object? values = null)
    {
        return new HTMLDom(Writer, type, values);
    }

    internal void WriteKeyValue(string key, string value)
    {
        Writer.Write($"<b>{key}</b> {value} ");
    }

    internal void Write(string v)
    {
        Writer.Write(v);
    }

    public void Dispose()
    {
        Writer.Dispose();
        Stream.Dispose();
    }
}

public class HTMLDom : IDisposable
{
    readonly string Type;
    StreamWriter Writer;

    public HTMLDom(StreamWriter writer, string type, object? values = null)
    {
        Type = type;
        Writer = writer;

        Writer.Write($"<{type}");
        if (values != null)
        {
            foreach (FieldInfo member in values.GetType().GetFields())
            {
                string? txt = member.GetValue(values)?.ToString();
                txt = System.Net.WebUtility.HtmlEncode(txt);
                Writer.Write($" {member.Name}=\"{txt ?? ""}\"");
            }


            foreach (PropertyInfo member in values.GetType().GetProperties())
            {
                string? txt = member.GetValue(values)?.ToString();
                txt = System.Net.WebUtility.HtmlEncode(txt);
                Writer.Write($" {member.Name}=\"{txt ?? ""}\"");
            }

        }
        Writer.Write(">");
        Type = type;
    }

    public void Dispose()
    {
        Writer.WriteLine($"</{Type}>");
    }
}
