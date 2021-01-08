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
    /// Interaction logic for BarraInferior.xaml
    /// </summary>
    public partial class BarraInferior : UserControl
    {
        public BarraInferior()
        {
            InitializeComponent();
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
