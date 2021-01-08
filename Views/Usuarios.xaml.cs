using Kit.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace Inventario.Views
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : ObservableUserControl
    {
        public Usuario Modelo
        {
            get => _Modelo;
            set
            {
                _Modelo = value;
                OnPropertyChanged();
            }
        }
        private Usuario _Modelo;
        private ObservableCollection<Usuario> _ListaUsuarios;
        public ObservableCollection<Usuario> ListaUsuarios { get => _ListaUsuarios; set { _ListaUsuarios = value; OnPropertyChanged(); } }
        public Usuarios()
        {
            Modelo = new Usuario();

            InitializeComponent();
            Recargar();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Password = Password.Password;
            if (Modelo.Existe())
            {
                Modelo.Modificacion();
            }
            else
            {
                Modelo.Alta();
            }
            Recargar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está segur@ de eliminar a este usuario?.\nEsta acción no puede deshacerse,todos los movimientos asociados al usuario permaneceran asociados.\nEl usuario no podrá ingresar al sistema.", "Eliminar usuario", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Modelo.Baja();
                Recargar();
            }
        }
        public void Recargar()
        {
            this.ListaUsuarios = Usuario.Listar();
            Modelo = new Usuario();
            Password.Password = "";
            TxTNickName.IsEnabled = true;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaUsers.SelectedItem is null)
            {
                return;
            }
            Modelo = (Usuario)ListaUsers.SelectedItem;
            Password.Password = Modelo.Password;
            TxTNickName.IsEnabled = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Modelo = new Usuario();
            Password.Password = "";
            TxTNickName.IsEnabled = true;
        }
    }
}
