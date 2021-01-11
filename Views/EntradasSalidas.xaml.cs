using Inventario.ViewModels.EntradasSalidas;
using Kit.Enums;
using Kit.WPF.Controls;
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
    /// Interaction logic for EntradasSalidas.xaml
    /// </summary>
    public partial class EntradasSalidas
    {

        public EntradaSalida ModeloEntradaSalida { get; set; }
        public EntradasSalidas(Tipo Tipo)
        {
            this.ModeloEntradaSalida = new EntradaSalida(Tipo);
            InitializeComponent();
            DataContext = this;
        }

        private void CantidadCambio(object sender, RoutedEventArgs e)
        {
            //AjusteInventario pointer = (sender as TextBox)?.DataContext as AjusteInventario;
            //if (sender is TextBox text)
            //{
            //    decimal.TryParse(text.Text, out decimal Cantidad);
            //    if ((float)Cantidad != pointer.Cantidad)
            //    {
            //        pointer.Cantidad = (float)Cantidad;
            //        pointer.ExistenciaPosterior = pointer.ExistenciaActual + pointer.Cantidad;
            //    }
            //}


        }


        private void EliminarPartida(object sender, RoutedEventArgs e)
        {
            AjusteInventario pointer = (sender as Button)?.DataContext as AjusteInventario;
            ModeloEntradaSalida.Ajustes.Remove(pointer);
        }
        private void Buscame(object sender, RoutedEventArgs e)
        {
            //  ModeloEntradaSalida.Seleccion =?
        }
        private void Agregar(object sender, RoutedEventArgs e)
        {
            if (this.ModeloEntradaSalida.Seleccion is null)
            {
                Kit.Services.CustomMessageBox.Current.Show("Seleccione un producto primero", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                CmbxArticulos.Focus();
                CmbxArticulos.IsDropDownOpen = true;
                return;
            }
            this.ModeloEntradaSalida.Agregar();
        }
        private void Finalizar(object sender, RoutedEventArgs e)
        {
            if (!this.ModeloEntradaSalida.Ajustes.Any(x => x.Cantidad > 0))
            {
                Kit.Services.CustomMessageBox.Current.Show("No puede finalizar este ajuste por no tener partidas", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                return;
            }

            if (this.ModeloEntradaSalida.Concepto is null)
            {
                Kit.Services.CustomMessageBox.Current.Show("Es necesario el concepto del movimiento de inventario", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                CmbxConcepto.Focus();
                CmbxConcepto.IsDropDownOpen = true;
                return;
            }

            this.ModeloEntradaSalida.Finalizar();
            App.MainWindow.Navigate(new PantallaPrincipal());
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox pointTextBox = (sender as TextBox);
            if (!pointTextBox.IsFocused)
            {
                pointTextBox.Focus();
            }
            if (pointTextBox.Text == "0.00")
            {
                pointTextBox.Text = string.Empty;
                pointTextBox.Focus();
            }
            else
            {
                pointTextBox.SelectAll();
            }
        }
        public async Task<bool> PuedeCerrar()
        {
            if (this.ModeloEntradaSalida.Ajustes.Any())
            {
                if (await Kit.Services.CustomMessageBox.Current.ShowOKCancel("¿Está seguro que quiere salir sin guardar?", "Salir", "Si,descartar", "Cancelar", CustomMessageBoxImage.Question) != CustomMessageBoxResult.OK)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
