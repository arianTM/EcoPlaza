namespace Presentacion.helpers
{
    /// <summary>
    /// La clase Administrador guarda, almacena y resetea:
    /// - La clave primaria (id) del usuario (operaciones)
    /// - La clave primaria (id) de la subcontrata (operaciones + navegación)
    /// </summary>
    internal static class Administrador
    {
        // El id comienza en 1, y se incrementa en 1 (1, 2, 3,...)
        private static int _idUsuario = 0;
        private static int _idSubcontrata = 0;
        // Cuando está en 0, es porque no está asignada a ningun valor.

        #region Id Usuario
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
        #endregion

        #region Id Subcontrata

        public static int GetIdSubcontrata()
        {
            return _idSubcontrata;
        }

        public static void SetIdSubcontrata(int idSubcontrata)
        {
            _idSubcontrata = idSubcontrata;
        }

        public static void ClearIdSubcontrata()
        {
            _idSubcontrata = 0;
        }

        #endregion
    }
}