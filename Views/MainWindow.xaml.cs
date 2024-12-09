using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

namespace CatalogScolarOnline
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            this.DataContext = ViewModel;

        }

        public void ReceiveEmail(string email)
        {
            // Transmiți email-ul la ViewModel
            ViewModel.SetEmail(email);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;

                }
            }
        }

    }
}
