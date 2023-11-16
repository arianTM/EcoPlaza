using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Presentacion.helpers
{
    internal static class Validador
    {
        #region Campos Vacios o sin Selección
        public static bool TextBoxVacio(TextBox txt)
        {
            return String.IsNullOrWhiteSpace(txt.Text);
        }

        public static bool PasswordBoxVacio(PasswordBox contra)
        {
            return String.IsNullOrWhiteSpace(contra.Password);
        }

        public static bool ComboBoxSinSeleccion(ComboBox cmb)
        {
            return cmb.SelectedIndex.Equals(-1);
        }
        #endregion

        #region Campos sin Formato Numérico
        public static bool TextBoxSinFormatoIntPositivo(TextBox txt)
        {
            return !int.TryParse(txt.Text, out int num) || num <= 0;
        }

        public static bool TextBoxSinFormatoDecimalPositivo(TextBox txt)
        {
            return !decimal.TryParse(txt.Text, out decimal num) || num <= 0;
        }
        #endregion

        #region Campos de Usuario
        public static bool NombreUsuarioSinFormato(TextBox txt)
        {
            Regex userRegex = new Regex("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+");
            return !userRegex.IsMatch(txt.Text);
        }
        #endregion
    }
}
