using Inventario.ViewModels.EntradasSalidas;
using Kit.Enums;
using Kit.WPF.Services.ICustomMessageBox;
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
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            App.MainWindow.RecargarAlertas();
            this.TablaAlertas.RecargarAlertas();
        }
        private async void REntradas_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (!App.Usuario.PEntrada)
            {
                CustomMessageBox.Show("No tienes privilegio de Entradas", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                App.MainWindow.Navigate(new EntradasSalidas(Tipo.Entrada));
            }
        }
        private async void RSalidas_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (!App.Usuario.PSalida)
            {
                CustomMessageBox.Show("No tienes privilegio de Salidas", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                App.MainWindow.Navigate(new EntradasSalidas(Tipo.Salida));
            }
        }
        private async void InventarioFisico_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (!App.Usuario.PEntrada)
            {
                CustomMessageBox.Show("No tienes privilegio de Entradas", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                App.MainWindow.Navigate(new InventarioFisico());
            }
        }
        private async void Movimientos_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (App.Usuario.PReportes == false)
            {
                CustomMessageBox.Show("No tienes privilegio de Reportes", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                Reporte.Movimientos();
            }
        }
        private async void Entrada_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (App.Usuario.PReportes == false)
            {
                CustomMessageBox.Show("No tienes privilegio de Reportes", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                Reporte.Entradas();
            }
        }
        private async void Salida_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (App.Usuario.PReportes == false)
            {
                CustomMessageBox.Show("No tienes privilegio de Reportes", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                Reporte.Salidas();
            }
        }
        private async void Existencia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (App.Usuario.PReportes == false)
            {
                CustomMessageBox.Show("No tienes privilegio de Reportes", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                Reporte.Existencia();
            }
        }
        private async void Usuarios_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!App.Usuario.PUsuarios)
            {

                CustomMessageBox.Show("No eres administrador", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
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
        private async void Productos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Usuario.SoloLectura)
            {
                CustomMessageBox.Show("No tienes permiso para acceder a esta ventana SOLO LECTURA", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else if (!App.Usuario.PEntrada)
            {
                CustomMessageBox.Show("No tienes privilegio de Entradas", "Imposible continuar", CustomMessageBoxButton.OK, CustomMessageBoxImage.Error);
                return;
            }
            else
            {
                App.MainWindow.Navigate(new AltaProductos());
            }
        }
    }
}
