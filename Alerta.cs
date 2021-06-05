using Kit.Sql.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Kit.WPF.Extensions;
using static Kit.WPF.Extensions.Extensiones;
namespace Inventario
{
    public class Alerta
    {
        public static string ObtenerAlerta()
        {
            string leyenda = "Ningún producto requiere atención";
            bool maximo = AlertaMaximo();
            bool minimo = AlertaMinimo();

            if (maximo && minimo)
            {
                leyenda = "Existen productos que requieren atención";
            }
            else if (maximo)
            {
                leyenda = "Existen productos con sobreexistencia";
            }
            else if (minimo)
            {
                leyenda = "Existen productos bajos en existencia";
            }
            return leyenda;
        }
        public static bool AlertaMaximo()
        {
            return Conexion.Sqlite.Exists("SELECT * FROM PRODUCTOS WHERE EXISTENCIA>=MAXIMO");
        }
        public static bool AlertaMinimo()
        {
            return Conexion.Sqlite.Exists("SELECT * FROM PRODUCTOS WHERE EXISTENCIA<=MINIMO");
        }
        public static List<Producto> ListarProductosConAlerta()
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = null;
            using (IReader leector = Conexion.Sqlite.Read("SELECT * FROM PRODUCTOS WHERE EXISTENCIA >= MAXIMO OR EXISTENCIA <= MINIMO"))
            {
                while (leector.Read())
                {
                    int Id = Convert.ToInt32(leector["ID"]);
                    string codigo = Convert.ToString(leector["CODIGO"]);
                    string nombre = Convert.ToString(leector["NOMBRE"]);
                    string descripcion = Convert.ToString(leector["DESCRIPCION"]);
                    string clasificacion = Convert.ToString(leector["CLASIFICACION"]);
                    string unidad = Convert.ToString(leector["UNIDAD"]);
                    ImageSource imagen = ((byte[])leector["IMAGEN"]).BytesToBitmap();
                    string proveedor = Convert.ToString(leector["PROVEDOR"]);
                    float existencia = Convert.ToSingle(leector["EXISTENCIA"]);
                    float minimo = Convert.ToSingle(leector["MINIMO"]);
                    float maximo = Convert.ToSingle(leector["MAXIMO"]);
                    float precio = Convert.ToSingle(leector["PRECIO"]);
                    producto = new Producto(Id, codigo, nombre, descripcion, clasificacion, unidad, imagen, proveedor, existencia, minimo, maximo, precio);
                    productos.Add(producto);
                }
            }
            return productos;
        }

    }
}
