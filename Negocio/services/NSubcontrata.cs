using Datos;
using Datos.repositories;
using System.Collections.Generic;

namespace Negocio.services
{
    public class NSubcontrata
    {
        private DSubcontrata _dSubcontrata = new DSubcontrata();
        public void Registrar(Subcontrata subcontrata)
        {
            _dSubcontrata.Registrar(subcontrata);
        }

        public Subcontrata GetSubcontrata(int id)
        {
            return _dSubcontrata.GetSubcontrata(id);
        }

        public void Modificar(Subcontrata subcontrata)
        {
            _dSubcontrata.Modificar(subcontrata);
        }

        public void Eliminar(int id)
        {
            _dSubcontrata.Eliminar(id);
        }

        public List<Subcontrata> GetSubcontratas()
        {
            return _dSubcontrata.GetSubcontratas();
        }
    }
}
