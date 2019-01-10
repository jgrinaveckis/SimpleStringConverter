using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace TextConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            };

            if (ofd.ShowDialog() == true)
            {
                string fileName = ofd.FileName;
                try
                {
                    switch (fileName)
                    {
                        case string a when a.Contains("KLIENTAI"):
                            ReadKlientaiText rkt = new ReadKlientaiText();
                            List<Klientai> klientuSarasas = new List<Klientai>();
                            klientuSarasas = rkt.ReadKlientaiFromText(fileName);
                            Listview.ItemsSource = klientuSarasas;
                            break;
                        case string a when a.Contains("SANDELIAI"):
                            ReadSandeliaiText rst = new ReadSandeliaiText();
                            List<Sandeliai> sandeliuSarasas = new List<Sandeliai>();
                            sandeliuSarasas = rst.ReadSandeliaiFromText(fileName);
                            Listview.ItemsSource = sandeliuSarasas;
                            break;
                        case string a when a.Contains("PREKES"):
                            ReadPrekesText rpt = new ReadPrekesText();
                            List<Prekes> prekiuSarasas = new List<Prekes>();
                            prekiuSarasas = rpt.ReadPrekesFromText(fileName);
                            PrekiuListview.ItemsSource = prekiuSarasas;
                            break;
                        default:
                            MessageBox.Show("Pasirinktas netinkamas dokumentas " + fileName);
                            break;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Dokumentas neatitinka reikalavimu " + fileName);
                }
            }
        }
    }
}
