using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace TextConverter
{
    public class ReadPrekesText
    {
        /*readonly IFileSystem _fileSystem;
        //Konstruktoriai mockinant FileSystem ir naudojant system.io.abstractions
        //tuscias konstruktorius - productione, o overridintas - testui.
        public ReadPrekesText() : this(new FileSystem()) { }
        public ReadPrekesText(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }*/
        public List<Prekes> ReadPrekesFromText(string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (fileName.Length == 0 || !fileName.Contains(".TXT")) throw new ArgumentException();
            List<Prekes> prekiuSarasas = new List<Prekes>();
            try
            {
                //uzkomentuotas kodas kai naudojame filesystema. viskas veikia, bet nera kaip idet encodingo, nes readina faila su UTF8, o jis lt simboliu neskaito
                using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding(1257))/*_fileSystem.File.OpenText(fileName)*/)
                {
                    int eilute = 0;
                    char quotes = '"';
                    bool isNumeric;
                    while (!sr.EndOfStream)
                    {
                        if (eilute >= 0)
                        {
                            string temp = sr.ReadLine();
                            if (temp.Length != 0)
                            {
                                try
                                {
                                    string[] tempTrimmed = Array.ConvertAll(temp.Split('\t'), p => p.Trim(quotes));
                                    Prekes prekes = new Prekes
                                    {
                                        Numeris = tempTrimmed[0],
                                        PrekesKodas = tempTrimmed[1],
                                        PrekesPavadinimas = tempTrimmed[2],
                                        PrekiuGrupe = tempTrimmed[3]
                                    };
                                    if (!(isNumeric = decimal.TryParse(tempTrimmed[4], NumberStyles.Any, new CultureInfo("en-US"), out decimal kaina)))
                                    {
                                        prekes.Kaina = Regex.Replace(tempTrimmed[4], "[^.0-9]", "");
                                    }
                                    if (kaina > 0)
                                    {
                                        //nukerpami papildomi nuliai iki vieno skaiciaus po kableliu(jei ne nulis)
                                        prekes.Kaina = kaina.ToString("G29", CultureInfo.InvariantCulture);
                                        prekiuSarasas.Add(prekes);
                                    }
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    throw new Exception("Nuskaitymo klaida");
                                }
                            }
                        }
                        eilute++;
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Negalima atidaryti dokumento");
            }
            return prekiuSarasas;
        }
    }
}
