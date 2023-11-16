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
        #region Constructor
        public LoginPage()
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

        private bool Camposs()
        {
            if (Validador.TextBoxVacio(txtNombre) || Validador.PasswordBoxVacio(txtContra))
            {
                MostrarError("¡Complete todos los campos!");
                return false;
            }

            return true;
        }

        private void Login()
        {
            // VALIDAR CAMPOS
            if (!Camposs()) return;


            //LOGIN


            // NAVEGAR
            Navegar(new MenuPage());
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
        #endregion

    }
}
