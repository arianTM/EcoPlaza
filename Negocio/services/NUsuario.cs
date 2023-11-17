using Datos;
using Datos.repositories;
using System;

namespace Negocio.services
{
    public class NUsuario
    {
        private DUsuario _dUsuario = new DUsuario();

        public void Registrar(Usuario usuario)
        {
            _dUsuario.Registrar(usuario);
        }

        public int GetIdUsuario(String username, String contra)
        {
            return _dUsuario.GetIdUsuario(username, contra);
        }

        public Usuario GetUsuario(int id)
        {
            return _dUsuario.GetUsuario(id);
        }

        public void Modificar(Usuario usuario)
        {
            _dUsuario.Modificar(usuario);
        }

        public void Eliminar(int id)
        {
            _dUsuario.Eliminar(id);
        }
    }
}
