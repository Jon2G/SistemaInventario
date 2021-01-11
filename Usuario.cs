using SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Media;
using static Kit.WPF.Extensions.Extensiones;
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
                    EDUSUARIO = false;
                }
            }
        }
        private bool _EDUSUARIO;
        public bool EDUSUARIO
        {
            get => _EDUSUARIO;
            set
            {
                _EDUSUARIO = value;
                OnPropertyChanged();

            }
        }
        private ImageSource _Imagen;
        public ImageSource Imagen { get => _Imagen; set { _Imagen = value; OnPropertyChanged(); } }

        public Usuario()
        {
        }
        public Usuario(int Id, string NickName, string Nombre, string Password, bool PEntrada, bool PSalida, bool PReportes, bool SoloLectura, ImageSource Imagen, bool EDUSUARIO)
        {
            this.Id = Id;
            this.NickName = NickName;
            this.Nombre = Nombre;
            this.Password = Password;
            this.PEntrada = PEntrada;
            this.PReportes = PReportes;
            this.PSalida = PSalida;
            this.SoloLectura = SoloLectura;
            this.Imagen = Imagen;
            this.EDUSUARIO = EDUSUARIO;
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

                    ImageSource imagen = ((byte[])leector["IMAGEN"]).ByteToImage();
                    bool PEntrada = Convert.ToInt32(leector["PENTRADA"]) == 1;
                    bool PReportes = Convert.ToInt32(leector["PREPORTES"]) == 1;
                    bool PSalida = Convert.ToInt32(leector["PSALIDA"]) == 1;
                    bool SoloLectura = Convert.ToInt32(leector["ROLSL"]) == 1;
                    bool EDusuario = Convert.ToInt32(leector["EDUSUARIOS"]) == 1;
                    usuario = new Usuario(Id, nickname, nombre, Password, PEntrada, PSalida, PReportes, SoloLectura, imagen, EDusuario);

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
                    string Password = Kit.Extensions.Security.Decrypta(Convert.ToString(leector["PASSWORD"]));

                    ImageSource imagen = null;
                    if (leector["IMAGEN"] is byte[] bytes)
                    {
                        imagen = bytes.ByteToImage();
                    }
                    bool PEntrada = Convert.ToInt32(leector["PENTRADA"]) == 1;
                    bool PReportes = Convert.ToInt32(leector["PREPORTES"]) == 1;
                    bool PSalida = Convert.ToInt32(leector["PSALIDA"]) == 1;
                    bool SoloLectura = Convert.ToInt32(leector["ROLSL"]) == 1;
                    bool EDusuario = Convert.ToInt32(leector["EDUSUARIOS"]) == 1;
                    usuario = new Usuario(Id, nickname, nombre, Password, PEntrada, PSalida, PReportes, SoloLectura, imagen, EDusuario);
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
            int edusuario = 0;
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
            if (EDUSUARIO)
            {
                edusuario = 1;
            }
            Conexion.Sqlite.EXEC("INSERT INTO USUARIOS (NICKNAME,NOMBRE,PASSWORD,PENTRADA,PSALIDA,PREPORTES,ROLSL,IMAGEN,EDUSUARIOS) VALUES(?,?,?,?,?,?,?,?,?);"
                , NickName, Nombre, Kit.Extensions.Security.Decrypta(Password), entrada, salida, reportes, slectura, Imagen.ImageToBytes(), edusuario);
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
            int edusuario = 0;
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
            if (EDUSUARIO)
            {
                edusuario = 1;
            }
            Conexion.Sqlite.EXEC(
                "UPDATE USUARIOS SET NOMBRE=?,PASSWORD=?,PENTRADA=?,PREPORTES=?,PSALIDA=?,ROLSL=?,IMAGEN=?,EDUSUARIOS=? WHERE  NICKNAME=?",
                Nombre, Kit.Extensions.Security.Decrypta(Password), entrada, reportes, salida, slectura, Imagen.ImageToBytes(), edusuario, NickName);
        
        }
    }
}
