﻿using Kit.WPF.Controls;

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
using Kit.WPF.Dialogs.ICustomMessageBox;
using Kit;

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
            CmbxProveedor.ItemsSource = Producto.ListarProvedores();
            CmbxCategoria.ItemsSource = Producto.ListarCategorias();
        }

        private void Imagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (abrir.ShowDialog() ?? false)
            {
                byte[] imagen = File.ReadAllBytes(abrir.FileName);
                Producto.Imagen = imagen.BytesToBitmap();
            }

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (!Producto.Validar())
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
                TxtExistencia.IsEnabled = true;
                Producto = new Producto();
                return;
            }
            TxtExistencia.IsEnabled = false;
            Producto = Inventario.Producto.Obtener(seleccion);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo("¿Esta seguro que desea eliminar el producto '" + Producto.Nombre + "'?", "Atención", "Si,eliminar", "Cancelar", Kit.Enums.CustomMessageBoxImage.Question) == Kit.Enums.CustomMessageBoxResult.Yes)
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
