using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.repositories
{
    public class DAsistencia
    {
        public void Registrar(Asistencia asistencia)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Asistencias.Add(asistencia);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public Asistencia GetAsistencia(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Asistencias.Find(id) ?? throw new OwnException("¡Asistencia no encontrada!");
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

        public void Modificar(Asistencia asistencia)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Asistencia asistenciaModificada = context.Asistencias.Find(asistencia.id) ?? throw new OwnException("¡Asistencia no encontrada!");
                    asistenciaModificada.trabajador = asistencia.trabajador;
                    asistenciaModificada.fecha = asistencia.fecha;
                    asistenciaModificada.hora = asistencia.hora;
                    asistenciaModificada.updated_at = asistencia.updated_at;
                    asistenciaModificada.updated_by = asistencia.updated_by;
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
                    Asistencia asistencia = context.Asistencias.Find(id) ?? throw new OwnException("¡Asistencia no encontrada!");
                    context.Asistencias.Remove(asistencia);
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

        public List<Asistencia> GetAsistenciasPorSubcontrata(int idSubcontrata)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    List<Asistencia> asistencias = context.Asistencias.ToList().FindAll(el => el.subcontrata_id == idSubcontrata);
                    asistencias.Reverse();
                    return asistencias;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public void EliminarAsistenciasDeSubcontrata(int idSubcontrata)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    List<Asistencia> asistencias = context.Asistencias.ToList().FindAll(el => el.subcontrata_id == idSubcontrata);
                    asistencias.ForEach(material =>
                    {
                        Eliminar(material.id);
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public List<Asistencia> GetAsistencias()
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Asistencias.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public List<Asistencia> GetAsistenciasPorSubcontrataYTrabajador(int idSubcontrata, String trabajador)
        {
            List<Asistencia> asistencias = GetAsistenciasPorSubcontrata(idSubcontrata);
            return asistencias.FindAll(asistencia => asistencia.trabajador.IndexOf(trabajador, StringComparison.OrdinalIgnoreCase) != -1);
        }
    }
}
