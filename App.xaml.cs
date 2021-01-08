using Inventario.Views;
using System;
using System.Diagnostics;
using System.Windows;

namespace Inventario
{
    public partial class App 
    {
        public static new MainWindow MainWindow { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            SQLHelper.SQLHelper.Init(Environment.CurrentDirectory, Debugger.IsAttached);
            Conexion.Inicializar("Inventario.db");
            base.OnStartup(e);
        }
    }
}
