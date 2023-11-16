using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarAsistenciaWindow.xaml
    /// </summary>
    public partial class AgregarAsistenciaWindow : Window
    {
        #region Constructor
        public AgregarAsistenciaWindow()
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

        #region Opciones del formulario (Agregar y Limpiar)
        private void Registrar()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Registrar();
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
