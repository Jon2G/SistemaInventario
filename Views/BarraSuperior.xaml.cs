﻿using System;
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
    /// Interaction logic for BarraSuperior.xaml
    /// </summary>
    public partial class BarraSuperior : UserControl
    {
        public BarraSuperior()
        {
            InitializeComponent();
            if (!Kit.Tools.Instance.IsInDesingMode)
            {
                Advertencias.Content = Alerta.ObtenerAlerta();
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Alertas alertas = new Alertas();
            alertas.ShowDialog();
        }
        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            switch (App.MainWindow.Contenido.Content)
            {
                case PantallaPrincipal pantalla:
                    pantalla.CCerrar(sender, e);
                    return;
                case EntradasSalidas entradasSalidas:
                    if (!await entradasSalidas.PuedeCerrar())
                    {
                        return;
                    }
                    break;
            }
            App.MainWindow.Navigate(new PantallaPrincipal());
        }
    }
}
