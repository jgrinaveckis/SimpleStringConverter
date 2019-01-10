using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TextConverter
{
    public class ReadSandeliaiText
    {
        /*readonly IFileSystem _fileSystem;
        //Konstruktoriai mockinant FileSystem ir naudojant system.io.abstractions
        //tuscias konstruktorius - productione, o overridintas - testui.
        public ReadSandeliaiText() { }
        public ReadSandeliaiText(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }*/
        public List<Sandeliai> ReadSandeliaiFromText(string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (fileName.Length == 0) throw new ArgumentNullException();
            List<Sandeliai> sandeliuSarasas = new List<Sandeliai>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding(1257)))
                {
                    int eilute = 0;
                    char quotes = '"';
                    while (!sr.EndOfStream)
                    {
                        string temp = sr.ReadLine();
                        if (eilute >= 0)
                        {
                            string[] tempTrimmed = Array.ConvertAll(temp.Split('\t'), p => p.Trim(quotes));
                            try
                            {
                                Sandeliai sandeliai = new Sandeliai
                                {
                                    Kodas = tempTrimmed[0],
                                    Pavadinimas = tempTrimmed[1]
                                };
                                sandeliuSarasas.Add(sandeliai);
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
                MessageBox.Show("Nepavyko atidaryti dokumento");
            }
            return sandeliuSarasas;
        }
    }
}
