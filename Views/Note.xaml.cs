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
    /// <summary>
    /// Interaction logic for Note.xaml
    /// </summary>
    public partial class Note : Page
    {

        public GradesViewModel ViewModel { get; private set; }
        public Note()
        {
            InitializeComponent();
            ViewModel = new GradesViewModel(); 
            this.DataContext = ViewModel;
        }

        public void ReceiveEmail(string email)
        {
            // Transmiți email-ul la ViewModel
            ViewModel.SetEmail(email);
        }

    }
}
