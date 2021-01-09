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
    public partial class PantallaPrincipal : UserControl
    {
        public PantallaPrincipal()
        {
            //Aqui no existe el txt
            InitializeComponent();//Inicializa la interfaz de usuario
                                  //Aqui ya :D
            this.TxbUsu.Text = "¡Bienvenido," + App.Usuario.Nombre + "!";
        }

        private void BotonExistencia_Click(object sender, RoutedEventArgs e)
        {
            //App.MainWindow.Navigate(new (LogIn));
        }

        private void CProdAlta(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PEntrada == false)
            {
                MessageBox.Show("No tienes privilegio de entrada.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new AltaProductos());
                App.MainWindow.MostrarBarras(false);
            }

        }

        private void CBaja(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PSalida  == false)
            {
                MessageBox.Show("No tienes privilegio de salida.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new AltaProductos());
                App.MainWindow.MostrarBarras(false);
            }

        }

        private void CModificar(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PEntrada == false)
            {
                MessageBox.Show("No tienes privilegio para modificar.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new AltaProductos());
                App.MainWindow.MostrarBarras(false);
            }
        }

        private void CEntrada(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana :ROLSOLOLECTURA .", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PEntrada == false)
            {
                MessageBox.Show("No tienes privilegio entrada.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //App.MainWindow.Navigate(new (LogIn));
            }
        }
        private void CSalida(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PSalida == false)
            {
                MessageBox.Show("No tienes privilegio salida.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //App.MainWindow.Navigate(new (LogIn));
            }
        }
        private void CIFisico(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PEntrada == false)
            {
                MessageBox.Show("No tienes privilegio entrada.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new InventarioFisico());
                App.MainWindow.MostrarBarras(false);
            }
        }

        private void CMovimientos(object sender, RoutedEventArgs e)
        {
            FechasReporte reporte = new FechasReporte();
            reporte.ShowDialog();
            Reporte.Movimiento(reporte.FechaInicial, reporte.FechaFinal);
        }

        private void CEntradas(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PReportes == false)
            {
                MessageBox.Show("No tienes privilegio de reportes.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //App.MainWindow.Navigate(new (LogIn));
            }
        }
        private void CSalidas(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PReportes == false)
            {
                MessageBox.Show("No tienes privilegio de reportes.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //App.MainWindow.Navigate(new (LogIn));
            }
        }
        private void CExistencia(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura == true )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.PReportes == false)
            {
                MessageBox.Show("No tienes privilegio de reportes.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                FechasReporte reporte = new FechasReporte();
                reporte.ShowDialog();
                Reporte.Existencia(reporte.FechaInicial, reporte.FechaFinal);
            }
        }

        private void CUsuarios(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura )
            {
                MessageBox.Show("No tienes permiso para acceder a esta ventana ROLSOLOLECTURA.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.Usuario.EDUSUARIO==false)
            {

                MessageBox.Show("No eres adminsitrador.", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.MainWindow.Navigate(new Usuarios());
                App.MainWindow.MostrarBarras(false);
            }
        }

        public void CCerrar(object sender, RoutedEventArgs e)
        {
            App.MainWindow.Navigate(new LogIn());
        }
    }
}
