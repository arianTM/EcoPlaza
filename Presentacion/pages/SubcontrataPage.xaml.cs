using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for SubcontrataPage.xaml
    /// </summary>
    public partial class SubcontrataPage : Page
    {
        #region Constructor
        public SubcontrataPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Módulos
        /// </summary>
        private void Navegar(object root)
        {
            // root --> página (new SigninPage(), por ejemplo)
            NavigationService?.Navigate(root);
        }

        private MessageBoxResult MostrarDecision(String mensaje, String titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }

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

        #region Opciones de Subcontrata (Modificar subcontrata, Eliminar subcontrata)

        private void Modificar()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Guardar cambios?", "MODIFICACIÓN");

            if (result == MessageBoxResult.Yes)
            {
                // GUARDAR CAMBIOS
            }
        }

        private void Eliminar()
        {
            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Eliminar subcontrata?", "ELIMINACIÓN");

            if (result == MessageBoxResult.Yes)
            {
                // ELIMINAR SUBCONTRATA

                // NAVEGAR
                Navegar(new MenuPage());
            }
        }

        #endregion

        #region Eventos de Controles

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MenuPage());
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }

        private void btnAsistencias_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new AsistenciasPage());
        }

        private void btnMateriales_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MaterialesPage());
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

        #endregion
    }
}
