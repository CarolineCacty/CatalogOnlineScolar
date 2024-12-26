using CatalogScolarOnline.Utilities;
using CatalogScolarOnline.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CatalogScolarOnline.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        public ICommand NavigateToInsertContCommand {  get; }
        public ICommand CloseCommand { get; }
        public ICommand NavigateToInsertClaseCommand { get; set; }
        public ICommand NavigateToInsertMaterieCommand { get; set; }
        public ICommand NavigateToInsertPredareCommand { get; set; }
        public ICommand NavigateToInsertNoteCommand { get; set; }
        public ICommand NavigateToInsertAbsenteCommand { get; set; }
        public ICommand NavigateToInsertOrarCommand { get; set; }


        public ICommand LoginWindow { get; }
        public AdminViewModel()
        {
            LoginWindow = new RelayCommand(LoginWindowShow);
            NavigateToInsertContCommand = new RelayCommand(NavigateInsertCont);
            CloseCommand = new RelayCommand(CloseApplication);
            NavigateToInsertClaseCommand = new RelayCommand(NavigateToInsertClase);
            NavigateToInsertMaterieCommand = new RelayCommand(NavigateToInsertMaterie);
            NavigateToInsertPredareCommand = new RelayCommand(NavigateToInsertPredare);
            NavigateToInsertNoteCommand = new RelayCommand(NavigateToInsertNote);
            NavigateToInsertAbsenteCommand = new RelayCommand(NavigateToInsertAbsente);
            NavigateToInsertOrarCommand = new RelayCommand(NavigateToInsertOrar);
        }

        public void NavigateToInsertOrar(object parameter)
        {
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var insertOrarPage = new Views.InsertOrar();
                adminWindow.MainFrame.Navigate(insertOrarPage);
            }
        }
        private void NavigateToInsertAbsente(object parameter)
        {
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var insertAbsentePage = new Views.InsertAbsente();
                adminWindow.MainFrame.Navigate(insertAbsentePage);
            }
        }
        private void NavigateToInsertNote(object parameter)
        {
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var insertNotePage = new Views.InsertNote();
                adminWindow.MainFrame.Navigate(insertNotePage);
            }
        }

        private void LoginWindowShow(object parameter)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            Login login = new Login();
            login.Show();

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        public void NavigateToInsertPredare(object sender)
        {
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var insertPredarePage = new Views.InsertPredare();
                adminWindow.MainFrame.Navigate(insertPredarePage);
            }
        }
        public void NavigateToInsertMaterie(object sender)
        {
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var insertMateriePage = new Views.InsertMaterie();
                adminWindow.MainFrame.Navigate(insertMateriePage);
            }
        }
        private void CloseApplication(object parameter)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            currentWindow?.Close();
        }
        private void NavigateInsertCont(object parameter)
        {
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var insertConturiPage = new Views.InsertConturi();
                adminWindow.MainFrame.Navigate(insertConturiPage);
            }
        }
        private void NavigateToInsertClase(object parameter)
        {
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
            {
                var insertClasePage = new Views.InsertClase();
                adminWindow.MainFrame.Navigate(insertClasePage);
            }
            
        }
    }
}
