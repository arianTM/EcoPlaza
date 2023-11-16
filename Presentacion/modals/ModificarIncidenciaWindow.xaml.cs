using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for ModificarIncidenciaWindow.xaml
    /// </summary>
    public partial class ModificarIncidenciaWindow : Window
    {
        public ModificarIncidenciaWindow()
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
