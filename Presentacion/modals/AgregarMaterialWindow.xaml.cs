using Presentacion.helpers;
using System;
using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarMaterialWindow.xaml
    /// </summary>
    public partial class AgregarMaterialWindow : Window
    {
        #region Constructor
        public AgregarMaterialWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
        private bool ValidarCampos()
        {
            if (Validador.TextBoxVacio(txtNombre) || Validador.ComboBoxSinSeleccion(cbMarca) || Validador.TextBoxVacio(txtCantidad) || Validador.TextBoxVacio(txtCosto))
            {
                MostrarError("¡Complete los campos!");
                return false;
            }

            if (Validador.TextBoxSinFormatoIntPositivo(txtCantidad))
            {
                MostrarError("¡El campo 'Cantidad! debe ser entero positivo!");
                return false;
            }

            if (Validador.TextBoxSinFormatoDecimalPositivo(txtCosto))
            {
                MostrarError("¡El campo 'Costo! debe ser decimal positivo!");
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
            cbMarca.SelectedIndex = -1;
            txtCantidad.Clear();
            txtCosto.Clear();
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

        private void txtNombre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void cbMarca_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtCantidad_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtCosto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
