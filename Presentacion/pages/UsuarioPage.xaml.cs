using Datos;
using Negocio;
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
        private readonly Controlador _controlador = new Controlador();

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

            if (Validador.NombreUsuarioSinFormato(txtNombreUsuario))
            {
                MostrarError("¡Formato no válido del usuario (poner el mouse sobre el campo para ver detalles)!");
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

        #region Opciones de usuario (Ver datos, Modificar cuenta, Logout, Eliminar cuenta)

        private void MostrarDatos()
        {
            try
            {
                Usuario usuario = _controlador.GetUsuario(Administrador.GetIdUsuario());
                tbUsuario.Text = txtNombreUsuario.Text = usuario.username;
                txtNombresYApellidos.Text = usuario.nombre;
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
                _controlador.ModificarUsuario(usuario);

                // MOSTRAR DATOS
                MostrarDatos();
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

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ

            // BORRAR EL ID USUARIO DE ADMINISTRADOR
            Administrador.ClearIdUsuario();

            // NAVEGAR
            Navegar(new LoginPage());
        }

        private void Eliminar()
        {
            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Eliminar cuenta?", "ELIMINACIÓN");

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ

            try
            {
                // ELIMINAR CUENTA
                _controlador.EliminarUsuario(Administrador.GetIdUsuario());

                // NAVEGAR
                Navegar(new LoginPage());
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        #endregion

        #region Eventos de Controles
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
            MostrarDatos();
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
