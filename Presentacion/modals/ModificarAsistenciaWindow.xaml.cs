using System.Windows;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for ModificarAsistenciaWindow.xaml
    /// </summary>
    public partial class ModificarAsistenciaWindow : Window
    {
        public ModificarAsistenciaWindow()
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
