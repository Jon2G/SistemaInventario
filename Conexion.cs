using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Inventario
{
    public static class Conexion
    {
        public static SQLHelper.SQLHLite Sqlite;
        public static void Inicializar(string RutaBaseDeDatos)
        {
            Sqlite = new SQLHelper.SQLHLite("0.0.7", RutaBaseDeDatos);
        }

    }
}
