using Datos;
using Negocio;
using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for SigninPage.xaml
    /// </summary>
    public partial class SigninPage : Page
    {
        private readonly Controlador _controlador = new Controlador();

        #region Constructor
        public SigninPage()
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

        private bool ValidarCampos()
        {
            if (Validador.TextBoxVacio(txtNombreUsuario) || Validador.TextBoxVacio(txtNombresYApellidos) || Validador.PasswordBoxVacio(txtContra))
            {
                MostrarError("¡Complete todos los campos!");
                return false;
            }

            if (Validador.NombreUsuarioSinFormato(txtNombreUsuario))
            {
                MostrarError("¡Formato no válido del usuario (poner el mouse sobre el campo para ver detalles)!");
                return false;
            }

            if (Validador.ContraSinFormato(txtContra))
            {
                MostrarError("¡Formato no válido de la contraseña (poner el mouse sobre el campo para ver detalles)!");
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

        #region Registro de Usuario
        /// <summary>
        /// Funciones para el Sign-in del usuario
        /// </summary>

        private void Registrar()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            // CREAR OBJETO USUARIO
            Usuario usuario = new Usuario()
            {
                username = txtNombreUsuario.Text,
                nombre = txtNombresYApellidos.Text,
                contra = txtContra.Password,
                activo = true
            };

            try
            {
                // REGISTRAR USUARIO
                _controlador.RegistrarUsuario(usuario);

                // GUARDAR CLAVE DEL USUARIO
                Administrador.SetIdUsuario(_controlador.GetIdUsuario(usuario.username, usuario.contra));

                // NAVEGAR
                Navegar(new MenuPage());
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
        }

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Registrar();
        }
        private void linkIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new LoginPage());
        }

        private void txtNombreUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtNombresYApellidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtContra_PasswordChanged(object sender, RoutedEventArgs e)
        {
            OcultarError();
        }
        #endregion
    }
}
