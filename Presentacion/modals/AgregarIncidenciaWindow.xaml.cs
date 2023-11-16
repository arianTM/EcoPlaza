using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarIncidenciaWindow.xaml
    /// </summary>
    public partial class AgregarIncidenciaWindow : Window
    {
        public AgregarIncidenciaWindow()
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
