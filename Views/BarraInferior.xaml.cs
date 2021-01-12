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

        public void DeterminarRol()
        {
            if (App.Usuario.SoloLectura)
            {
                LblRol.Content = "Solo lectura";
            }
            else if (App.Usuario.PEntrada && App.Usuario.PSalida && App.Usuario.PReportes && App.Usuario.PUsuarios)
            {
                LblRol.Content = "Control total";
            }
            else if (!App.Usuario.PSalida && App.Usuario.PEntrada && App.Usuario.PReportes && App.Usuario.PUsuarios)
            {
                LblRol.Content = "Entradas y reportes";
            }
            else if (!App.Usuario.PEntrada && App.Usuario.PSalida && App.Usuario.PReportes && App.Usuario.PUsuarios)
            {
                LblRol.Content = "Salidas y reportes";
            }
            else if (!App.Usuario.PEntrada && !App.Usuario.PSalida && App.Usuario.PReportes && App.Usuario.PUsuarios)
            {
                LblRol.Content = "Solo reportes";
            }
            else if (!App.Usuario.PEntrada && !App.Usuario.PSalida && !App.Usuario.PReportes && App.Usuario.PUsuarios)
            {
                LblRol.Content = "Solo usuarios";
            }
            else if (App.Usuario.PReportes)
            {
                LblRol.Content = "Solo lectura";
            }
            else if (!App.Usuario.PEntrada && !App.Usuario.PSalida && !App.Usuario.PReportes && !App.Usuario.PUsuarios)
            {
                LblRol.Content = "Usuario deshabilitado";
            }
            else
            {
                LblRol.Content = "";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LblFecha.Content = DateTime.Now.ToString();
        }
    }
}
