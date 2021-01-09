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
using System.Windows.Threading;

namespace Inventario.Views
{
    /// <summary>
    /// Interaction logic for BarraInferior.xaml
    /// </summary>
    public partial class BarraInferior : UserControl
    {
        public BarraInferior()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            
            

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LblFecha.Content = DateTime.Now.ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(App.MainWindow.Contenido.Content is PantallaPrincipal pantalla)
            {
                pantalla.CCerrar(sender, e);
                return;
            }
            App.MainWindow.Navigate(new PantallaPrincipal());
        }
    }
}
