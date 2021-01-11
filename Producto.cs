using Kit;
using SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Kit.WPF.Extensions.Extensiones;

namespace Inventario
{
    public class Producto : ViewModelBase<Producto>
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Clasificacion { get; set; }
        public string Unidad { get; set; }
        private ImageSource _Imagen;
        public ImageSource Imagen { get => _Imagen; set { _Imagen = value; OnPropertyChanged(); } }
        public string Proveedor { get; set; }
        public float Existencia { get; set; }
        public float Minimo { get; set; }
        public float Maximo { get; set; }
        public float Precio { get; set; }

        public Producto() { }
        public Producto(int Id, string Codigo, string Nombre, string Descripcion, string Clasificacion, string Unidad, ImageSource Imagen, string Proveedor, float Existencia, float Minimo, float Maximo, float Precio)
        {
            this.Id = Id;
            this.Codigo = Codigo;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Clasificacion = Clasificacion;
            this.Unidad = Unidad;
            this.Imagen = Imagen;
            this.Proveedor = Proveedor;
            this.Existencia = Existencia;
            this.Minimo = Minimo;
            this.Maximo = Maximo;
            this.Precio = Precio;
        }

        /// <summary>
        /// Se lee en la base de datos la información de un producto
        /// </summary>
        /// <param name="Codigo">El código del prodcuto que se desea obtener</param>
        /// <returns>Se regresa un producto con sus datos cargados</returns>
        public static Producto Obtener(string Codigo)
        {
            Producto producto = null;
            using (IReader leector = Conexion.Sqlite.Leector("SELECT * FROM PRODUCTOS WHERE CODIGO='" + Codigo + "'"))
            {
                if (leector.Read())
                {
                    int Id = Convert.ToInt32(leector["ID"]);
                    string codigo = Convert.ToString(leector["CODIGO"]);
                    string nombre = Convert.ToString(leector["NOMBRE"]);
                    string descripcion = Convert.ToString(leector["DESCRIPCION"]);
                    string clasificacion = Convert.ToString(leector["CLASIFICACION"]);
                    string unidad = Convert.ToString(leector["UNIDAD"]);
                    ImageSource imagen = ((byte[])leector["IMAGEN"]).ByteToImage();
                    string proveedor = Convert.ToString(leector["PROVEDOR"]);
                    float existencia = Convert.ToSingle(leector["EXISTENCIA"]);
                    float minimo = Convert.ToSingle(leector["MINIMO"]);
                    float maximo = Convert.ToSingle(leector["MAXIMO"]);
                    float precio = Convert.ToSingle(leector["PRECIO"]);
                    producto = new Producto(Id, codigo, nombre, descripcion, clasificacion, unidad, imagen, proveedor, existencia, minimo, maximo, precio);
                }
            }
            return producto;
        }
        public static List<Producto> Listar()
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = null;
            using (IReader leector = Conexion.Sqlite.Leector("SELECT *FROM PRODUCTOS WHERE OCULTO=0 ORDER BY NOMBRE"))
            {
                if (leector.Read())
                {
                    int Id = Convert.ToInt32(leector["ID"]);
                    string codigo = Convert.ToString(leector["CODIGO"]);
                    string nombre = Convert.ToString(leector["NOMBRE"]);
                    string descripcion = Convert.ToString(leector["DESCRIPCION"]);
                    string clasificacion = Convert.ToString(leector["CLASIFICACION"]);
                    string unidad = Convert.ToString(leector["UNIDAD"]);
                    ImageSource imagen = ((byte[])leector["IMAGEN"]).ByteToImage();
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

        public static List<Producto> Buscar(string Categoria, string Busqueda)
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = null;
            using (IReader leector = Conexion.Sqlite.Leector("SELECT *FROM PRODUCTOS WHERE (CLASIFICACION = '" + Categoria + "' OR '" + Categoria + "'='') AND OCULTO=0 AND NOMBRE LIKE '%" + Busqueda + "%'  ORDER BY NOMBRE"))
            {
                while (leector.Read())
                {
                    int Id = Convert.ToInt32(leector["ID"]);
                    string codigo = Convert.ToString(leector["CODIGO"]);
                    string nombre = Convert.ToString(leector["NOMBRE"]);
                    string descripcion = Convert.ToString(leector["DESCRIPCION"]);
                    string clasificacion = Convert.ToString(leector["CLASIFICACION"]);
                    string unidad = Convert.ToString(leector["UNIDAD"]);
                    ImageSource imagen = ((byte[])leector["IMAGEN"]).ByteToImage();
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

        public static float ObtenerExistencia(string CodigoProducto)
        {
            return Conexion.Sqlite.Single<float>($"SELECT EXISTENCIA FROM PRODUCTOS WHERE CODIGO='{CodigoProducto}'"); ;
        }
        public static int ObtenerId(string CodigoProducto)
        {
            return Conexion.Sqlite.Single<int>($"SELECT ID FROM PRODUCTOS WHERE CODIGO='{CodigoProducto}'"); ;
        }

        public bool Existe()
        {
            return Conexion.Sqlite.Exists("SELECT CODIGO FROM PRODUCTOS WHERE CODIGO='" + Codigo + "'");
        }
        /// <summary>
        /// Insertar en la base de datos un nuevo producto
        /// </summary>
        public void Alta()
        {
            if (Existe())
            {
                Modificacion();
                return;
            }
            Conexion.Sqlite.EXEC(
                "INSERT INTO PRODUCTOS (CODIGO,NOMBRE,DESCRIPCION,CLASIFICACION,UNIDAD,IMAGEN,PROVEDOR,EXISTENCIA,MINIMO,MAXIMO,PRECIO) VALUES(?,?,?,?,?,?,?,?,?,?,?);"
                , Codigo, Nombre, Descripcion, Clasificacion, Unidad, Imagen.ImageToBytes(), Proveedor, Existencia, Minimo, Maximo, Precio);

        }
        /// <summary>
        /// Dar de baja en la base de datos un producto
        /// </summary>
        public void Baja()
        {
            Conexion.Sqlite.EXEC("UPDATE PRODUCTOS SET OCULTO = 1 WHERE CODIGO = ?", Codigo);
        }
        /// <summary>
        ///  Actualizar un producto en la base
        /// </summary>
        public void Modificacion()
        {
            Conexion.Sqlite.EXEC(
                "UPDATE PRODUCTOS SET NOMBRE=?,DESCRIPCION=?,CLASIFICACION=?,UNIDAD=?,IMAGEN=?,PROVEDOR=?,EXISTENCIA=?,MINIMO=?,MAXIMO=?,PRECIO=? WHERE CODIGO=?"
                , Nombre, Descripcion, Clasificacion, Unidad, Imagen.ImageToBytes(), Proveedor, Existencia, Minimo, Maximo, Precio, Codigo);
        }
    }
}
