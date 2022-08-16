namespace CrawlGen.Out.Utils
{
    public class HTMLPage
    {
        public void WriteElem(string heading, string text)
        {
            Console.ForegroundColor = heading == "p" ? ConsoleColor.Gray : ConsoleColor.Yellow;
            Console.WriteLine(text);
        }
    }
}
