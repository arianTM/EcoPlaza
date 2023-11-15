using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarSubcontrataWindow.xaml
    /// </summary>
    public partial class AgregarSubcontrataWindow : Window
    {
        public AgregarSubcontrataWindow()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
