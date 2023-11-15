using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Navegar(object root)
        {
            // root --> página (new SigninPage(), por ejemplo)
            NavigationService?.Navigate(root);
        }

        private void linkRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new SigninPage());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MenuPage());
        }
    }
}
