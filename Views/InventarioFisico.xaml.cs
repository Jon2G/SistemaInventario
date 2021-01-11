using Inventario.ViewModels.InventarioFisico;
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
    /// Lógica de interacción para InventarioFisico.xaml
    /// </summary>
    public partial class InventarioFisico : ObservableUserControl
    {
        public Invis Modelo { get; set; }
        public InventarioFisico()
        {
            Modelo = new Invis();
            InitializeComponent();
        }

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            this.Modelo.ConfirmarConteo();
        }

        private void ReporteExistencia_Click(object sender, RoutedEventArgs e)
        {

            Reporte.Existencia();
        }
    }
}
