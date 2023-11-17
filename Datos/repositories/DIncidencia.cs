using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.repositories
{
    public class DIncidencia
    {
        public void Registrar(Incidencia incidencia)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Incidencias.Add(incidencia);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public Incidencia GetIncidencia(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Incidencias.Find(id) ?? throw new OwnException("¡Incidencia no encontrada!");
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

        public void Modificar(Incidencia incidencia)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Incidencia incidenciaModificada = context.Incidencias.Find(incidencia.id) ?? throw new OwnException("¡Incidencia no encontrada!");
                    incidenciaModificada.descripcion = incidencia.descripcion;
                    incidenciaModificada.categoria = incidencia.categoria;
                    incidenciaModificada.fecha = incidencia.fecha;
                    incidenciaModificada.hora = incidencia.hora;
                    incidenciaModificada.updated_at = incidencia.updated_at;
                    incidenciaModificada.updated_by = incidencia.updated_by;
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

        public void Eliminar(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Incidencia incidencia = context.Incidencias.Find(id) ?? throw new OwnException("¡Incidencia no encontrada!");
                    context.Incidencias.Remove(incidencia);
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

        public List<Incidencia> GetIncidencias()
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Incidencias.ToList();
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
