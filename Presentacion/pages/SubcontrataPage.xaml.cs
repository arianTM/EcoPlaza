using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for SubcontrataPage.xaml
    /// </summary>
    public partial class SubcontrataPage : Page
    {
        public SubcontrataPage()
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MenuPage());
        }

        private void btnAsistencias_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new AsistenciasPage());
        }
    }
}
