using Kit.WPF.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static Kit.WPF.Extensions.Extensiones;
namespace Inventario.Views
{
    /// <summary>
    /// Lógica de interacción para AltaProductos.xaml
    /// </summary>
    public partial class AltaProductos : ObservableUserControl
    {
        private Producto _Producto;
        public Producto Producto { get => _Producto; set { _Producto = value; OnPropertyChanged(); } }
        public AltaProductos()
        {
            Producto = new Producto();
            InitializeComponent();
            CargarCombos();
        }

        private void CargarCombos()
        {
            CmbxCodigo.ItemsSource = Conexion.Sqlite.Lista<string>("SELECT CODIGO FROM PRODUCTOS WHERE OCULTO=0");
            CmbxProveedor.ItemsSource = Conexion.Sqlite.Lista<string>("SELECT PROVEDOR FROM PRODUCTOS WHERE OCULTO=0");
            CmbxCategoria.ItemsSource = Conexion.Sqlite.Lista<string>("SELECT CLASIFICACION FROM PRODUCTOS WHERE OCULTO=0");
        }

        private void Imagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (abrir.ShowDialog() ?? false)
            {
                byte[] imagen = File.ReadAllBytes(abrir.FileName);
                Producto.Imagen =imagen.BytesToBitmap();
            }

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if(!Producto.Validar())
            {
                return;
            }
            if (Producto.Existe())
            {
                Producto.Modificacion();
            }
            else
            {
                Producto.Alta();
            }

            Producto = new Producto();
            CargarCombos();
        }

        private void CmbxCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string seleccion = CmbxCodigo.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(seleccion))
            {
                Producto = new Producto();
                return;
            }
            Producto = Inventario.Producto.Obtener(seleccion);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea eliminar el producto '" + Producto.Nombre + "'?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Producto.Baja();
                Producto = new Producto();
                CargarCombos();
            }
        }

        private void CmbxCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CmbxCodigo.Text != CmbxCodigo.SelectedValue?.ToString())
            {
                CmbxCodigo.SelectedValue = CmbxCodigo.Text;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Buscador b = new Buscador();
            b.ShowDialog();
            if (b.Seleccionado != null)
            {
                this.Producto = b.Seleccionado;
            }
        }
    }
}
