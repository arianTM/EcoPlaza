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

        private void linkRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SigninPage());
        }
    }
}
