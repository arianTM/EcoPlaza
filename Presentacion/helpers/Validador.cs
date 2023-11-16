using MaterialDesignThemes.Wpf;
using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Presentacion.helpers
{
    internal static class Validador
    {
        #region Campos Vacios o sin Selección
        public static bool TextBoxVacio(TextBox txt)
        {
            return String.IsNullOrWhiteSpace(txt.Text);
        }

        public static bool RichTextBoxVacio(RichTextBox txt)
        {
            TextRange textRange = new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd);
            return String.IsNullOrWhiteSpace(textRange.Text);
        }

        public static bool PasswordBoxVacio(PasswordBox contra)
        {
            return String.IsNullOrWhiteSpace(contra.Password);
        }

        public static bool ComboBoxSinSeleccion(ComboBox cmb)
        {
            return cmb.SelectedIndex.Equals(-1);
        }

        public static bool DataGridSinSeleccion(DataGrid dg)
        {
            return dg.SelectedIndex.Equals(-1);
        }

        public static bool DatePickerSinSeleccion(DatePicker dp)
        {
            return dp.SelectedDate == null;
        }

        public static bool TimePickerSinSeleccion(TimePicker tp)
        {
            return tp.SelectedTime == null;
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

        public static bool ContraSinFormato(PasswordBox contra)
        {
            Regex passwordRegex = new Regex("^[a-zA-Z0-9_@#$%^+=-]{5,30}$");
            return !passwordRegex.IsMatch(contra.Password);
        }
        #endregion
    }
}
