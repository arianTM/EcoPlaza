using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarAsistenciaWindow.xaml
    /// </summary>
    public partial class AgregarAsistenciaWindow : Window
    {
        public AgregarAsistenciaWindow()
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
