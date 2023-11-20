using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.repositories
{
    public class DMaterial
    {
        public void Registrar(Material material)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Materials.Add(material);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public Material GetMaterial(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Materials.Find(id) ?? throw new OwnException("¡Material no encontrado!");
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

        public void Modificar(Material material)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Material materialModificado = context.Materials.Find(material.id) ?? throw new OwnException("¡Material no encontrado!");
                    materialModificado.nombre = material.nombre;
                    materialModificado.marca = material.marca;
                    materialModificado.cantidad = material.cantidad;
                    materialModificado.costo = material.costo;
                    materialModificado.updated_at = material.updated_at;
                    materialModificado.updated_by = material.updated_by;
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
                    Material material = context.Materials.Find(id) ?? throw new OwnException("¡Material no encontrado!");
                    context.Materials.Remove(material);
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

        public List<Material> GetMaterialesPorSubcontrata(int idSubcontrata)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    List<Material> materiales = context.Materials.ToList().FindAll(el => el.subcontrata_id == idSubcontrata);
                    materiales.Reverse();
                    return materiales;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public void EliminarMaterialesDeSubcontrata(int idSubcontrata)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    List<Material> materiales = context.Materials.ToList().FindAll(el => el.subcontrata_id == idSubcontrata);
                    materiales.ForEach(material =>
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

        public List<Material> GetMateriales()
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    return context.Materials.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("¡Ocurrió un error de conexión! Inténtelo nuevamente más tarde.");
            }
        }

        public List<Material> GetMaterialesPorSubcontrataYTrabajador(int idSubcontrata, String nombre)
        {
            List<Material> materiales = GetMaterialesPorSubcontrata(idSubcontrata);
            return materiales.FindAll(material => material.nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) != -1);
        }
    }
}
