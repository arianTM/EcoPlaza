using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarSubcontrataWindow.xaml
    /// </summary>
    public partial class AgregarSubcontrataWindow : Window
    {
        #region Constructor
        public AgregarSubcontrataWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
        private bool ValidarCampos()
        {
            if (Validador.TextBoxVacio(txtNombre) || Validador.RichTextBoxVacio(txtDescripcion) || Validador.TextBoxVacio(txtRuc) || Validador.TextBoxVacio(txtCelular))
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
            txtNombre.Clear();
            txtDescripcion.Document.Blocks.Clear();
            txtRuc.Clear();
            txtCelular.Clear();
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

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtRuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtCelular_TextChanged(object sender, TextChangedEventArgs e)
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
