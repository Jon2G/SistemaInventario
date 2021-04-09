using Kit.WPF.Controls;
using LiveCharts;
using LiveCharts.Wpf;
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
    /// Interaction logic for GraficaPrincipal.xaml
    /// </summary>
    public partial class GraficaPrincipal : ObservableUserControl
    {
        private SeriesCollection _SeriesGrafica;
        public SeriesCollection SeriesGrafica
        {
            get => _SeriesGrafica;
            private set
            {
                _SeriesGrafica = value;
                OnPropertyChanged();
            }
        }
        private AxesCollection _AxisXGrafica;
        public AxesCollection AxisXGrafica
        {
            get => _AxisXGrafica;
            private set
            {
                _AxisXGrafica = value;
                OnPropertyChanged();
            }
        }
        private TimeSpan Periodo;
        private DispatcherTimer Clock;
        public GraficaPrincipal()
        {
            InitializeComponent();
            this.Loaded += GraficaPrincipal_Loaded;

        }

        private void GraficaPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            if(Kit.Tools.IsInited)
            Init();
        }

        private async void Init()
        {
            this.Periodo = TimeSpan.FromMinutes(1);
            this.Clock = new DispatcherTimer();
            this.Clock.Interval = this.Periodo;
            this.Clock.Tick += Clock_Tick;
            this.SeriesGrafica = new SeriesCollection{
                        new LineSeries{Title="Cantidad de movimientos",
                       Values =new ChartValues<double>()
                        }
                    };
            this.AxisXGrafica = new AxesCollection
                    {
                        new Axis
                        {
                            Labels = new string[]{"" },
                            Separator = new LiveCharts.Wpf.Separator { IsEnabled =true, Step =2 }
                        }
                    };
            await Grafica();
            this.Clock.Start();
        }

        private async void Clock_Tick(object sender, EventArgs e)
        {
            this.Clock.Stop();
            await Grafica();
            this.Clock.Start();
        }
        private async Task Grafica()
        {
            await Task.Yield();
            string[] xy = new string[31];
            double[] xyfixed = new double[31];
            int i = 0;
            using (var reader = Conexion.Sqlite.Read(@"SELECT EXISTENCIA_ACTUAL,FECHA FROM MOVIMIENTOS ORDER BY ID ASC"))
            {
                while (reader.Read())
                {
                    xyfixed[i] = Convert.ToDouble(reader[0]);
                    xy[i] = Convert.ToDateTime(reader[1]).ToShortDateString();
                    i++;
                }
            }
            Array.Resize<double>(ref xyfixed, i);
            Array.Resize<string>(ref xy, i);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                this.SeriesGrafica.Clear();
                this.SeriesGrafica.Add(new LineSeries
                {
                    Title = "Valor de movimiento",
                    Values = new ChartValues<double>(xyfixed)
                });
                this.AxisXGrafica.Clear();
                this.AxisXGrafica.Add(new Axis()
                {
                    Labels = xy
                });
                //OnPropertyChanged(nameof(SeriesGrafica));
                //OnPropertyChanged(nameof(AxisXGrafica));
            });


            //this.SeriesGrafica = new SeriesCollection{
            //            new LineSeries{Title="Importe de la venta",
            //           Values =new ChartValues<double>(xyfixed)
            //            }
            //        };
            //this.AxisXGrafica = new AxesCollection
            //        {
            //            new Axis
            //            {
            //                Labels = xy,
            //                Separator = new LiveCharts.Wpf.Separator { IsEnabled =true, Step =2 }
            //            }
            //        };

        }
    }
}
