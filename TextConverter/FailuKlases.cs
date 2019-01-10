using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    public class Klientai
    {
        public string Kodas { get; set; }
        public string Pavadinimas { get; set; }
    }

    public class Prekes
    {
        public string Numeris { get; set; }
        public string PrekesKodas { get; set; }
        public string PrekesPavadinimas { get; set; }
        public string PrekiuGrupe { get; set; }
        public string Kaina { get; set; }
    }

    public class Sandeliai
    {
        public string Kodas { get; set; }
        public string Pavadinimas { get; set; }
    }
}
