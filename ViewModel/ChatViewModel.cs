using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Threading; 


namespace CatalogScolarOnline.ViewModel
{
    public class ChatViewModel : BaseViewModel
    {
        private DispatcherTimer _timer;

        private ObservableCollection<ChatModel> _mesaje {  get; set; }
        public ObservableCollection<ChatModel> Mesaje
        {
            get => _mesaje;
            set
            {
                _mesaje = value;
                OnPropertyChanged(nameof(Mesaje));
            }
        }

        private readonly Online_School_CatalogEntities _catalogDataContext = new Online_School_CatalogEntities();

        private string _mesajCurent;
        public string MesajCurent
        {
            get => _mesajCurent;
            set
            {
                _mesajCurent = value;
                OnPropertyChanged(nameof(MesajCurent));
            }
        }

        private string _parinteSelectat;
        public string ParinteSelectat
        {
            get => _parinteSelectat;
            set
            {
                _parinteSelectat = value;
                OnPropertyChanged(nameof(ParinteSelectat));
                _destinatarID = (new ChatModel()).GetDestinatarIDByParinteSelectat(_parinteSelectat);
                Mesaje = (new ChatModel()).GetMesaje(_destinatarID,Session.GetClasaID());
            }
        }


        private string _diriginte;
        public string Diriginte
        {
            get => _diriginte;
            set
            {
                _diriginte = value;
                OnPropertyChanged(nameof(Diriginte));
                
            }
        }

        private List<string> _listaParinti;
        public List<string> ListaParinti
        {
            get { return _listaParinti; }
            set { _listaParinti = value; OnPropertyChanged(nameof(ListaParinti)); }
        }

        public int UtilizatorCurentID { get; set; }

        private int _destinatarID { get; set; } 
        public int DestinatarID
        {
            get => _destinatarID;
            set
            {
                _destinatarID = value;
                OnPropertyChanged(nameof(DestinatarID));
               
            }
        }


        private Visibility _parinteGridVisibillity = Visibility.Collapsed;
        private Visibility _profesorGridVisibility = Visibility.Collapsed;

        public Visibility ProfesorGridVisibility
        {
            get { return _profesorGridVisibility; }
            set
            {
                if (_profesorGridVisibility != value)
                {
                    _profesorGridVisibility = value;
                    OnPropertyChanged(nameof(ProfesorGridVisibility));
                }
            }
        }

        public Visibility ParinteGridVisibillity
        {
            get { return _parinteGridVisibillity; }
            set
            {
                if (_parinteGridVisibillity != value)
                {
                    _parinteGridVisibillity = value;
                    OnPropertyChanged(nameof(ParinteGridVisibillity));
                }
            }
        }

        public ICommand TrimiteMesajCommand { get; set; }

        private void ActualizeazaMesaje()
        {
            var mesajeNoi = (new ChatModel()).GetMesaje(_destinatarID, Session.GetClasaID());

            if (mesajeNoi.Count != Mesaje.Count)
            {
                Mesaje = mesajeNoi;
            }
        }

        public ChatViewModel()
        {
            int rol = Session.GetRol(Session.Email);
            if (rol == 1)
            {
                _parinteGridVisibillity = Visibility.Visible;
                _diriginte = (new ChatModel()).GetDiriginteByClasaID(Session.GetClasaID());
                _destinatarID = (new ChatModel()).GetUserIDByDiriginiteID(Session.GetClasaID());
                Mesaje = (new ChatModel()).GetMesaje(_destinatarID, Session.GetClasaID());
            }
            else if (rol == 2)
            {
                _profesorGridVisibility = Visibility.Visible;
            }

            _listaParinti = (new ChatModel()).GetParintiList();

            UtilizatorCurentID = (int)Session.UtilizatorID;

            TrimiteMesajCommand = new RelayCommand(TrimiteMesaj);


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(3); 
            _timer.Tick += (sender, e) => ActualizeazaMesaje();
            _timer.Start();
        }



        private void TrimiteMesaj(object parameter)
        {
            if (string.IsNullOrWhiteSpace(MesajCurent)) return;

            var mesajNou = new Mesaje
            {
                Continut = MesajCurent,
                DataTrimitere = DateTime.Now,
                ExpeditorID = UtilizatorCurentID,
                DestinatarID = _destinatarID,
                EsteCitit = false
            };

            try
            {
                _catalogDataContext.Mesajes.Add(mesajNou);
                _catalogDataContext.SaveChanges();

                var chatModelMesaj = new ChatModel
                {
                    DataTrimitere = mesajNou.DataTrimitere,
                    EsteCitit = mesajNou.EsteCitit,
                    Continut = mesajNou.Continut,
                    ExpeditorID = UtilizatorCurentID,
                    DestinatarID = mesajNou.DestinatarID
                };


                Mesaje.Add(chatModelMesaj);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la trimiterea mesajului: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            


            
            MesajCurent = string.Empty;

            Mesaje = (new ChatModel()).GetMesaje(_destinatarID, Session.GetClasaID());
        }

    }
}
