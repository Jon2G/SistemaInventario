using Kit.WPF.Controls;
using Kit.WPF.Controls.RangoFechas;
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
    public partial class FechasReporte : CloseReasonWindow
    {
        public Rango Rango { get; private set; }
        public FechasReporte()
        {
            Owner = App.MainWindow;
            InitializeComponent();
            this.SelectorFechas.Rango.TodasLasFechas = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            this.Rango = SelectorFechas.Rango;
            this.Rango.Cancelado = this.CloseReason == ECloseReason.SystemMenuClosedByUser;
            if (!this.Rango.TodasLasFechas && !this.Rango.Cancelado)
            {
                this.Rango.Inicio = ((DateTime)this.Rango.Inicio).AddHours(23).AddMinutes(59);
                this.Rango.Fin = ((DateTime)this.Rango.Fin).AddHours(23).AddMinutes(59);
            }

            base.OnClosed(e);

        }
    }
}
