using Presentacion.modals;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for MaterialesPage.xaml
    /// </summary>
    public partial class MaterialesPage : Page
    {
        public MaterialesPage()
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
            AbrirVentana(new AgregarMaterialWindow());
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentana(new ModificarMaterialWindow());
        }
    }
}
