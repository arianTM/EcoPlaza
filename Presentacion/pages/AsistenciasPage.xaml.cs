using Presentacion.modals;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for AsistenciasPage.xaml
    /// </summary>
    public partial class AsistenciasPage : Page
    {
        public AsistenciasPage()
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
            Navegar(new SubcontrataPage());
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentana(new AgregarAsistenciaWindow());
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentana(new ModificarAsistenciaWindow());
        }
    }
}
