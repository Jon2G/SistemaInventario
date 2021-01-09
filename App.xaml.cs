using Inventario.Views;
using System;
using System.Diagnostics;
using System.Windows;

namespace Inventario
{
    public partial class App 
    {
        public static new MainWindow MainWindow { get; set; }
        public static Usuario Usuario { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Kit.WPF.Tools.Init();
            SQLHelper.SQLHelper.Init(Environment.CurrentDirectory, Debugger.IsAttached);
            Conexion.Inicializar("Inventario.db");
            Conexion.Sqlite.SetDbScriptResource<App>("Script.sql");
            Conexion.Sqlite.RevisarBaseDatos();

            base.OnStartup(e);
        }
    }
}
