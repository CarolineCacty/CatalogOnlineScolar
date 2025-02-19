﻿using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogScolarOnline.ViewModel
{
    public class NotificariViewModel : BaseViewModel
    {
        private ObservableCollection<NotificariModel> _notificariList;
        public ObservableCollection<NotificariModel> NotificariList
        {
            get { return _notificariList; }
            set
            {
                if (_notificariList != value)
                {
                    _notificariList = value;
                    OnPropertyChanged(nameof(NotificariList));
                }
            }
        }


        private MainWindowViewModel _mainWindowViewModel {  get; set; }  
        public NotificariViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            NotificariList = (new NotificariModel()).GetNotificariList((new NotificariModel()).GetParinteID(Session.GetElevId()));
            _mainWindowViewModel.SetNumarNotificari((new NotificariModel()).GetNrNotificariNoi());
            if(_mainWindowViewModel.NumarNotificari == 0)
            {
                _mainWindowViewModel.NrNotificariVisibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                _mainWindowViewModel.NrNotificariVisibility = System.Windows.Visibility.Visible;
            }
        }

        private readonly Online_School_CatalogEntities Context = new Online_School_CatalogEntities();

        public void MarcheazaNotificarileCaCitite()
        {

            int parinteID = (new NotificariModel()).GetParinteID(Session.GetElevId());


            List<Notificari> notificariNecitite = Context.Notificaris
                .Where(n => n.EsteCitita == false && n.ParinteID == parinteID)
                .ToList();

            if (notificariNecitite.Any())
            {
                foreach (var notificare in notificariNecitite)
                {
                    notificare.EsteCitita = true;
                }

                Context.SaveChanges();
            }
        }

    }
}
