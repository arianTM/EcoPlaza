using Datos;
using Datos.repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    #region Controlador
    public class Controlador
    {
        #region CRUD
        #region Asistencias
        private readonly DAsistencia _dAsistencia = new DAsistencia();

        public void RegistrarAsistencia(Asistencia asistencia)
        {
            _dAsistencia.Registrar(asistencia);
        }

        public Asistencia GetAsistencia(int id)
        {
            return _dAsistencia.GetAsistencia(id);
        }

        public void ModificarAsistencia(Asistencia asistencia)
        {
            _dAsistencia.Modificar(asistencia);
        }

        public void EliminarAsistencia(int id)
        {
            _dAsistencia.Eliminar(id);
        }

        public List<Asistencia> GetAsistenciasPorSubcontrata(int idSubcontrata)
        {
            return _dAsistencia.GetAsistenciasPorSubcontrata(idSubcontrata);
        }

        public void EliminarAsistenciasDeSubcontrata(int idSubcontrata)
        {
            _dAsistencia.EliminarAsistenciasDeSubcontrata(idSubcontrata);
        }

        private List<Asistencia> GetAsistencias()
        {
            return _dAsistencia.GetAsistencias();
        }

        #endregion

        #region Incidencia
        private readonly DIncidencia _dIncidencia = new DIncidencia();

        public void RegistrarIncidencia(Incidencia incidencia)
        {
            _dIncidencia.Registrar(incidencia);
        }

        public Incidencia GetIncidencia(int id)
        {
            return _dIncidencia.GetIncidencia(id);
        }

        public void ModificarIncidencia(Incidencia incidencia)
        {
            _dIncidencia.Modificar(incidencia);
        }

        public void EliminarIncidencia(int id)
        {
            _dIncidencia.Eliminar(id);
        }

        public List<Incidencia> GetIncidencias()
        {
            return _dIncidencia.GetIncidencias();
        }

        #endregion

        #region Material
        private readonly DMaterial _dMaterial = new DMaterial();

        public void RegistrarMaterial(Material material)
        {
            _dMaterial.Registrar(material);
        }

        public Material GetMaterial(int id)
        {
            return _dMaterial.GetMaterial(id);
        }

        public void ModificarMaterial(Material material)
        {
            _dMaterial.Modificar(material);
        }

        public void EliminarMaterial(int id)
        {
            _dMaterial.Eliminar(id);
        }

        public List<Material> GetMaterialesPorSubcontrata(int idSubcontrata)
        {
            return _dMaterial.GetMaterialesPorSubcontrata(idSubcontrata);
        }

        public void EliminarMaterialesDeSubcontrata(int idSubcontrata)
        {
            _dMaterial.EliminarMaterialesDeSubcontrata(idSubcontrata);
        }

        private List<Material> GetMateriales()
        {
            return _dMaterial.GetMateriales();
        }

        #endregion

        #region Subcontrata
        private readonly DSubcontrata _dSubcontrata = new DSubcontrata();

        public void RegistrarSubcontrata(Subcontrata subcontrata)
        {
            _dSubcontrata.Registrar(subcontrata);
        }

        public Subcontrata GetSubcontrata(int id)
        {
            return _dSubcontrata.GetSubcontrata(id);
        }

        public void ModificarSubcontrata(Subcontrata subcontrata)
        {
            _dSubcontrata.Modificar(subcontrata);
        }

        /// <summary>
        /// Antes de eliminar la subcontrata, se deben eliminar sus asistencias y sus materiales
        /// </summary>
        public void EliminarSubcontrata(int id)
        {
            _dAsistencia.EliminarAsistenciasDeSubcontrata(id);
            _dMaterial.EliminarMaterialesDeSubcontrata(id);
            _dSubcontrata.Eliminar(id);
        }

        public List<Subcontrata> GetSubcontratas()
        {
            return _dSubcontrata.GetSubcontratas();
        }

        #endregion

        #region Usuario
        private readonly DUsuario _dUsuario = new DUsuario();

        public void CargaInicial()
        {
            _dUsuario.CargaInicial();
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            _dUsuario.Registrar(usuario);
        }

        public int GetIdUsuario(String username, String contra)
        {
            return _dUsuario.GetIdUsuario(username, contra);
        }

        public Usuario GetUsuario(int id)
        {
            return _dUsuario.GetUsuario(id);
        }

        public void ModificarUsuario(Usuario usuario)
        {
            _dUsuario.Modificar(usuario);
        }

        public void EliminarUsuario(int id)
        {
            _dUsuario.Eliminar(id);
        }

        #endregion

        #endregion

        #region Reportes

        #region Reporte 1
        /// <summary>
        /// Cantidad de Materiales por Marca (DeWalt, Makita, ...)
        /// </summary>
        public List<int> MaterialesPorMarca(List<String> marcas)
        {
            List<int> materialesPorCategoria = new List<int>();
            List<Material> materiales = GetMateriales();
            marcas.ForEach(marca =>
            {
                int cantidad = materiales.Count(m => m.marca.Equals(marca));
                materialesPorCategoria.Add(cantidad);
            });
            return materialesPorCategoria;
        }
        #endregion

        #region Reporte 2
        /// <summary>
        /// 5 MARCAS CON MAS COSTOS
        /// </summary>
        public List<R2ViewModel> Top5MarcasPorCostoTotal()
        {
            List<R2ViewModel> resultado = new List<R2ViewModel>();

            // Marcas y materiales
            List<String> marcas = GetMarcas();
            List<Material> materiales = GetMateriales();

            marcas.ForEach(marca =>
            {
                List<Material> materialesPorMarca = materiales.FindAll(m => m.marca.Equals(marca));

                // Extraer los ids únicos de las subcontratas, de todos los materiales de una marca
                List<int> idSubcontratas = new List<int>();
                materialesPorMarca.ForEach(material =>
                {
                    idSubcontratas.Add(material.subcontrata_id);
                });
                idSubcontratas = idSubcontratas.Distinct().ToList();

                // Obtener los nombres de las subcontratas
                String subcontratas = String.Empty;
                idSubcontratas.ForEach(id =>
                {
                    String nombre = GetSubcontrata(id).nombre;
                    subcontratas += $"{nombre}, ";
                });

                // Guardar datos
                R2ViewModel fila = new R2ViewModel();
                fila.Marca = marca;
                fila.Cantidad = materialesPorMarca.Count();
                fila.Costo = materialesPorMarca.Sum(m => m.costo * m.cantidad);
                fila.Subcontratas = subcontratas;

                // Añadir a la lista
                resultado.Add(fila);
            });

            // Ordenar descendentemente según costo
            resultado.Sort((x, y) => y.Costo.CompareTo(x.Costo));

            // Retornar los 5 primeros
            return resultado.Take(5).ToList();
        }

        #endregion

        #region Reporte 3
        /// <summary>
        /// Números de asistencias por cada día durante los últimos 7 días
        /// </summary>
        public List<int> AsistenciasPorSubcontrata(int idSubcontrata)
        {
            List<int> numeroAsistencias = new List<int>();

            DateTime fecha = DateTime.Now;

            for (int i = 1; i <= 7; i++)
            {
                int asistenciasPorSubcontrata = GetAsistenciasPorSubcontrata(idSubcontrata).Count(a => a.fecha.Day == fecha.Day);

                numeroAsistencias.Add(asistenciasPorSubcontrata); // numero asistencias subcontrata

                fecha = fecha.AddDays(-1);
            }

            numeroAsistencias.Reverse();

            return numeroAsistencias;
        }

        #endregion

        #region Reporte 4

        public int AsistenciasPorDia(DateTime? fecha)
        {
            List<Asistencia> asistencias = GetAsistencias();
            return asistencias.Count(a => a.fecha.Day == fecha.Value.Day);
        }

        #endregion

        #region Reporte 5
        /// <summary>
        /// REPORTE 5: Número de incidencias por Categoría (Robo, Accidente, ...)
        /// </summary>
        public List<int> IncidenciasPorCategoria(List<String> categorias)
        {
            List<int> incidenciasPorCategoria = new List<int>();
            List<Incidencia> incidencias = GetIncidencias();
            categorias.ForEach(categoria =>
            {
                int cantidad = incidencias.Count(i => i.categoria.Equals(categoria));
                incidenciasPorCategoria.Add(cantidad);
            });
            return incidenciasPorCategoria;
        }

        #endregion

        #endregion

        #region Tablas estáticas

        public List<String> GetMarcas()
        {
            return new List<String>()
            {
                "DeWalt",
                "Makita",
                "Bosch",
                "Milwaukee",
                "Hilti",
                "Stanley",
                "Metabo HPT",
                "Caterpillar",
                "Ryobi",
            };
        }

        public List<String> GetCategorias()
        {
            return new List<String>()
            {
                "Robo",
                "Desperfecto",
                "Retraso",
                "Accidente",
                "Riesgo",
                "Otro",
            };
        }

        #endregion

    }

    #endregion

    #region View Models

    public class R2ViewModel
    {
        public String Marca { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public String Subcontratas { get; set; }
    }

    #endregion
}
