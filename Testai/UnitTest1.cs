using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using TextConverter;

namespace Testai
{
    [TestClass]
    public class PradiniaiTestai
    {
        [TestMethod]
        public void ReadingStringOfPrekesToAListOfPrekesType()
        {
            //Arrange
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            File.WriteAllText(fileName, "530\t\"02164\"\t\"02164 Poliuretano detalė Forafoam 002 aukštas atlošas\"\t\"Hovden\"\t5.75000", Encoding.GetEncoding(1257));
            var rpt = new ReadPrekesText();
            var expected = new List<Prekes>
            {
                new Prekes()
                {
                    Kaina = "5.75",
                    Numeris = "530",
                    PrekesKodas = "02164",
                    PrekesPavadinimas = "02164 Poliuretano detalė Forafoam 002 aukštas atlošas",
                    PrekiuGrupe = "Hovden"
                }
            };
            //Act
            var actual = rpt.ReadPrekesFromText(fileName);
            //Assert
            Assert.AreEqual(expected[0].PrekesPavadinimas, actual[0].PrekesPavadinimas);

            #region Testas su MockInputFile
            //Testinam su system.io.abstractions - veikia, taciau nera kaip encodingo uzdet
            /*var mockInputFile = new MockFileData("530\t\"02164\"\t\"02164 Poliuretano detalė Forafoam 002 aukštas atlošas\"\t\"Hovden\"\t5.75000");
            string fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);

            var sut = new ReadPrekesText(mockFileSystem);
            var expected = new List<Prekes>
            {
                new Prekes()
                {
                    Kaina = "5.75",
                    Numeris = "530",
                    PrekesKodas = "02164",
                    PrekesPavadinimas = "02164 Poliuretano detalė Forafoam 002 aukštas atlošas",
                    PrekiuGrupe = "Hovden"
                }
            };
            //Act
            List<Prekes> actual = sut.ReadPrekesFromText(fileName);
            //Assert
            Assert.AreEqual(expected[0].PrekesPavadinimas, actual[0].PrekesPavadinimas);*/
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Nuskaitymo klaida")]
        public void ReadingBadFormattedStringOfPrekesToAListOfPrekesType()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            File.WriteAllText(fileName, "530\"02164\"\t\"02164 Poliuretano detalė Forafoam 002 aukštas atlošas\"\t\"Hovden\"\t5.75000", Encoding.GetEncoding(1257));
            var rpt = new ReadPrekesText();
            var expected = new List<Prekes>
            {
                new Prekes()
                {
                    Kaina = "5.75",
                    Numeris = "530",
                    PrekesKodas = "02164",
                    PrekesPavadinimas = "02164 Poliuretano detalė Forafoam 002 aukštas atlošas",
                    PrekiuGrupe = "Hovden"
                }
            };
            //Act
            var actual = rpt.ReadPrekesFromText(fileName);
            #region Testas mockInputFile
            //Arrange
            /*var mockInputFile = new MockFileData("530\"02164\"\t\"02164 Poliuretano detalė Forafoam 002 aukštas atlošas\"\t\"Hovden\"\t5.75000", Encoding.GetEncoding(1257));
            string fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);
            //var sut = new ReadPrekesText(mockFileSystem);
            //Act
            //List<Prekes> actual = sut.ReadPrekesFromText(fileName);*/
            #endregion
        }
        [TestMethod]
        public void SuccessfulReadingStringOfSandeliaiToAListOfSandeliai()
        {
            //Act
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            File.WriteAllText(fileName, "\"S1\"\t\"Bendrasis\"", Encoding.GetEncoding(1257));
            var rst = new ReadSandeliaiText();
            var expected = new List<Sandeliai>
            {
                new Sandeliai()
                {
                    Kodas = "S1",
                    Pavadinimas = "Bendrasis"
                }
            };
            //Act
            var actual = rst.ReadSandeliaiFromText(fileName);
            //Assert
            Assert.AreEqual(expected[0].Kodas, actual[0].Kodas);
            #region testas su mockInputFile
            //Arrange
            /*var mockInputFile = new MockFileData("\"S1\"\t\"Bendrasis\"", Encoding.GetEncoding(1257));
            string fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);
            var sut = new ReadSandeliaiText(mockFileSystem);
            var expected = new List<Sandeliai>
            {
                new Sandeliai()
                {
                    Kodas = "S1",
                    Pavadinimas = "Bendrasis"
                }
            };
            //Act
            var actual = sut.ReadSandeliaiFromText(fileName);
            //Assert
            Assert.AreEqual(expected[0].Kodas, actual[0].Kodas);*/
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Nuskaitymo klaida")]
        public void ReadingBadFormattedStringOfSandeliaiToAListOfSandeliai()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            File.WriteAllText(fileName, "\"S1\"\"Bendrasis\"", Encoding.GetEncoding(1257));
            var rst = new ReadSandeliaiText();

            var expected = new List<Sandeliai>
            {
                new Sandeliai()
                {
                    Kodas = "S1",
                    Pavadinimas = "Bendrasis"
                }
            };
            //Act
            var actual = rst.ReadSandeliaiFromText(fileName);
            #region Testas su mockInputFile
            //Arrange
            /*var mockInputFile = new MockFileData("\"S1\"\"Bendrasis\"", Encoding.GetEncoding(1257));
            string fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);
            var sut = new ReadSandeliaiText(mockFileSystem);
            //Act
            var actual = sut.ReadSandeliaiFromText(fileName);*/
            #endregion
        }
        [TestMethod]
        public void SuccessfulReadingStringOfKlientaiToAListOfKlientai()
        {
            //Arrange
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            File.WriteAllText(fileName, "\"147223840\"\t\"A. Narbuto įmonė\"", Encoding.GetEncoding(1257));
            var rkt = new ReadKlientaiText();
            var expected = new List<Klientai>
            {
                new Klientai()
                {
                    Kodas = "147223840",
                    Pavadinimas = "A. Narbuto įmonė"
                }
            };
            //Act
            var actual = rkt.ReadKlientaiFromText(fileName);
            //Assert
            Assert.AreEqual(expected[0].Kodas, actual[0].Kodas);

            #region Testas su mockInputFile
            //Arrange
            /*var mockInputFile = new MockFileData("\"147223840\"\t\"A. Narbuto �mon�\"", Encoding.GetEncoding(1257));
            string fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);
            var sut = new ReadKlientaiText(mockFileSystem);
            var expected = new List<Klientai>
            {
                new Klientai()
                {
                    Kodas = "147223840",
                    Pavadinimas = "A. Narbuto įmonė"
                }
            };
            //Act
            var actual = sut.ReadKlientaiFromText(fileName);
            //Assert
            Assert.AreEqual(expected[0].Kodas, actual[0].Kodas);*/
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Nuskaitymo klaida")]
        public void ReadingBadFormattedStringOfKlientaiToAListOfKlientai()
        {
            //Arrange
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            File.WriteAllText(fileName, "\"147223840\"\"A. Narbuto įmonė\"", Encoding.GetEncoding(1257));
            var rkt = new ReadKlientaiText();
            var expected = new List<Klientai>
            {
                new Klientai()
                {
                    Kodas = "147223840",
                    Pavadinimas = "A. Narbuto įmonė"
                }
            };
            //Act
            var actual = rkt.ReadKlientaiFromText(fileName);
            #region Testas su mockInputFile
            //Arrange
            /*var mockInputFile = new MockFileData("\"147223840\"\"A. Narbuto �mon�\"", Encoding.GetEncoding(1257));
            string fileName = "C:\\Users\\jgrin\\Desktop\\NAUJAS.TXT";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);
            var sut = new ReadKlientaiText(mockFileSystem);
            //Act
            var actual = sut.ReadKlientaiFromText(fileName);*/
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadingPrekesStringWithoutFileName()
        {
            //Arrange
            string fileName = "";
            var sut = new ReadPrekesText();
            //Act
            var temp = sut.ReadPrekesFromText(fileName);
            #region Testas su mockInputFile
            /*var mockInputFile = new MockFileData("530\t\"02164\"\t\"02164 Poliuretano detalė Forafoam 002 aukštas atlošas\"\t\"Hovden\"\t5.75000", Encoding.GetEncoding(1257));
            string fileName = "";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);
            // var sut = new ReadPrekesText(mockFileSystem);
            //Act
            //var temp = sut.ReadPrekesFromText(fileName);*/
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadingPrekesStringWithoutValidFileNameExtension()
        {
            //Arrange
            string fileName = "aaaa";
            var sut = new ReadPrekesText();
            //Act
            var temp = sut.ReadPrekesFromText(fileName);
            #region Testas su mockInputFile
            /*var mockInputFile = new MockFileData("530\t\"02164\"\t\"02164 Poliuretano detalė Forafoam 002 aukštas atlošas\"\t\"Hovden\"\t5.75000", Encoding.GetEncoding(1257));
            string fileName = "aaaa";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(fileName, mockInputFile);
            //var sut = new ReadPrekesText(mockFileSystem);
            //Act
            //var temp = sut.ReadPrekesFromText(fileName);*/
            #endregion
        }
    }
}
