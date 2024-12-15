using CatalogScolarOnline.ViewModel;
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

namespace CatalogScolarOnline.Views
{

    public partial class ProfilulMeu : Page
    {
        public ProfilulMeuViewModel ProfilulMeuViewModel { get; set; }
        public ProfilulMeu()
        {
            InitializeComponent();
            ProfilulMeuViewModel = new ProfilulMeuViewModel();
            this.DataContext = ProfilulMeuViewModel;
        }
    }
}
