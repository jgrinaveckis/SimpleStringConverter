using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace TextConverter
{
    public class ReadKlientaiText
    {

        /*readonly IFileSystem _fileSystem;
        //Konstruktoriai mockinant FileSystem ir naudojant system.io.abstractions
        //tuscias konstruktorius - productione, o overridintas - testui.
        public ReadKlientaiText() { }
        public ReadKlientaiText(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }*/
        public List<Klientai> ReadKlientaiFromText(string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (fileName.Length == 0 || !fileName.Contains(".TXT")) throw new ArgumentException();
            List<Klientai> klientuSarasas = new List<Klientai>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding(1257)))
                {
                    int eilute = 0;
                    char quotes = '"';
                    bool isNumeric;

                    while (!sr.EndOfStream)
                    {
                        string temp = sr.ReadLine();
                        if (eilute >= 0)
                        {
                            string[] tempTrimmed = Array.ConvertAll(temp.Split('\t'), p => p.Trim(quotes).Replace("\"", ""));
                            Klientai klientai = new Klientai();
                            try
                            {
                                if (tempTrimmed[0].Length != 0)
                                {
                                    if (!(isNumeric = double.TryParse(tempTrimmed[0], out double k)))
                                    {
                                        klientai.Kodas = Regex.Replace(tempTrimmed[0], "[^-0-9]", "");
                                    }
                                    else
                                    {
                                        klientai.Kodas = tempTrimmed[0];
                                    }
                                    klientai.Pavadinimas = tempTrimmed[1];
                                    klientuSarasas.Add(klientai);
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                throw new Exception("Nuskaitymo klaida");
                            }
                        }
                        eilute++;
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Negalima atidaryti failo");
            }
            return klientuSarasas;
        }
    }
}
