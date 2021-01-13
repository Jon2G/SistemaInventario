using Kit.Enums;
using Kit.WPF.Controls;
using Microsoft.Win32;
using SQLHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using static Kit.WPF.Extensions.Extensiones;

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
            Password.Password = Password.Password?.Trim();
            this.Modelo.NickName = this.Modelo.NickName?.Trim();
            this.Modelo.Nombre = this.Modelo.Nombre?.Trim();

            if (string.IsNullOrEmpty(this.Modelo.Nombre))
            {
                Kit.Services.CustomMessageBox.Current.Show("El campo nombre no puede estar vacio", "Atención ", CustomMessageBoxButton.OK, CustomMessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.Modelo.NickName))
            {
                Kit.Services.CustomMessageBox.Current.Show("El campo usuario no puede estar vacio", "Atención ", CustomMessageBoxButton.OK, CustomMessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(Password.Password))
            {
                Kit.Services.CustomMessageBox.Current.Show("El campo contraseña no puede estar vacio", "Atención ", CustomMessageBoxButton.OK, CustomMessageBoxImage.Warning);
                return;
            }

            Modelo.Password = Password.Password;
            if (Modelo.Existe())
            {
                Modelo.Modificacion();
                if (Modelo.NickName == App.Usuario.NickName)
                {
                    App.Usuario = Modelo;
                }
            }
            else
            {
                Modelo.Alta();

            }
            Recargar();


        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SQLH.IsInjection(Modelo.NickName))
            {
                await Kit.Services.CustomMessageBox.Current.Show("Intento de baja invalido", "Atención", CustomMessageBoxButton.OK, CustomMessageBoxImage.Warning);
                return;
            }
            if (!Conexion.Sqlite.Exists("SELECT NICKNAME FROM USUARIOS WHERE NICKNAME != '" + Modelo.NickName + "' AND OCULTO=0 "))
            {
                await Kit.Services.CustomMessageBox.Current.Show("Si elimina a este usuario perderá acceso al sistema.\nDebe haber al menos un usuario registrado.", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            if (await Kit.Services.CustomMessageBox.Current.ShowYesNo("¿Está segur@ de eliminar a este usuario?.\nEsta acción no puede deshacerse,todos los movimientos asociados al usuario se conservarán.\nEl usuario no podrá ingresar al sistema.", "Eliminar usuario","Sí, eliminar","Cancelar", CustomMessageBoxImage.Warning) == CustomMessageBoxResult.Yes)
            {
                Modelo.Baja();
                if (App.Usuario.NickName == Modelo.NickName)
                {
                    App.MainWindow.Navigate(new LogIn());
                    return;
                }
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
        private void Imagen_Click(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (abrir.ShowDialog() ?? false)
            {
                byte[] imagen = File.ReadAllBytes(abrir.FileName);
                Modelo.Imagen = imagen.ByteToImage();
            }

        }
    }
}
