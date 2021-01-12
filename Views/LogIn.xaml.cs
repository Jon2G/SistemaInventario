using Kit.Enums;
using Kit.Security.Encryption;
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
using Kit.Services;

namespace Inventario.Views
{
    /// <summary>
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {
        public LogIn()
        {
            App.MainWindow.MostrarBarras(false, false);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsuario.Text;
            string password = TxtPassword.Password;
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(TxtPassword.Password))
            {
                CustomMessageBox.Current.Show("Intento de inicio sesión incorrecto.", "Acceso denegado", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }

            Usuario usu = Usuario.Obtener(usuario);

            if (usu is null)
            {
                CustomMessageBox.Current.Show("Usuario no encontrado", "Acceso denegado", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);

            }
            else if (usu.Password != password)
            {
                CustomMessageBox.Current.Show("Contraseña incorrecta", "Acceso denegado", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
            }
            else
            {
                App.Usuario = usu;
                App.MainWindow.Navigate(new PantallaPrincipal());
                App.MainWindow.BarraInferior.DeterminarRol();

                App.MainWindow.MostrarBarras(true);
            }

        }


    }
}
