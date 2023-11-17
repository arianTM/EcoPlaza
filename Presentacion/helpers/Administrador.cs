namespace Presentacion.helpers
{
    /// <summary>
    /// La clase Administrador se encarga de guardar la clave (id) del usuario
    /// Para hacer operaciones en UsuarioPage
    /// </summary>
    internal static class Administrador
    {
        // El id del usuario comienza en 1, y se incrementa en 1 (1, 2, 3,...)
        private static int _idUsuario = 0;

        public static int GetIdUsuario()
        {
            return _idUsuario;
        }

        public static void SetIdUsuario(int idUsuario)
        {
            _idUsuario = idUsuario;
        }

        public static void ClearIdUsuario()
        {
            _idUsuario = 0;
        }
    }
}