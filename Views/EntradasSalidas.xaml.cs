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
            Cargar();
            this.DtFecha.Text= DateTime.Today.ToString();
        }

        private void CantidadCambio(object sender, RoutedEventArgs e)
        {
            AjusteInventario pointer = (sender as TextBox)?.DataContext as AjusteInventario;
            double.TryParse((sender as TextBox)?.Text, out double Cantidad);
            if (Cantidad != pointer.Cantidad)
            {
                pointer.Cantidad = Cantidad;
                pointer.InventarioF = pointer.Inventario + pointer.Cantidad;
                pointer.Importe = pointer.Precio * pointer.Cantidad;
            }
        }

        private void PrecioCambio(object sender, RoutedEventArgs e)
        {
            AjusteInventario pointer = (sender as TextBox)?.DataContext as AjusteInventario;
            double.TryParse((sender as TextBox)?.Text, out double Precio);
            if (Precio != pointer.Precio)
            {
                pointer.Precio = Precio;
                pointer.Importe = pointer.Precio * pointer.Cantidad;
            }
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
            double inventarioActual = Producto.ObtenerExistencia(ModeloEntradaSalida.Seleccion.Id);
            double.TryParse(TxtCantidad.Text, out double Cantidad);
            double.TryParse(TxtPrecio.Text, out double Precio);
            this.ModeloEntradaSalida.Ajustes.Add(new AjusteInventario()
            {
                Articulo = ModeloEntradaSalida.Seleccion.Codigo,
                Cantidad = Cantidad,
                Descripcion = ModeloEntradaSalida.Seleccion.Nombre,
                Importe = Cantidad * Precio,
                Precio = Precio,
                Inventario = inventarioActual,
                InventarioF = inventarioActual + Cantidad
            });
            ModeloEntradaSalida.Seleccion = null;
            TxtCantidad.Text =
                TxtPrecio.Text = string.Empty;
        }
        private void Finalizar(object sender, RoutedEventArgs e)
        {
            if (!this.ModeloEntradaSalida.Ajustes.Any(x => x.Cantidad > 0))
            {
                Kit.Services.CustomMessageBox.Current.Show("No puede finalizar este ajuste por no tener partidas", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                return;
            }

            if (CmbxConcepto.SelectedItem is null)
            {
                Kit.Services.CustomMessageBox.Current.Show("Es necesario el concepto del movimiento de inventario", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                CmbxConcepto.Focus();
                CmbxConcepto.IsDropDownOpen = true;
                return;
            }

            Concepto concepto = (Concepto)CmbxConcepto.SelectedItem;
            double importe = (this.ModeloEntradaSalida.Ajustes.Sum(x => x.Importe));
            string observaciones = TxtObservaciones.Text.Trim();

            bool imprime = (bool)ChkImprimir.IsChecked;

            this.ModeloEntradaSalida.Guardar(concepto, importe, observaciones, imprime);
            App.MainWindow.Navigate(new PantallaPrincipal());
        }
        private void Cargar()
        {
            //TxtCantidad.TextChanged += Validaciones.TextBox_ValidarCantidadRegEx;
            //TxtPrecio.TextChanged += Validaciones.TextBox_ValidarCantidadRegEx;
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
        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.ModeloEntradaSalida.Ajustes.Any())
            {
                if (await Kit.Services.CustomMessageBox.Current.ShowOKCancel("¿Está seguro que quiere salir sin guardar?", "Salir", "Si,descartar", "Cancelar", CustomMessageBoxImage.Question) == CustomMessageBoxResult.OK)
                {
                    return;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
