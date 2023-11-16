using Presentacion.helpers;
using Presentacion.modals;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for IncidenciasPage.xaml
    /// </summary>
    public partial class IncidenciasPage : Page
    {
        #region Constructor
        public IncidenciasPage()
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

        private void AbrirVentana(object root)
        {
            Window window = (Window)root;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private bool DataGridSeleccionado()
        {
            if (Validador.DataGridSinSeleccion(dgIncidencias))
            {
                MostrarError("¡Seleccione un elemento de la tabla de incidencias!");
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

        #region Opciones de Incidencias (Registrar, Modificar y Eliminar)
        private void Registrar()
        {
            OcultarError();

            // ABRIR VENTANA
            AbrirVentana(new AgregarIncidenciaWindow());
        }

        private void Modificar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            //if (!DataGridSeleccionado()) return;

            // ABRIR VENTANA
            AbrirVentana(new ModificarIncidenciaWindow());
        }

        private void Eliminar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;
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

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Registrar();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }
        #endregion
    }
}
