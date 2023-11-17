using Negocio.services;
using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private NUsuario _nUsuario = new NUsuario();

        #region Constructor
        public LoginPage()
        {
            InitializeComponent();
            _nUsuario.CargaInicial();
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
            if (Validador.TextBoxVacio(txtNombre) || Validador.PasswordBoxVacio(txtContra))
            {
                MostrarError("¡Complete todos los campos!");
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

        #region Login de Usuario
        /// <summary>
        /// Funciones para el Log-in del usuario
        /// </summary>
        /// 

        private void Login()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            try
            {
                // LOGIN y GUARDAR CLAVE DEL USUARIO
                Administrador.SetIdUsuario(_nUsuario.GetIdUsuario(txtNombre.Text, txtContra.Password));

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

        private void linkRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new SigninPage());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
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
