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
using System.Windows.Shapes;

namespace CatalogScolarOnline.Views
{
    public partial class AdminWindow : Window
    {
        public AdminViewModel ViewModel { get; private set; }
        public AdminWindow()
        {
            InitializeComponent();
            ViewModel = new AdminViewModel();
            this.DataContext = ViewModel;
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var acasaPage = new Views.Acasa();
                adminWindow.MainFrame.Navigate(acasaPage);
            }
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

        private void Acasa_Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Acasa());
        }
    }
}
