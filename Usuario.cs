using SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit;
using System.Collections.ObjectModel;
using System.Data;

namespace Inventario
{
    public class Usuario : ViewModelBase<Usuario>
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }


        private bool _PEntrada;
        public bool PEntrada
        {
            get => _PEntrada;
            set
            {
                _PEntrada = value;
                OnPropertyChanged();
                if (_PEntrada && _SoloLectura)
                {
                    SoloLectura = false;
                }
            }
        }
        private bool _PSalida;
        public bool PSalida
        {
            get => _PSalida;
            set
            {
                _PSalida = value;
                OnPropertyChanged();
                if (_PSalida && _SoloLectura)
                {
                    SoloLectura = false;
                }
            }
        }
        public bool PReportes { get; set; }

        private bool _SoloLectura;
        public bool SoloLectura
        {
            get => _SoloLectura;
            set
            {
                _SoloLectura = value;
                OnPropertyChanged();
                if (_SoloLectura)
                {
                    PEntrada = false;
                    PSalida = false;
                }
            }
        }

        public Usuario()
        {
        }
        public Usuario(int Id, string NickName, string Nombre, string Password, bool PEntrada, bool PSalida, bool PReportes, bool SoloLectura)
        {
            this.Id = Id;
            this.NickName = NickName;
            this.Nombre = Nombre;
            this.Password = Password;
            this.PEntrada = PEntrada;
            this.PReportes = PReportes;
            this.PSalida = PSalida;
            this.SoloLectura = SoloLectura;
        }
        public static Usuario Obtener(string NickName)
        {
            Usuario usuario = null;
            //leer informacion
            using (IReader leector = Conexion.Sqlite.Leector("SELECT * FROM USUARIOS WHERE NICKNAME ='" + NickName + "' AND OCULTO=0;"))
            {
                if (leector.Read())
                {
                    //tomar la informacion
                    int Id = Convert.ToInt32(leector["ID"].ToString());
                    string nickname = leector["NICKNAME"].ToString();
                    string nombre = leector["NOMBRE"].ToString();
                    string Password = Convert.ToString(leector["PASSWORD"]);

                    bool PEntrada = Convert.ToInt32(leector["PENTRADA"]) == 1;
                    bool PReportes = Convert.ToInt32(leector["PREPORTES"]) == 1;
                    bool PSalida = Convert.ToInt32(leector["PSALIDA"]) == 1;
                    bool SoloLectura = Convert.ToInt32(leector["ROLSL"]) == 1;

                    usuario = new Usuario(Id, nickname, nombre, Password, PEntrada, PSalida, PReportes, SoloLectura);

                }
            }
            return usuario;
        }

        internal bool Existe()
        {
            return Conexion.Sqlite.Exists("SELECT NICKNAME FROM USUARIOS WHERE NICKNAME='" + NickName + "'");
        }

        public static ObservableCollection<Usuario> Listar()
        {
            ObservableCollection<Usuario> usuariso = new ObservableCollection<Usuario>();
            Usuario usuario = null;
            //leer informacion
            using (IReader leector = Conexion.Sqlite.Leector("SELECT * FROM USUARIOS WHERE  OCULTO=0"))
            {
                while (leector.Read())
                {
                    //tomar la informacion
                    int Id = Convert.ToInt32(leector["ID"].ToString());
                    string nickname = leector["NICKNAME"].ToString();
                    string nombre = leector["NOMBRE"].ToString();
                    string Password = Convert.ToString(leector["PASSWORD"]);

                    bool PEntrada = Convert.ToInt32(leector["PENTRADA"]) == 1;
                    bool PReportes = Convert.ToInt32(leector["PREPORTES"]) == 1;
                    bool PSalida = Convert.ToInt32(leector["PSALIDA"]) == 1;
                    bool SoloLectura = Convert.ToInt32(leector["ROLSL"]) == 1;

                    usuario = new Usuario(Id, nickname, nombre, Password, PEntrada, PSalida, PReportes, SoloLectura);
                    usuariso.Add(usuario);

                }
                //leer informacion
            }
            return usuariso;
        }

        public void Alta()
        {
            int entrada = 0;
            int salida = 0;
            int reportes = 0;
            int slectura = 0;
            if (PEntrada)
            {
                entrada = 1;
            }
            if (PSalida)
            {
                salida = 1;
            }
            if (PReportes)
            {
                reportes = 1;
            }
            if (SoloLectura)
            {
                slectura = 1;
            }
            Conexion.Sqlite.EXEC("INSERT INTO USUARIOS (NICKNAME,NOMBRE,PASSWORD,PENTRADA,PSALIDA,PREPORTES,ROLSL) VALUES(?,?,?,?,?,?,?);"
                , NickName, Nombre, Password, entrada, salida, reportes, slectura);
        }
        public void Baja()
        {
            Conexion.Sqlite.EXEC("UPDATE USUARIOS SET OCULTO = 1 WHERE nickname = ?;", NickName);
        }
        public void Modificacion()
        {
            int entrada = 0;
            int salida = 0;
            int reportes = 0;
            int slectura = 0;
            if (PEntrada)
            {
                entrada = 1;
            }
            if (PSalida)
            {
                salida = 1;
            }
            if (PReportes)
            {
                reportes = 1;
            }
            if (SoloLectura)
            {
                slectura = 1;
            }

            Conexion.Sqlite.EXEC(
                "UPDATE USUARIOS SET NOMBRE=?,PASSWORD=?,PENTRADA=?,PREPORTES=?,PSALIDA=?,ROLSL=? WHERE  NICKNAME=?",
                Nombre, Password, entrada, reportes, salida, slectura, NickName);
        }
    }
}
