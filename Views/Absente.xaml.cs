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
using CatalogScolarOnline.ViewModel;

namespace CatalogScolarOnline.Views
{
    /// <summary>
    /// Interaction logic for Absente.xaml
    /// </summary>
    public partial class Absente : Page
    {
        public AbsenteViewModel absenteViewModel { get; set; }
        public Absente()
        {
            InitializeComponent();
            absenteViewModel = new AbsenteViewModel();
            this.DataContext = absenteViewModel;
        }
    }
}
