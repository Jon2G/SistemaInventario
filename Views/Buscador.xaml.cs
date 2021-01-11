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
    /// Lógica de interacción para Buscador.xaml
    /// </summary>
    public partial class Buscador
    {
        public Producto Seleccionado { get; set; }
        public Buscador()
        {
            this.Owner = App.MainWindow;
            InitializeComponent();
            List<string> categorias = Conexion.Sqlite.Lista<string>("SELECT CLASIFICACION FROM PRODUCTOS WHERE OCULTO = 0");
            categorias.Insert(0, string.Empty);
            CmbxCategoria.ItemsSource = categorias;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Categoria = CmbxCategoria.Text;
            string Busqueda = TxtBusqueda.Text;
            List<Producto> productos = Producto.Buscar(Categoria, Busqueda);
            ResBusqueda.ItemsSource = productos;
        }

        private void ResBusqueda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Seleccionado = (Producto)ResBusqueda.SelectedItem;
            Close();
        }

        private void ResBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
