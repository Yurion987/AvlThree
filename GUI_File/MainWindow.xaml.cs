using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Models;
namespace GUI_File
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpravujSubor TestSubor { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TestSubor = new SpravujSubor("Data");
            AktualizujView();
        }
        public void AktualizujView()
        {
            MainView.ItemsSource = TestSubor.SeqSubor();

        }
    }
}
