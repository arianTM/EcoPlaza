using Datos;
using Datos.repositories;
using System.Collections.Generic;

namespace Negocio.services
{
    public class NAsistencia
    {
        private DAsistencia _dAsistencia = new DAsistencia();
        public void Registrar(Asistencia asistencia)
        {
            _dAsistencia.Registrar(asistencia);
        }

        public Asistencia GetAsistencia(int id)
        {
            return _dAsistencia.GetAsistencia(id);
        }

        public void Modificar(Asistencia asistencia)
        {
            _dAsistencia.Modificar(asistencia);
        }

        public void Eliminar(int id)
        {
            _dAsistencia.Eliminar(id);
        }

        public List<Asistencia> GetAsistenciasPorSubcontrata(int idSubcontrata)
        {
            return _dAsistencia.GetAsistenciasPorSubcontrata(idSubcontrata);
        }
    }
}
