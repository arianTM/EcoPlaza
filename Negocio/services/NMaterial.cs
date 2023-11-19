﻿using Datos;
using Datos.repositories;
using System;
using System.Collections.Generic;

namespace Negocio.services
{
    public class NMaterial
    {
        private DMaterial _dMaterial = new DMaterial();

        #region CRUD

        public void Registrar(Material material)
        {
            _dMaterial.Registrar(material);
        }

        public Material GetMaterial(int id)
        {
            return _dMaterial.GetMaterial(id);
        }

        public void Modificar(Material material)
        {
            _dMaterial.Modificar(material);
        }

        public void Eliminar(int id)
        {
            _dMaterial.Eliminar(id);
        }

        public List<Material> GetMaterialesPorSubcontrata(int idSubcontrata)
        {
            return _dMaterial.GetMaterialesPorSubcontrata(idSubcontrata);
        }

        #endregion

        #region Reportes

        public List<int> CantidadesPorMarca(List<String> marcas)
        {
            return _dMaterial.CantidadesPorMarca(marcas);
        }

        #endregion
    }
}
