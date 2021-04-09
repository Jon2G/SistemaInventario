using Inventario.ViewModels.EntradasSalidas;
using Kit.Enums;
using Kit.WPF.Controls;
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
    /// Interaction logic for EntradasSalidas.xaml
    /// </summary>
    public partial class EntradasSalidas
    {
        public EntradaSalida ModeloEntradaSalida
        {
            get => _ModeloEntradaSalida;
            set
            {
                _ModeloEntradaSalida = value;
                OnPropertyChanged();
            }
        }
        private EntradaSalida _ModeloEntradaSalida;
        public EntradasSalidas(Tipo Tipo)
        {
            this.ModeloEntradaSalida = new EntradaSalida(Tipo);
            InitializeComponent();
            DataContext = this;
        }
        private void EliminarPartida(object sender, RoutedEventArgs e)
        {
            AjusteInventario pointer = (sender as Button)?.DataContext as AjusteInventario;
            ModeloEntradaSalida.Ajustes.Remove(pointer);
        }
        private void Buscame(object sender, RoutedEventArgs e)
        {
            Buscador buscador = new Buscador();
            buscador.ShowDialog();
            if (buscador.Seleccionado != null)
            {
                this.ModeloEntradaSalida.Seleccion = this.ModeloEntradaSalida.Productos.FirstOrDefault(X => X.Codigo == buscador.Seleccionado.Codigo);

            }
        }
        private void Agregar(object sender, RoutedEventArgs e)
        {
            if (this.ModeloEntradaSalida.Seleccion is null)
            {
                CustomMessageBox.Show("Seleccione un producto primero", "Atención",
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
                CustomMessageBox.Show("No puede finalizar este ajuste por no tener partidas", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                return;
            }

            if (this.ModeloEntradaSalida.Concepto is null)
            {
                CustomMessageBox.Show("Es necesario el concepto del movimiento de inventario", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                CmbxConcepto.Focus();
                CmbxConcepto.IsDropDownOpen = true;
                return;
            }

            this.ModeloEntradaSalida.Finalizar();
            this.ModeloEntradaSalida = new EntradaSalida(this.ModeloEntradaSalida.TipoAjuste);
        }

  
        public async Task<bool> PuedeCerrar()
        {
            if (this.ModeloEntradaSalida.Ajustes.Any())
            {
                if (CustomMessageBox.ShowOKCancel("¿Está seguro que quiere salir sin guardar?", "Salir", "Si,descartar", "Cancelar", CustomMessageBoxImage.Question) != CustomMessageBoxResult.OK)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
