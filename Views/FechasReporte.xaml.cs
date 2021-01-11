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
using System.Windows.Shapes;

namespace Inventario.Views
{
    /// <summary>
    /// Lógica de interacción para FechasReporte.xaml
    /// </summary>
    public partial class FechasReporte : Window
    {
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public FechasReporte()
        {
            Owner = App.MainWindow;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.FechaInicial = SelectorFechas.Rango.Inicio;
            this.FechaFinal = SelectorFechas.Rango.Fin;
            if (!SelectorFechas.Rango.TodasLasFechas)
            {
                this.FechaInicial = ((DateTime)this.FechaInicial).AddHours(23).AddMinutes(59);
                this.FechaFinal = ((DateTime)this.FechaFinal).AddHours(23).AddMinutes(59);
            }
            Close();
        }

    }
}
