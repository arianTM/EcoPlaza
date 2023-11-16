using Presentacion.modals;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for IncidenciasPage.xaml
    /// </summary>
    public partial class IncidenciasPage : Page
    {
        public IncidenciasPage()
        {
            InitializeComponent();
        }

        private void Navegar(object root)
        {
            // root --> página (new SigninPage(), por ejemplo)
            NavigationService?.Navigate(root);
        }

        private void AbrirVentana(object root)
        {
            Window window = (Window)root;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MenuPage());
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentana(new AgregarIncidenciaWindow());
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentana(new ModificarIncidenciaWindow());
        }
    }
}
