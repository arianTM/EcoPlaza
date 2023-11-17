using Datos;
using Datos.repositories;
using System.Collections.Generic;

namespace Negocio.services
{
    public class NIncidencia
    {
        private DIncidencia _dIncidencia = new DIncidencia();
        public void Registrar(Incidencia incidencia)
        {
            _dIncidencia.Registrar(incidencia);
        }

        public Incidencia GetIncidencia(int id)
        {
            return _dIncidencia.GetIncidencia(id);
        }

        public void Modificar(Incidencia incidencia)
        {
            _dIncidencia.Modificar(incidencia);
        }

        public void Eliminar(int id)
        {
            _dIncidencia.Eliminar(id);
        }

        public List<Incidencia> GetIncidencias()
        {
            return _dIncidencia.GetIncidencias();
        }
    }
}
