﻿using Datos;
using Datos.repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
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

        /// <summary>
        /// REPORTE 1: Cantidad de Materiales por Marca
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

        /// <summary>
        /// REPORTE 5: Número de incidencias por cada categoría (Robo, Accidente, ...)
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
}