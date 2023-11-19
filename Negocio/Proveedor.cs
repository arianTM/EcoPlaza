using System;
using System.Collections.Generic;

namespace Negocio
{
    /// <summary>
    /// Clase que provee entidades en código (que no se deben almacenar como base de datos)
    /// </summary>
    public static class Proveedor
    {
        public static List<String> GetMarcas()
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

        public static List<String> GetCategorias()
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
    }
}
