using Datos;
using Negocio.services;
using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for UsuarioPage.xaml
    /// </summary>
    public partial class UsuarioPage : Page
    {
        private NUsuario _nUsuario = new NUsuario();

        #region Constructor
        public UsuarioPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
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
            if (Validador.TextBoxVacio(txtNombreUsuario) || Validador.TextBoxVacio(txtNombresYApellidos))
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

        #region Opciones de usuario (Modificar cuenta, Logout, Eliminar cuenta)
        private void Modificar()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Guardar cambios?", "MODIFICACIÓN");

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ

            // CREAR OBJETO USUARIO
            Usuario usuario = new Usuario()
            {
                id = Administrador.GetIdUsuario(),
                username = txtNombreUsuario.Text,
                nombre = txtNombresYApellidos.Text,
            };

            try
            {
                // GUARDAR CAMBIOS
                _nUsuario.Modificar(usuario);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void Logout()
        {
            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Cerrar sesión?", "LOG OUT");

            if (result == MessageBoxResult.Yes)
            {
                // LOG OUT

                // NAVEGAR
                Navegar(new LoginPage());
            }
        }

        private void Eliminar()
        {
            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Eliminar cuenta?", "ELIMINACIÓN");

            if (result == MessageBoxResult.Yes)
            {
                // ELIMINAR CUENTA

                // NAVEGAR
                Navegar(new LoginPage());
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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }


        private void txtNombreUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtNombresYApellidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }
        #endregion
    }
}
