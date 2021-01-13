
using Prism.Regions;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

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
            this.BarraInferior.Visibility = BarraInferior ? Visibility.Visible : Visibility.Collapsed;
            this.BarraSuperior.Visibility = BarraSuperior ? Visibility.Visible : Visibility.Collapsed;
        }

        internal void RecargarAlertas()
        {
            this.BarraSuperior.RecargarAlertas();
        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                return;
            }
            this.WindowState = WindowState.Maximized;
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private MessageBoxResult _result = MessageBoxResult.None;
        private Button _close;
        private FrameworkElement _title;

        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            this._close =this.PART_Close;
            if (null != this._close)
            {
                //if (false == this._cancel.IsVisible)
                //{
                //    this._close.IsCancel = false;
                //}
            }

            this._title =this.PART_Title;
            if (null != this._title)
            {
                this._title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown2);
            }
        }

        private void title_MouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowDrag(sender,e);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //Attach this to the MouseDown event of your drag control to move the window in place of the title bar
        private void WindowDrag(object sender, MouseButtonEventArgs e) // MouseDown
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                0xA1, (IntPtr)0x2, (IntPtr)0);
        }

        //Attach this to the PreviewMousLeftButtonDown event of the grip control in the lower right corner of the form to resize the window
        private void WindowResize(object sender, MouseButtonEventArgs e) //PreviewMousLeftButtonDown
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61448, IntPtr.Zero);
        }
    }
}
