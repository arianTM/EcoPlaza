using Presentacion.helpers;
using System;
using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for ModificarAsistenciaWindow.xaml
    /// </summary>
    public partial class ModificarAsistenciaWindow : Window
    {
        #region Constructor
        public ModificarAsistenciaWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers

        private bool ValidarCampos()
        {
            if (Validador.TextBoxVacio(txtNombreTrabajador) || Validador.DatePickerSinSeleccion(dpFecha) || Validador.TimePickerSinSeleccion(tpHora))
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
            txtNombreTrabajador.Clear();
            dpFecha.SelectedDate = null;
            tpHora.SelectedTime = null;
        }
        #endregion

        #region Eventos de Controles
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
        }

        private void txtNombreTrabajador_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
