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

namespace Inventario
{
    /// <summary>
    /// Lógica de interacción para InventarioFisico.xaml
    /// </summary>
    public partial class InventarioFisico : ObservableUserControl
    {
        private Producto _Producto;
        public Producto Producto { get => _Producto; set { _Producto = value; OnPropertyChanged(); } }
        public InventarioFisico()
        {
           
            Producto = new Producto();
            InitializeComponent();
            CargarCombos();
        }

        private void CargarCombos()
        {
            CmbxCategoria.ItemsSource = Conexion.Sqlite.Lista<string>("SELECT CLASIFICACION FROM PRODUCTOS WHERE OCULTO=0");
        }

        private void CmbxCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string seleccion = CmbxCategoria.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(seleccion))
            {
                Producto = new Producto();
                return;
            }
            Producto = Inventario.Producto.Obtener(seleccion);
        }

        private void BtnBuscador_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
