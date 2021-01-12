using Inventario.ViewModels.EntradasSalidas;
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
    /// Lógica de interacción para PantallaPrincipal.xaml
    /// </summary>
    public partial class PantallaPrincipal : ModernWpf.Controls.Page
    {
        public PantallaPrincipal()
        {
            InitializeComponent();
        }

        private void BotonExistencia_Click(object sender, RoutedEventArgs e)
        {
            //App.MainWindow.Navigate(new (LogIn));
        }

        private void CProdAlta(object sender, RoutedEventArgs e)
        {


        }

        private void CBaja(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!App.Usuario.PSalida)
            {
                MessageBox.Show("No tienes privilegio de salida.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new AltaProductos());
            }

        }

        private void CModificar(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!App.Usuario.PEntrada)
            {
                MessageBox.Show("No tienes privilegio para modificar.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new AltaProductos());
            }
        }

        private void REntradas_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana :ROLSOLOLECTURA .", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!App.Usuario.PEntrada)
            {
                MessageBox.Show("No tienes privilegio entrada.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new EntradasSalidas(Tipo.Entrada));
            }
        }
        private void RSalidas_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!App.Usuario.PSalida)
            {
                MessageBox.Show("No tienes privilegio salida.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new EntradasSalidas(Tipo.Salida));
            }
        }
        private void InventarioFisico_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!App.Usuario.PEntrada)
            {
                MessageBox.Show("No tienes privilegio entrada.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new InventarioFisico());
            }
        }

        private void Movimientos_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PReportes == false)
            {
                MessageBox.Show("No tienes privilegio de reportes.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Reporte.Movimientos();
            }
        }

        private void Entrada_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PReportes == false)
            {
                MessageBox.Show("No tienes privilegio de reportes.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Reporte.Entradas();
            }
        }
        private void Salida_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PReportes == false)
            {
                MessageBox.Show("No tienes privilegio de reportes.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Reporte.Salidas();
            }
        }
        private void Existencia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PReportes == false)
            {
                MessageBox.Show("No tienes privilegio de reportes.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Reporte.Existencia();
            }
        }

        private void Usuarios_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!App.Usuario.PUsuarios)
            {

                MessageBox.Show("No eres adminsitrador.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new Usuarios());
            }
        }

        public void LogOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.MainWindow.Navigate(new LogIn());
        }

        private void Productos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!App.Usuario.PEntrada)
            {
                MessageBox.Show("No tienes privilegio de entrada.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new AltaProductos());
            }
        }
    }
}
