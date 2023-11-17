using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.repositories
{
    public class DSubcontrata
    {
        public void Registrar(Subcontrata subcontrata)
        {
            using (var context = new BDEFEntities())
            {
                context.Subcontratas.Add(subcontrata);
                context.SaveChanges();
            }
        }

        public Subcontrata GetSubcontrata(int id)
        {
            using (var context = new BDEFEntities())
            {
                return context.Subcontratas.Find(id) ?? throw new Exception("¡Subcontrata no encontrada!");
            }
        }

        public void Modificar(Subcontrata subcontrata)
        {
            using (var context = new BDEFEntities())
            {
                Subcontrata subcontrataModificada = GetSubcontrata(subcontrata.id);
                subcontrataModificada.nombre = subcontrata.nombre;
                subcontrataModificada.descripcion = subcontrata.descripcion;
                subcontrataModificada.ruc = subcontrata.ruc;
                subcontrataModificada.celular = subcontrata.celular;
                context.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var context = new BDEFEntities())
            {
                Subcontrata subcontrata = GetSubcontrata(id);
                context.Subcontratas.Remove(subcontrata);
                context.SaveChanges();
            }
        }

        public List<Subcontrata> GetSubcontratas()
        {
            using (var context = new BDEFEntities())
            {
                return context.Subcontratas.ToList();
            }
        }
    }
}
