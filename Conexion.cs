using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kit.Sql.Helpers;
using Kit.Sql.Sqlite;

namespace Inventario
{
    public static class Conexion
    {
        public static SQLiteConnection Sqlite;
        public static void Inicializar(string RutaBaseDeDatos)
        {
            Sqlite = new SQLiteConnection(new FileInfo(RutaBaseDeDatos), 107);
        }

    }
}
