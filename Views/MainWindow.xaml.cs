
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Inventario.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            App.MainWindow = this;
            InitializeComponent();
            
            Navigate(new LogIn());
        }

        public void Navigate(UserControl ventana)
        {
            this.Contenido.Navigate(ventana);
        }
        public void Navigate(Page page)
        {
            this.Contenido.Navigate(page);
        }
        public void MostrarBarras(bool BarraSuperior, bool BarraInferior = true)
        {

            //this.BarraInferior.Visibility = BarraInferior ? Visibility.Visible : Visibility.Collapsed;
            //this.BarraSuperior.Visibility = BarraSuperior ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
