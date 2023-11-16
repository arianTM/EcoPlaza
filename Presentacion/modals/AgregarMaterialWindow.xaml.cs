using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarMaterialWindow.xaml
    /// </summary>
    public partial class AgregarMaterialWindow : Window
    {
        public AgregarMaterialWindow()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
