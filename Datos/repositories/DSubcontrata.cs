using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.repositories
{
    public class DSubcontrata
    {
        private DMaterial _dMaterial = new DMaterial();
        private DAsistencia _dAsistencia = new DAsistencia();
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
                    return context.Subcontratas.Find(id) ?? throw new OwnException("¡Subcontrata no encontrada!");
                }
            }
            catch (OwnException)
            {
                throw;
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
                    Subcontrata subcontrataModificada = context.Subcontratas.Find(subcontrata.id) ?? throw new OwnException("¡Subcontrata no encontrada!");
                    subcontrataModificada.nombre = subcontrata.nombre;
                    subcontrataModificada.descripcion = subcontrata.descripcion;
                    subcontrataModificada.ruc = subcontrata.ruc;
                    subcontrataModificada.celular = subcontrata.celular;
                    subcontrataModificada.updated_at = subcontrata.updated_at;
                    subcontrataModificada.updated_by = subcontrata.updated_by;
                    context.SaveChanges();
                }
            }
            catch (OwnException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        /// <summary>
        /// Antes de eliminar la subcontrata, se deben eliminar sus asistencias y sus materiales
        /// </summary>
        public void Eliminar(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Subcontrata subcontrata = context.Subcontratas.Find(id) ?? throw new OwnException("¡Subcontrata no encontrada!");
                    _dAsistencia.EliminarAsistenciasDeSubcontrata(subcontrata.id);
                    _dMaterial.EliminarMaterialesDeSubcontrata(subcontrata.id);
                    context.Subcontratas.Remove(subcontrata);
                    context.SaveChanges();
                }
            }
            catch (OwnException)
            {
                throw;
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
