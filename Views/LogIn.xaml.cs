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

            Usuario usu = Usuario.Obtener(usuario);

            if (usu is null)
            {
                MessageBox.Show("Usuario no encontrado", "Acceso denegado", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (usu.Password != password)
            {
                MessageBox.Show("Contraseña incorrecta", "Acceso denegado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                App.MainWindow.Navigate(new PantallaPrincipal());
                App.MainWindow.MostrarBarras(true);
            }

        }
    }
}
