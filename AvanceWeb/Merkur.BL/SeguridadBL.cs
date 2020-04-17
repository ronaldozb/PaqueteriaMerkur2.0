using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;
using System.Security.Cryptography;

namespace Merkur.BL
{
    public class SeguridadBL
    {
        Contexto _contexto;
        public BindingList<Usuarios> ListaUsuarios { get; set; }

        public SeguridadBL()
        {
            _contexto = new Contexto();
            ListaUsuarios = new BindingList<Usuarios>();
        }

        public bool Autorizar(string nombreUsuario, string contrasena)
        {

            var contrasenaEncriptada = Encriptar.CodificarContrasena(contrasena);
            var usuario = _contexto.Usuarios
                .FirstOrDefault(r => r.Nombre == nombreUsuario &&
                r.Contrasena == contrasenaEncriptada);

            if (usuario != null)
            {
                return true;
            }

            return false;
        }
    
        





        public Usuarios Autenticar(string usuario, string contrasena)
        {
            var usuarios = _contexto.Usuarios.ToList();

            foreach (var UsuarioDB in usuarios)
            {
                if (usuario == UsuarioDB.Nombre && contrasena == UsuarioDB.Contrasena)
                {
                    return UsuarioDB;
                }
            }
            return null;

        }
        public BindingList<Usuarios> ObtenerUsuarios()
        {
            _contexto.Usuarios.Load();
            ListaUsuarios = _contexto.Usuarios.Local.ToBindingList();

            return ListaUsuarios;
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        public Resultado GuardarUsuario(Usuarios usuario)
        {
            var resultado = Validar(usuario);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;
        }

        public void AgregarUsuario()
        {
            var nuevoUsuario = new Usuarios();
            _contexto.Usuarios.Add(nuevoUsuario);
        }

        public bool EliminarUsuario(int id)
        {
            foreach (var usuario in ListaUsuarios.ToList())
            {
                if (usuario.Id == id)
                {
                    ListaUsuarios.Remove(usuario);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        private Resultado Validar(Usuarios usuario)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (usuario == null)
            {
                resultado.Mensaje = "Agregue un usuario valido";
                resultado.Exitoso = false;

                return resultado;
            }

            return resultado;
        }

        
        
    }

    public static class Encriptar
    {
        public static string CodificarContrasena(string contrasena)
        {
            var salt = "UNAH";

            byte[] bIn = Encoding.Unicode.GetBytes(contrasena);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            HashAlgorithm s = HashAlgorithm.Create("SHA512");
            byte[] bRet = s.ComputeHash(bAll);

            return Convert.ToBase64String(bRet);
        }

    }
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string TipodeUsiario { get; set; }

        public string Privilegio { get; set; }
    }
}
