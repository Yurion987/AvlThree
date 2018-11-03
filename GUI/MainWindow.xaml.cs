using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AvlThree;
namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpravujKataster AfrikaKataster { get; set; }
        private List<StackPanel> PanelList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            AfrikaKataster = new SpravujKataster();
            PanelList = new List<StackPanel>();
            PanelList.Add(KatUzemPanel);
            PanelList.Add(ListVlastPanel);
            PanelList.Add(NehnutelnostPanel);
            PanelList.Add(ObcanPanel);
            PanelList.Add(PanelKatasterButony);
            PanelList.Add(PanelListyButony);
            PanelList.Add(PanelNehnutelnostiButony);
            PanelList.Add(PanelObcanButony);
            PanelList.Add(Uloha12Panel);
            PanelList.Add(Uloha7Panel);
            PanelList.Add(Uloha8Panel);
            PanelList.Add(Uloha1Panel);
            PanelList.Add(Uloha2Panel);
            PanelList.Add(Uloha3Panel);
            PanelList.Add(Uloha4Panel);
            PanelList.Add(Uloha5Panel);
            PanelList.Add(Uloha6Panel);
            PanelList.Add(Uloha9Panel);
            PanelList.Add(Uloha10Panel);
            PanelList.Add(Uloha11Panel);
            PanelList.Add(Uloha20Panel);
            PanelList.Add(Uloha22Panel);
            PanelList.Add(UlohVsetkoPanel);
        
        }

        private void VlozObcana_Click(object sender, RoutedEventArgs e)
        {

            var RodneCislo = InputRodneCislo.Text;
            if (RodneCislo.Length == 16)
            {

                if (InputDatumNarodenia.SelectedDate != null)
                {
                    var meno = InputName.Text;
                    var priezvisko = InputPriezvisko.Text;
                    var uspech = AfrikaKataster.PridajOsobu(RodneCislo, meno, priezvisko, InputDatumNarodenia.SelectedDate.Value.Date);
                    if (uspech == "OK")
                    {
                        MessageBox.Show("Osoba Uspesne preidana", "OK");
                    }
                    else
                    {
                        MessageBox.Show(uspech, "Neuspesne pridanie osoby");
                    }
                }
                else
                {
                    MessageBox.Show("Nezadany datum narodenia", "Zly datum");
                }
            }
            else
            {
                MessageBox.Show("Zla dlzka rodneho cisla", "Zle Rodne Cislo");
            }
        }

        private void VlozList_Click(object sender, RoutedEventArgs e)
        {
            var IDlistu = int.Parse(InputIDListu.Text);
            var NazovUzemia = InputKatasUzemieList.Text;
            var uspech = AfrikaKataster.PridajList(NazovUzemia, IDlistu);
            if (uspech == "OK")
            {
                var nazovKatastru = InputKatasUzemieList.Text;
                Uloha19ListKata.ItemsSource = AfrikaKataster.Uloha19NazovKat(nazovKatastru);
            }
            else
            {
                MessageBox.Show(uspech, "ERR");
            }

        }

        private void InputIDListu_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputIDListu.Text = Regex.Replace(InputIDListu.Text, "[^0-9]+", "");
        }

        private void VlozKataster_Click(object sender, RoutedEventArgs e)
        {
            var InputNazov = InputNazovKatUzemia.Text;
            var IdKat = int.Parse(InputIDKatUzem.Text);
            var uspech = AfrikaKataster.PridajKatastralUzemie(IdKat, InputNazov);
            if (uspech == "OK")
            {
                Uloha15List.ItemsSource = AfrikaKataster.Uloha15();
            }
            else
            {
                MessageBox.Show(uspech, "ERR");
            }
        }

        private void PridajNehnutelnost_Click(object sender, RoutedEventArgs e)
        {

            var SupisCislo = int.Parse(InputSupisCislo.Text);
            var IdListu = int.Parse(InputListNehnutelnost.Text);
            var IdKatastru = int.Parse(InputKatasterNehnutelnost.Text);
            var popis = InputPopis.Text;
            var adresa = InputAdresa.Text;
            var uspech = AfrikaKataster.PridajNehnutelnost(IdListu, IdKatastru, adresa, popis, SupisCislo);
            if (uspech == "OK")
            {
                MessageBox.Show("Nehnutelnost uspesne pridana", "OK");
            }
            else
            {
                MessageBox.Show(uspech, "ERR");
            }
        }
        private void AktivnyPanel(StackPanel aktivny)
        {
            foreach (var item in PanelList)
            {
                item.Visibility = Visibility.Hidden;
            }
            aktivny.Visibility = Visibility.Visible;
            var list = AfrikaKataster.VsetkoInfo();
            LabelKat.Content = "K:"+list[0];
            LabelList.Content = "L:" + list[2];
            LabelObcan.Content = "O:" + list[1];
            LabelNeh.Content = "N:" + list[3];
        }

        private void Kataster_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(KatUzemPanel);
            Uloha15List.ItemsSource = AfrikaKataster.Uloha15();
        }

        private void ListNehnutelnosti_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(ListVlastPanel);
        }

        private void Nehnutelnosti_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(NehnutelnostPanel);
        }

        private void Obcan_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(ObcanPanel);
        }

        private void InputIDKatUzem_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputIDKatUzem.Text = Regex.Replace(InputIDKatUzem.Text, "[^0-9]+", "");
        }

        private void InputSupisCislo_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputSupisCislo.Text = Regex.Replace(InputSupisCislo.Text, "[^0-9]+", "");
        }

        private void InputListNehnutelnost_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputListNehnutelnost.Text = Regex.Replace(InputListNehnutelnost.Text, "[^0-9]+", "");
        }

        private void InputKatasterNehnutelnost_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputKatasterNehnutelnost.Text = Regex.Replace(InputKatasterNehnutelnost.Text, "[^0-9]+", "");
        }

        private void InputUloha7NazovKatastru_KeyUp(object sender, KeyEventArgs e)
        {
            var nazovKatastru = InputUloha7NazovKatastru.Text;
            if (e.Key == Key.Enter)
            {
                Uloha7List.ItemsSource = AfrikaKataster.Uloha7(nazovKatastru);
            }
        }

        private void InputUloha8CisloKatUzem_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputUloha8CisloKatUzem.Text = Regex.Replace(InputUloha8CisloKatUzem.Text, "[^0-9]+", "");
        }

        private void InputUloha8RodCislo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var idKatastru = int.Parse(InputUloha8CisloKatUzem.Text);
                var rodCisloObcan = InputUloha8RodCislo.Text;
                Uloha8List.ItemsSource = AfrikaKataster.Uloha8(rodCisloObcan, idKatastru);
            }
        }

        private void InputUloha8CisloKatUzem_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                var idKatastru = int.Parse(InputUloha8CisloKatUzem.Text);
                var rodCisloObcan = InputUloha8RodCislo.Text;
                Uloha8List.ItemsSource = AfrikaKataster.Uloha8(rodCisloObcan, idKatastru);
            }
        }

        private void Uloha12BtPridaj_Click(object sender, RoutedEventArgs e)
        {


            var rodCislo = InputUloha12RodCislo.Text;
            var cisloListu = int.Parse(InputUloha12CisloListuVlast.Text);
            var cisloKatUzem = int.Parse(InputUloha12CisloKatUzem.Text);
            var majetkovyPodiel = double.Parse(InputUloha12MajetkovyPodiel.Text);
            var uspech = AfrikaKataster.PridajVlastnika(rodCislo, cisloListu, cisloKatUzem, majetkovyPodiel);
            if (uspech == "OK")
            {    
                Uloha12List.ItemsSource = AfrikaKataster.Uloha12(rodCislo, cisloListu, cisloKatUzem);
            }
            else
            {
                MessageBox.Show(uspech, "Err");
            }
        }

        private void InputUloha12CisloListuVlast_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputUloha12CisloListuVlast.Text = Regex.Replace(InputUloha12CisloListuVlast.Text, "[^0-9]+", "");
        }

        private void InputUloha12CisloKatUzem_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputUloha12CisloKatUzem.Text = Regex.Replace(InputUloha12CisloKatUzem.Text, "[^0-9]+", "");
        }

        private void Uloha12BtUprav_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                var rodCislo = InputUloha12RodCislo.Text;
                var idKatastru = int.Parse(InputUloha12CisloKatUzem.Text);
                var cisloListu = int.Parse(InputUloha12CisloListuVlast.Text);
              
                Uloha12List.ItemsSource = AfrikaKataster.Uloha12(rodCislo, cisloListu, idKatastru);
            }
            catch (Exception)
            {

                MessageBox.Show("Zle zadane vstupne parametre", "Err");
            }

        }

        private void Uloha12List_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var rodneCislo = Uloha12List.SelectedItem.ToString().Substring(13, 16);
            var majetkovyPodiel = Uloha12List.SelectedItem.ToString().Substring(Uloha12List.SelectedItem.ToString().LastIndexOf(":") + 2);
            InputUloha12RodCislo.Text = rodneCislo;
            InputUloha12MajetkovyPodiel.Text = majetkovyPodiel;
        }

        private void Uloha12BUloz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var idKatastru = int.Parse(InputUloha12CisloKatUzem.Text);
                var rodCislo = InputUloha12RodCislo.Text;
                var cisloListu = int.Parse(InputUloha12CisloListuVlast.Text);
                var majetkovyPod = double.Parse(InputUloha12MajetkovyPodiel.Text);
                var uspech = AfrikaKataster.Uloha12Update(rodCislo, cisloListu, idKatastru, majetkovyPod);
                if (uspech == "OK")
                {
                    Uloha12List.ItemsSource = AfrikaKataster.Uloha12(rodCislo, cisloListu, idKatastru);
                }
                else
                {
                    MessageBox.Show(uspech, "Err");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Zle zadane ciselne hodnoty (Majetkovy Podiel)", "Err");
            }


        }

        private void ButtonUloha8_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha8Panel);
        }

        private void ButtonUloha7_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha7Panel);
        }

        private void ButtonUloha12_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha12Panel);
            Uloha12List.ItemsSource = new List<string>();
            InputUloha12CisloKatUzem.Text = "";
            InputUloha12RodCislo.Text = "";
            InputUloha12CisloListuVlast.Text = "";
            InputUloha12MajetkovyPodiel.Text = "";
        }

        private void ObcanMenu_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(PanelObcanButony);
        }

        private void KatasterMenu_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(PanelKatasterButony);
        }

        private void ListNehnutelnostiMenu_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(PanelListyButony);
        }

        private void NehnutelnostiMenu_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(PanelNehnutelnostiButony);
        }

        private void ButtonUloha1_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha1Panel);
        }

        private void InputUloha1KatCis_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputUloha1KatCis.Text = Regex.Replace(InputUloha1KatCis.Text, "[^0-9]+", "");
        }

        private void InputUloha1SupisCis_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputUloha1SupisCis.Text = Regex.Replace(InputUloha1SupisCis.Text, "[^0-9]+", "");
        }

        private void Uloha1Vyhladaj_Click(object sender, RoutedEventArgs e)
        {
            var idKatUzem = int.Parse(InputUloha1KatCis.Text);
            var supisCislo = int.Parse(InputUloha1SupisCis.Text);
            Uloha1List.ItemsSource = AfrikaKataster.Uloha1(supisCislo,idKatUzem);
        }

        private void Uloha2Vyhladaj_Click(object sender, RoutedEventArgs e)
        {
            var rodCislo = InputUloha2RodCislo.Text;
            Uloha2List.ItemsSource = AfrikaKataster.Uloha2(rodCislo);
        }

        private void Uloha2ObcanInfo_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha2Panel);
        }

        private void ButtonUloha3_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha3Panel);
        }

        private void Uloha3Vyhladaj_Click(object sender, RoutedEventArgs e)
        {
            var supisCislo = int.Parse(InputUloha3SupisCislo.Text);
            var katCislo = int.Parse(InputUloha3KatCis.Text);
            var listCislo = int.Parse(InputUloha3ListCis.Text);

            Uloha3List.ItemsSource = AfrikaKataster.Uloha3(katCislo,listCislo,supisCislo);
        }

        private void Uloha4Vyhladaj_Click(object sender, RoutedEventArgs e)
        {
            var idKat = int.Parse(InputUloha4KatCis.Text);
            var idList = int.Parse(InputUloha4ListCis.Text);
            Uloha4List.ItemsSource = AfrikaKataster.Uloha4(idKat, idList);
        }

        private void ButtonUloha4_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha4Panel);
        }

        private void ButtonUloha5_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha5Panel);
        }

        private void Uloha5Vyhladaj_Click(object sender, RoutedEventArgs e)
        {
            var nazovNeh = InputUloha5NazovKat.Text;
            var cisNeh = int.Parse(InputUloha5CisNehnutel.Text);

            Uloha5List.ItemsSource = AfrikaKataster.Uloha5(nazovNeh, cisNeh);
        }

        private void ButtonUloha6_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha6Panel);
        }

        private void Uloha6Vyhladaj_Click(object sender, RoutedEventArgs e)
        {
            var NazovKat = InputUloha6katNazov.Text;
            var idList = int.Parse(InputUloha6ListCis.Text);
            Uloha6List.ItemsSource = AfrikaKataster.Uloha6(NazovKat, idList);
        }

        private void ButtonUloha9_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha9Panel);
        }

        private void InputUloha9RodCislo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var rodCisloObcan = InputUloha9RodCislo.Text;
                Uloha9List.ItemsSource = AfrikaKataster.Uloha9(rodCisloObcan);
            }
        }

        private void Uloha10Pridaj_Click(object sender, RoutedEventArgs e)
        {
            var rodCislo = InputUloha10RodCislo.Text;

            var nazovKatuzem = InputUloha10KatCis.Text;

            var supisCsilo = int.Parse(InputUloha10SupisCislo.Text);
            var uspech = AfrikaKataster.Uloha10(rodCislo, supisCsilo, nazovKatuzem);
            if (uspech == "OK")
            {
                MessageBox.Show("Priradenie trvaleho bydliska bolo uspesne", "OK");
            }
            else
            {
                MessageBox.Show(uspech, "Err");
            }
        }

        private void ButtonUloha10_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha10Panel);
        }

        private void ButtonUloha11_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha11Panel);
        }

        private void Uloha11Pridaj_Click(object sender, RoutedEventArgs e)
        {
            var rodCisloPovod = InputUloha11RodCisloPovod.Text;
            var rodCisloNove = InputUloha11RodCisloNove.Text;
            var supisCislo = int.Parse(InputUloha11SupisCislo.Text);
            var cisKat = int.Parse(InputUloha11KatCis.Text);
            var uspech = AfrikaKataster.Uloha11(rodCisloPovod,supisCislo,cisKat,rodCisloNove);
            if (uspech == "OK")
            {
                MessageBox.Show("Zmena uspesna", "OK");
            }
            else
            {
                MessageBox.Show(uspech, "Err");
            }
        }

        private void Uloha13BZmaz_Click(object sender, RoutedEventArgs e)
        {
            var idKatastru = int.Parse(InputUloha12CisloKatUzem.Text);
            var rodCislo = InputUloha12RodCislo.Text;
            var cisloListu = int.Parse(InputUloha12CisloListuVlast.Text);
            Uloha12List.ItemsSource = AfrikaKataster.Uloha13(rodCislo, cisloListu, idKatastru);
        }

        private void InputKatasUzemieList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var nazovKatastru = InputKatasUzemieList.Text;
                Uloha19ListKata.ItemsSource = AfrikaKataster.Uloha19NazovKat(nazovKatastru);
            }
        }

        private void InputUloha19Katcislo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var cisloKata = int.Parse(InputUloha19Katcislo.Text);
                Uloha19ListKata.ItemsSource = AfrikaKataster.Uloha19CisloKat(cisloKata);
            }
        }

        private void InputUloha19IdListDel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var cisloListu = int.Parse(InputUloha19IdListDel.Text);
                var cisloKatastru = int.Parse(InputUloha19Katcislo.Text);
                Uloha19ListListVlast.ItemsSource = AfrikaKataster.Uloha19Listy(cisloKatastru,cisloListu);
            }
        }

        private void InputUloha19IdListNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var cisloListu = int.Parse(InputUloha19IdListNew.Text);
                var cisloKatastru = int.Parse(InputUloha19Katcislo.Text);
                Uloha19ListListVlast.ItemsSource = AfrikaKataster.Uloha19Listy(cisloKatastru,cisloListu);
            }
        }

        private void DeleteList_Click(object sender, RoutedEventArgs e)
        {
            var cisloListuNew = int.Parse(InputUloha19IdListNew.Text);
            var cisloListuDel = int.Parse(InputUloha19IdListDel.Text);
            var cisloKatastru = int.Parse(InputUloha19Katcislo.Text);
            var odpoved = AfrikaKataster.Uloha19(cisloListuDel,cisloKatastru,cisloListuNew);
            if (odpoved == "OK")
            {
                var cisloKata = int.Parse(InputUloha19Katcislo.Text);
                Uloha19ListKata.ItemsSource = AfrikaKataster.Uloha19CisloKat(cisloKata);
                Uloha19ListListVlast.ItemsSource = AfrikaKataster.Uloha19Listy(cisloKatastru, cisloListuNew);
                
            }
            else
            {
                MessageBox.Show(odpoved, "Err");
            }
        }

        private void ButtonUloha20_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha20Panel);
        }
        
        private void InputUloha20ListCis_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var cisloListu = int.Parse(InputUloha20ListCis.Text);
                var cisloKatastru = int.Parse(InputUloha20KatCis.Text);
                Uloha20List.ItemsSource = AfrikaKataster.Uloha20Nehnutelnosti(cisloKatastru,cisloListu);
            }
        }

        private void Uloha20Zmaz_Click(object sender, RoutedEventArgs e)
        {
            var cisloListu = int.Parse(InputUloha20ListCis.Text);
            var cisloKatastru = int.Parse(InputUloha20KatCis.Text);
            var supisCislo = int.Parse(InputUloha20SupisCislo.Text);
            var odpoved = AfrikaKataster.Uloha20(supisCislo,cisloListu,cisloKatastru);
            if (odpoved == "OK")
            {
                Uloha20List.ItemsSource = AfrikaKataster.Uloha20Nehnutelnosti(cisloKatastru, cisloListu);
            }
            else {
                MessageBox.Show(odpoved, "Err");
            }

        }

        private void ButtonUloha22_Click(object sender, RoutedEventArgs e)
        {
            AktivnyPanel(Uloha22Panel);
        }

        private void InputUloha22CisloKatNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var cisloKata = int.Parse(InputUloha22CisloKatNew.Text);
                Uloha22List.ItemsSource = AfrikaKataster.Uloha22Info(cisloKata);
            }
        }

        private void InputUloha22CisloKatDel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var cisloKata = int.Parse(InputUloha22CisloKatDel.Text);
                Uloha22List.ItemsSource = AfrikaKataster.Uloha22Info(cisloKata);
            }
        }

        private void Uloha22Zmaz_Click(object sender, RoutedEventArgs e)
        {
            var cisloKataNew = int.Parse(InputUloha22CisloKatNew.Text);
            var cisloKataDel = int.Parse(InputUloha22CisloKatDel.Text);
            Uloha22List.ItemsSource = AfrikaKataster.Uloha22(cisloKataDel, cisloKataNew);
        }

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VsetkoKatastre.ItemsSource = AfrikaKataster.Uloha15();
            VsetkoObcaniaKatastra.ItemsSource = AfrikaKataster.VsetkoObyvatelstvo();
            AktivnyPanel(UlohVsetkoPanel);
        }

        private void VsetkoKataster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    var cisKatastra = int.Parse(VsetkoKataster.Text);                   
                    VsetkoListyKatastra.ItemsSource = AfrikaKataster.Uloha19CisloKat(cisKatastra);
                    VsetkoNehnutelnostiKatastra.ItemsSource = AfrikaKataster.UlohaVsetkoNehnutelnosti(cisKatastra);                 
                }
                catch (Exception)
                {
                    MessageBox.Show("Zle zadany kataster cislo", "Err");
                }
            }
            
        
        }

        private void VsetkoListID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    var cisKatastra = int.Parse(VsetkoKataster.Text);
                    var idListu = int.Parse(VsetkoListID.Text);
                    VsetkoVlastnikove.ItemsSource = AfrikaKataster.VsetkoVlastniciListu(cisKatastra,idListu);
                    VsetkoListyNehnutelnosti.ItemsSource = AfrikaKataster.Uloha20Nehnutelnosti(cisKatastra,idListu);
                }
                catch (Exception)
                {
                    MessageBox.Show("Zle zadane udaje", "Err");
                }
            }
        }

        private void VsetkoRodCislo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                   
                    var rodCislo = VsetkoRodCislo.Text;
                    VsetkoListyKatastra.ItemsSource = AfrikaKataster.VsetkyListyObcana(rodCislo);
                    VsetkoVlastnikove.ItemsSource = AfrikaKataster.Uloha2(rodCislo);
                    VsetkoListyNehnutelnosti.ItemsSource = AfrikaKataster.VsetkoCoVlastniObcanNeh(rodCislo);
                }
                catch (Exception)
                {
                    MessageBox.Show("Zle zadane udaje", "Err");
                }
            }
        }

        private void VsetkoObcaniaKatastra_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                VsetkoRodCislo.Text = VsetkoObcaniaKatastra.SelectedItem.ToString().Substring(13, 16);
            }
            catch (Exception)
            {

                
            }
            
        }
    }
    }

