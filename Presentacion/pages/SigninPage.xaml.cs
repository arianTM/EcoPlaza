using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for SigninPage.xaml
    /// </summary>
    public partial class SigninPage : Page
    {
        public SigninPage()
        {
            InitializeComponent();
        }

        private void Navegar(object root)
        {
            // root --> página (new SigninPage(), por ejemplo)
            NavigationService?.Navigate(root);
        }

        private void linkIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new LoginPage());
        }

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MenuPage());
        }
    }
}
