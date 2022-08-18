using CrawlGen.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Model
{
    public struct Anchor
    {
        readonly string ID;

        static LinkGenerator LG = new();

        public Anchor()
        {
            ID = LG.Next();
        }

        public dynamic Href => new{ href= $"#{ID}" };
        public dynamic Id => new{ id= ID };
    }

    public class LinkGenerator{
        uint AutoInc = 0;
        uint Mask;

        const int CHARS = 6;
        const int BITS_PER_CHAR = 5;
        const uint BIT_MASK = 0x3fffffff;

        public LinkGenerator()
        {
            AutoInc = (uint)Rng.UniformInt() & BIT_MASK;
            Mask = (uint)Rng.UniformInt() & BIT_MASK;
        }

        internal string Next()
        {
            AutoInc += (uint)Rng.D(6);
            AutoInc &= BIT_MASK;

            return Stringify(Bijection(AutoInc));
        }


        public uint Bijection(uint val)
        {
            val *= 0x0d107c17;
            val ^= 0x6c2b19aa;
            val *= 0xd2d5a147;
            val ^= 0xd78f2862;
            val ^= val >> 3;
            val ^= val >> 9;
            val ^= val >> 11;

            val &= BIT_MASK;
            return val;
        }

        public string Stringify(uint val)
        {
            StringBuilder sb = new StringBuilder();
            
            const string TABLE = "RCTN3FELP6Y5BQ8JS27AZX9VDKMUGHW4";
            for (int i = 0; i < CHARS; ++i)
            {
                int c = (int)val % TABLE.Length;
                sb.Append(TABLE[c]);
                val >>= BITS_PER_CHAR;
            }
            return sb.ToString();
        }
    }
}
