using Presentacion.helpers;
using System;
using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for ModificarIncidenciaWindow.xaml
    /// </summary>
    public partial class ModificarIncidenciaWindow : Window
    {
        #region Constructor
        public ModificarIncidenciaWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
        private bool ValidarCampos()
        {
            if (Validador.RichTextBoxVacio(txtDescripcion) || Validador.ComboBoxSinSeleccion(cbCategoria) || Validador.DatePickerSinSeleccion(dpFecha) || Validador.TimePickerSinSeleccion(tpHora))
            {
                MostrarError("¡Complete los campos!");
                return false;
            }

            return true;
        }
        #endregion

        #region Errores

        private void OcultarError()
        {
            tbError.Text = String.Empty;
            tbError.Visibility = Visibility.Hidden;
        }

        private void MostrarError(String mensaje)
        {
            tbError.Text = mensaje;
            tbError.Visibility = Visibility.Visible;
        }
        #endregion

        #region Opciones del formulario (Modificar y Limpiar)
        private void Modificar()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            // CERRAR FORMULARIO
            Close();
        }

        private void Limpiar()
        {
            txtDescripcion.Document.Blocks.Clear();
            cbCategoria.SelectedIndex = -1;
            dpFecha.SelectedDate = null;
            tpHora.SelectedTime = null;
        }
        #endregion

        #region Eventos de Controles
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar();
        }

        private void txtDescripcion_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void cbCategoria_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            OcultarError();
        }

        private void dpFecha_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            OcultarError();
        }

        private void tpHora_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<System.DateTime?> e)
        {
            OcultarError();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        #endregion
    }
}
