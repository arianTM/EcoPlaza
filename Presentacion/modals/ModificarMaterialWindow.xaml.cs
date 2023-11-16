using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for ModificarMaterialWindow.xaml
    /// </summary>
    public partial class ModificarMaterialWindow : Window
    {
        public ModificarMaterialWindow()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
