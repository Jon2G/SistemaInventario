using Inventario.ViewModels.EntradasSalidas;
using Kit;
using Kit.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Model;
using Kit.Sql.Readers;
using Kit.WPF.Services.ICustomMessageBox;

namespace Inventario.ViewModels.InventarioFisico
{
    public class Invis : ModelBase    {
        public string _Categoria;
        public string Categoria
        {
            get => _Categoria;
            set
            {
                if (_Categoria != value)
                {
                    CambioCategoria(value);
                }
            }
        }
        public List<string> Categorias { get; set; }
        public ObservableCollection<ProductoInvis> Productos { get; set; }
        public Invis()
        {
            this.Categorias = Producto.ListarCategorias();
            this.Productos = new ObservableCollection<ProductoInvis>();
        }

        private async void CambioCategoria(string nueva)
        {
            if (this.Productos.Any(x => x.Editado))
            {
                if (CustomMessageBox.ShowYesNo("¿Descartar el inventario de la categoría actual?", "Alerta", "Sí,descartar", "No") != Kit.Enums.CustomMessageBoxResult.Yes)
                {
                    return;
                }
            }
            _Categoria = nueva;
            OnPropertyChanged(nameof(Categoria));
            if (!string.IsNullOrEmpty(_Categoria))
            {
                CargarCategoria();
            }
        }
        private void CargarCategoria()
        {
            this.Productos.Clear();
            using (IReader leector = Conexion.Sqlite.Read($"SELECT * FROM PRODUCTOS WHERE OCULTO=0 AND CLASIFICACION='{Categoria}'"))
            {
                while (leector.Read())
                {
                    int Id = Convert.ToInt32(leector["ID"]);
                    string codigo = Convert.ToString(leector["CODIGO"]);
                    string nombre = Convert.ToString(leector["NOMBRE"]);
                    string unidad = Convert.ToString(leector["UNIDAD"]);
                    float existencia = Convert.ToSingle(leector["EXISTENCIA"]);

                    this.Productos.Add(new ProductoInvis(Id, codigo, nombre, unidad, existencia));
                }
            }
        }
        public async void ConfirmarConteo()
        {
            if (!this.Productos.Where(x => x.Editado).Any())
            {
                CustomMessageBox.Show("No puede finalizar este inventario físico por no tener partidas", "Atención",
                         CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                return;
            }
            if (CustomMessageBox.ShowYesNo("¿Confirmar el conteo de la categoría actual?", "Alerta",
                "Sí,confirmar", "No", CustomMessageBoxImage.Warning) != CustomMessageBoxResult.Yes)
            {
                return;
            }
            foreach (ProductoInvis producto in this.Productos.Where(x => x.Editado))
            {
                float diferencia = (float)producto.ExistenciaReal - producto.ExistenciaTeorica;
                string concepto = "A+";
                Tipo tipo = Tipo.Entrada;
                if (diferencia < 0)
                {
                    concepto = "MERMA";
                    tipo = Tipo.Salida;
                }

                Movimiento movimiento = new Movimiento(
                    producto.CodigoProducto,
                    App.Usuario.Id, tipo,
                    diferencia,
                    producto.ExistenciaTeorica,
                    (float)producto.ExistenciaReal, concepto, DateTime.Now, producto.Nombre);
                movimiento.RegistrarMovimiento();
            }
            this.Productos.Clear();
            Categoria = null;
        }

    }
}
