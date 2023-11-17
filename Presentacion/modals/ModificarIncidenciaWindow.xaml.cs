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
    /// Interaction logic for ModificarIncidenciaWindow.xaml
    /// </summary>
    public partial class ModificarIncidenciaWindow : Window
    {
        private NIncidencia _nIncidencia = new NIncidencia();
        private int _idIncidencia = 0;

        #region Constructor
        public ModificarIncidenciaWindow(int idIncidencia)
        {
            InitializeComponent();
            _idIncidencia = idIncidencia;
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

        private void SetTextToRichTextBox(RichTextBox rtx, String msg)
        {
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(msg)));
            rtx.Document = doc;
        }

        private String TextoDeRichTextBox(RichTextBox rtx)
        {
            TextRange text = new TextRange(rtx.Document.ContentStart, rtx.Document.ContentEnd);
            return text.Text;
        }

        private void SetTextoComboBox(ComboBox cb, String txt)
        {
            foreach (object item in cb.Items)
            {
                if (item is ComboBoxItem cbItem && String.Equals(txt, cbItem.Content.ToString()))
                {
                    cb.SelectedItem = cbItem;
                    break;
                }
            }
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
                Incidencia incidenciaSeleccionada = _nIncidencia.GetIncidencia(_idIncidencia);
                SetTextToRichTextBox(txtDescripcion, incidenciaSeleccionada.descripcion);
                SetTextoComboBox(cbCategoria, incidenciaSeleccionada.categoria);
                dpFecha.SelectedDate = incidenciaSeleccionada.fecha;
                tpHora.SelectedTime = incidenciaSeleccionada.fecha + incidenciaSeleccionada.hora;
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

            // GUARDAR DESCRIPCION (RICHTEXTBOX)
            String descripcion = TextoDeRichTextBox(txtDescripcion);

            // CREAR OBJETO INCIDENCIA
            Incidencia incidencia = new Incidencia()
            {
                id = _idIncidencia,
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
                _nIncidencia.Modificar(incidencia);

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
            Mostrar();
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
