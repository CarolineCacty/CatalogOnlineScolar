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
using CatalogScolarOnline.Model;
using CatalogScolarOnline.ViewModel;
using CatalogScolarOnline.Views;
using CatalogScolarOnline.Utilities;
using Hardcodet.Wpf;
using Flattinger.UI.ToastMessage.Controls;

namespace CatalogScolarOnline.Views
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            this.DataContext = ViewModel;
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var acasaPage = new Views.Acasa();
                mainWindow.MainFrame.Navigate(acasaPage);
            }
        }

        public MainWindow(string email)
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel(email);
            this.DataContext = ViewModel;
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var acasaPage = new Views.Acasa();
                mainWindow.MainFrame.Navigate(acasaPage);
            }

        }

        public void ReceiveEmail(string email)
        {
            ViewModel.SetEmail(email);
            Session.Email = email;
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
        private void Note_Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Note());
        }

    }
}
