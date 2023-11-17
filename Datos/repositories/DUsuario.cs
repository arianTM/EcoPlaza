using System;
using System.Linq;

namespace Datos.repositories
{
    public class DUsuario
    {
        /// <summary>
        /// Carga inicial para agilizar el resto de queries
        /// </summary>
        public void CargaInicial()
        {
            using (var context = new BDEFEntities())
            {
                context.Usuarios.FirstOrDefault();
            }
        }

        /// <summary>
        /// Determinar si el username ya está tomado
        /// *Nota: cuando un usuario se elimine, se conservará su username
        /// ~ Por tanto, no se podrá registrar un usuario con el mismo username
        /// </summary>
        private bool Existe(String username)
        {
            using (var context = new BDEFEntities())
            {
                return context.Usuarios.Any(el => String.Equals(el.username, username));
            }
        }

        /// <summary>
        /// Registrar usuario en la base de datos
        /// *Nota: el id es autogenerado, no se debe asignar
        /// *Nota: incluir el campo activo como 'true' SIEMPRE!!
        /// </summary>
        public void Registrar(Usuario usuario)
        {
            if (Existe(usuario.username)) throw new Exception("¡Nombre de usuario no disponible!");
            using (var context = new BDEFEntities())
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Obtener ID luego de ingresar las credenciales
        /// (Login / Signin)
        /// *Nota: no podrás loguearte con un usuario eliminado
        /// </summary>
        public int GetIdUsuario(String username, String contra)
        {
            using (var context = new BDEFEntities())
            {
                Usuario usuario = context.Usuarios.FirstOrDefault(el => String.Equals(el.username, username) && String.Equals(el.contra, contra)) ?? throw new Exception("¡Usuario o contraseña incorrectas! Vuelva a intentarlo.");
                if (!usuario.activo) throw new Exception("¡Usuario eliminado!");
                return usuario.id;
            }
        }

        /// <summary>
        /// Obtener solamente el nombre de usuario y los nombres completos
        /// </summary>
        public Usuario GetUsuario(int id)
        {
            using (var context = new BDEFEntities())
            {
                Usuario usuario = context.Usuarios.Find(id) ?? throw new Exception("¡Usuario no encontrado!");
                return new Usuario()
                {
                    username = usuario.username,
                    nombre = usuario.nombre,
                };
            }
        }

        /// <summary>
        /// Hacer cambios en un usuario
        /// (Validar que el nuevo nombre de usuario no esté tomado)
        /// </summary>
        public void Modificar(Usuario usuario)
        {
            using (var context = new BDEFEntities())
            {
                Usuario usuarioModificado = context.Usuarios.Find(usuario.id) ?? throw new Exception("¡Usuario no encontrado!");
                if (!String.Equals(usuarioModificado.username, usuario.username) && Existe(usuario.username)) throw new Exception("¡Nombre de usuario no disponible!");
                usuarioModificado.username = usuario.username;
                usuarioModificado.nombre = usuario.nombre;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Cuando un usuario se elimine:
        /// - Su propiedad 'activo' pasará de 'true' a 'false'
        /// </summary>
        public void Eliminar(int id)
        {
            using (var context = new BDEFEntities())
            {
                Usuario usuarioEliminado = context.Usuarios.Find(id) ?? throw new Exception("¡Usuario no encontrado!");
                usuarioEliminado.activo = false;
                context.SaveChanges();
            }
        }
    }
}
