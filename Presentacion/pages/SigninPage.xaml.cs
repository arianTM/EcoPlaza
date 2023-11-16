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

        #endregion

        #region Mensajes de Error

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

        private bool Camposss()
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

            return true;
        }

        private void Registrar()
        {
            // VALIDAR CAMPOS
            if (!Camposss()) return;

            // REGISTRAR USUARIO
            Navegar(new MenuPage());
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
        #endregion

    }
}
