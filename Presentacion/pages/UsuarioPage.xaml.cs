using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for UsuarioPage.xaml
    /// </summary>
    public partial class UsuarioPage : Page
    {
        public UsuarioPage()
        {
            InitializeComponent();
        }

        private void Navegar(object root)
        {
            // root --> página (new SigninPage(), por ejemplo)
            NavigationService?.Navigate(root);
        }

        private void Modificar()
        {

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
            Navegar(new LoginPage());
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new LoginPage());
        }
    }
}
