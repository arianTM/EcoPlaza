using Datos;
using Negocio;
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
        private readonly Controlador _controlador = new Controlador();
        private readonly int _idAsistencia = 0;
        #region Constructor
        public ModificarAsistenciaWindow(int idAsistencia)
        {
            InitializeComponent();
            _idAsistencia = idAsistencia;
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

        #region Opciones del formulario (Mostrar datos, Modificar y Limpiar)

        private void Mostrar()
        {
            try
            {
                Asistencia asistencia = _controlador.GetAsistencia(_idAsistencia);
                txtNombreTrabajador.Text = asistencia.trabajador;
                dpFecha.SelectedDate = asistencia.fecha;
                tpHora.SelectedTime = asistencia.fecha + asistencia.hora;
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void Modificar()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            // CREAR OBJETO ASISTENCIA
            Asistencia asistencia = new Asistencia()
            {
                id = _idAsistencia,
                trabajador = txtNombreTrabajador.Text,
                fecha = dpFecha.SelectedDate.Value,
                hora = tpHora.SelectedTime.Value.TimeOfDay,
                updated_at = DateTime.Now,
                updated_by = Administrador.GetIdUsuario()
            };

            try
            {
                // MODIFICAR
                _controlador.ModificarAsistencia(asistencia);

                // CERRAR FORMULARIO
                Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

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
            Mostrar();
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
