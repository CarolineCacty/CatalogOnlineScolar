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
    public partial class Notificari : Page
    {
        public NotificariViewModel NotificariViewModel { get; set; }
        public Notificari(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();;
            NotificariViewModel = new NotificariViewModel(mainWindowViewModel);
            this.DataContext = NotificariViewModel;
        }

        private void NotificariView_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as NotificariViewModel;
            viewModel?.MarcheazaNotificarileCaCitite();
        }
    }
}
