using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.repositories
{
    public class DSubcontrata
    {
        public void Registrar(Subcontrata subcontrata)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Subcontratas.Add(subcontrata);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public Subcontrata GetSubcontrata(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Subcontratas.Find(id) ?? throw new Exception("¡Subcontrata no encontrada!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public void Modificar(Subcontrata subcontrata)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Subcontrata subcontrataModificada = context.Subcontratas.Find(subcontrata.id) ?? throw new Exception("¡Subcontrata no encontrada!");
                    subcontrataModificada.nombre = subcontrata.nombre;
                    subcontrataModificada.descripcion = subcontrata.descripcion;
                    subcontrataModificada.ruc = subcontrata.ruc;
                    subcontrataModificada.celular = subcontrata.celular;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Subcontrata subcontrata = GetSubcontrata(id);
                    context.Subcontratas.Remove(subcontrata);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public List<Subcontrata> GetSubcontratas()
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Subcontratas.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }
    }
}
