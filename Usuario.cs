
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
using Kit.Security.Encryption;
using System.Text.RegularExpressions;
using Kit.Enums;
using Kit.Model;
using Kit.Sql.Helpers;
using Kit.Sql.Readers;
using Kit.WPF.Extensions;

namespace Inventario
{
    public class Usuario : ModelBase
    {
        public int Id { get; set; }
        private string _NickName;
        public string NickName { get => _NickName; set { _NickName = value; OnPropertyChanged(); } }
        private string _Nombre;
        public string Nombre
        {
            get => _Nombre;
            set
            {
                _Nombre = value;
                OnPropertyChanged();

                Iniciales =Kit.Extensions.Helpers.ExtractInitialsFromName(_Nombre);
                OnPropertyChanged(nameof(Iniciales));
            }
        }

        public string Iniciales { get; set; }

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
                    PUsuarios = false;
                }
            }
        }
        private bool _EDUSUARIO;
        public bool PUsuarios
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
            this.PUsuarios = EDUSUARIO;
        }
        public static Usuario ObtenerPorNombreNick(string NickName)
        {
            Usuario usu = Obtener(NickName);
            if (usu is null)
            {
                if (SQLHelper.IsInjection(NickName))
                {
                    return null;
                }
                string nick = Conexion.Sqlite.Single<string>($"SELECT NICKNAME FROM USUARIOS WHERE NOMBRE='{NickName}'");
                if (!string.IsNullOrEmpty(nick))
                {
                    return Obtener(nick);
                }
            }
            return usu;
        }
        public static Usuario Obtener(string NickName)
        {
            Usuario usuario = null;
            if (SQLHelper.IsInjection(NickName))
            {
                return null;
            }
            Kit.Security.Encryption.Encryption Cesar = new Cesar();
            //leer informacion
            using (IReader leector = Conexion.Sqlite.Read($"SELECT *FROM USUARIOS WHERE NICKNAME='{NickName}' AND OCULTO=0"))
            {
                if (leector.Read())
                {
                    //tomar la informacion
                    int Id = Convert.ToInt32(leector["ID"].ToString());
                    string nickname = leector["NICKNAME"].ToString();
                    string nombre = leector["NOMBRE"].ToString();
                    string Password = Cesar.ToString(Cesar.Decrypt((byte[])leector["PASSWORD"]));
                    ImageSource imagen = ((byte[])leector["IMAGEN"]).BytesToBitmap();
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
        public bool Existe()
        {
            return Conexion.Sqlite.Exists("SELECT NICKNAME FROM USUARIOS WHERE NICKNAME='" + NickName + "'");
        }
        public static ObservableCollection<Usuario> Listar()
        {
            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>();
            foreach (string usuario in Conexion.Sqlite.Lista<string>("SELECT NICKNAME FROM USUARIOS WHERE  OCULTO=0"))
            {
                usuarios.Add(Obtener(usuario));
            }
            return usuarios;
        }
        public void Alta()
        {
            int entrada = PEntrada ? 1 : 0;
            int salida = PSalida ? 1 : 0;
            int reportes = PReportes ? 1 : 0;
            int slectura = SoloLectura ? 1 : 0;
            int edusuario = PUsuarios ? 1 : 0;
            Kit.Security.Encryption.Encryption Cesar = new Cesar();

            Conexion.Sqlite.EXEC("INSERT INTO USUARIOS (NICKNAME,NOMBRE,PASSWORD,PENTRADA,PSALIDA,PREPORTES,ROLSL,IMAGEN,EDUSUARIOS) VALUES(?,?,?,?,?,?,?,?,?);"
                , NickName, Nombre, Cesar.Encrypt(Password), entrada, salida, reportes, slectura, Imagen.ImageToBytes(), edusuario);
        }
        public void Baja()
        {
            Conexion.Sqlite.EXEC("UPDATE USUARIOS SET OCULTO = 1 WHERE nickname = ?;", NickName);
        }
        public void Modificacion()
        {
            Kit.Security.Encryption.Encryption Cesar = new Cesar();
            int entrada = PEntrada ? 1 : 0;
            int salida = PSalida ? 1 : 0;
            int reportes = PReportes ? 1 : 0;
            int slectura = SoloLectura ? 1 : 0;
            int edusuario = PUsuarios ? 1 : 0;

            Conexion.Sqlite.EXEC(
                "UPDATE USUARIOS SET NOMBRE=?,PASSWORD=?,PENTRADA=?,PREPORTES=?,PSALIDA=?,ROLSL=?,IMAGEN=?,EDUSUARIOS=? WHERE  NICKNAME=?",
                Nombre, Cesar.Encrypt(Password), entrada, reportes, salida, slectura, Imagen.ImageToBytes(), edusuario, NickName);
        }
    }
}
