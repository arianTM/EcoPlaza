using Datos;
using Negocio.services;
using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarIncidenciaWindow.xaml
    /// </summary>
    public partial class AgregarIncidenciaWindow : Window
    {
        private NIncidencia _nIncidencia = new NIncidencia();
        #region Constructor
        public AgregarIncidenciaWindow()
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

        private String TextoDeRichTextBox(RichTextBox rtx)
        {
            TextRange text = new TextRange(rtx.Document.ContentStart, rtx.Document.ContentEnd);
            return text.Text;
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

            // GUARDAR DESCRIPCION (RICHTEXTBOX)
            String descripcion = TextoDeRichTextBox(txtDescripcion);

            // CREAR OBJETO INCIDENCIA
            Incidencia incidencia = new Incidencia()
            {
                descripcion = descripcion.Trim(),
                categoria = cbCategoria.Text,
                fecha = dpFecha.SelectedDate.Value,
                hora = tpHora.SelectedTime.Value.TimeOfDay,
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                created_by = Administrador.GetIdUsuario(),
                updated_by = Administrador.GetIdUsuario()
            };

            try
            {
                // REGISTRAR
                _nIncidencia.Registrar(incidencia);

                // CERRAR FORMULARIO
                Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
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

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Registrar();
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
