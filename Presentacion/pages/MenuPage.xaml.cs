using Datos;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Negocio;
using Presentacion.helpers;
using Presentacion.modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private readonly Controlador _controlador = new Controlador();

        #region Constructor
        public MenuPage()
        {
            InitializeComponent();
            MostrarDatosUsuario();
            MostrarSubcontratas();
            MostrarReportes();

            DataContext = this;
        }
        #endregion

        #region Helpers
        private void Navegar(object root)
        {
            // root --> página (new SigninPage(), por ejemplo)
            NavigationService?.Navigate(root);
        }

        private void AbrirVentana(object root)
        {
            Window window = (Window)root;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Closed += VentanaCerrada;
            window.ShowDialog();
        }

        private void VentanaCerrada(object sender, EventArgs e)
        {
            MostrarSubcontratas();
        }

        #endregion

        #region Usuario
        private void MostrarDatosUsuario()
        {
            try
            {
                Usuario usuario = _controlador.GetUsuario(Administrador.GetIdUsuario());
                tbUsuario.Text = usuario.username;
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Subcontratas

        private void MostrarSubcontratas()
        {
            try
            {
                List<Subcontrata> subcontratas = _controlador.GetSubcontratas();
                icSubcontratas.ItemsSource = subcontratas;
            }
            catch (Exception)
            {

            }
        }

        private void VerSubcontrata(object sender)
        {
            // OBTENER ID SUBCONTRATA DEL BOTÓN
            Button btnVerSubcontrata = (Button)sender;
            int idSeleccionado = int.Parse(btnVerSubcontrata.Tag.ToString());

            // GUARDAR ID SUBCONTRATA EN ADMINISTRADOR
            Administrador.SetIdSubcontrata(idSeleccionado);

            // NAVEGAR
            Navegar(new SubcontrataPage());
        }

        #endregion

        #region Dashboard (reportes)

        #region Reporte 1

        public SeriesCollection R1SeriesCollection { get; set; }

        private void MostrarReporte1()
        {
            R1SeriesCollection = new SeriesCollection();

            try
            {
                List<String> marcas = _controlador.GetMarcas();
                List<int> cantidades = _controlador.MaterialesPorMarca(marcas);

                for (int i = 0; i < marcas.Count; i++)
                {
                    R1SeriesCollection.Add(new PieSeries()
                    {
                        Title = marcas.ElementAt(i),
                        Values = new ChartValues<ObservableValue> { new ObservableValue(cantidades.ElementAt(i)) }
                    });
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Reporte 2

        private ObservableCollection<R2ViewModel> _marcasViewModelOC { get; set; }

        private void MostrarReporte2()
        {
            _marcasViewModelOC = new ObservableCollection<R2ViewModel> { new R2ViewModel() };
            try
            {
                _marcasViewModelOC = new ObservableCollection<R2ViewModel>(_controlador.Top5MarcasPorCostoTotal());
                dgReporte2.ItemsSource = _marcasViewModelOC;
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Reporte 3

        public SeriesCollection R3SeriesCollection { get; set; }
        public string[] R3Labels { get; set; }
        public Func<double, string> R3YFormatter { get; set; }

        private void MostrarReporte3()
        {
            DateTime fechaActual = DateTime.Now;

            List<String> ultimasFechas = new List<String>();

            for (int i = 1; i <= 7; i++)
            {
                ultimasFechas.Add(fechaActual.ToString("yyyy-MM-dd"));

                fechaActual = fechaActual.AddDays(-1);
            }

            ultimasFechas.Reverse();

            R3Labels = ultimasFechas.ToArray();

            R3SeriesCollection = new SeriesCollection();

            try
            {
                List<Subcontrata> subcontratas = _controlador.GetSubcontratas();

                subcontratas.ForEach(subcontrata =>
                {
                    List<int> asistencias = _controlador.AsistenciasPorSubcontrata(subcontrata.id);

                    R3SeriesCollection.Add(new LineSeries
                    {
                        Title = subcontrata.nombre,
                        Values = new ChartValues<int>(asistencias)
                    });
                });
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Reporte 4
        private void MostrarReporte4()
        {
            try
            {
                tbAsistenciasPorFecha.Text = _controlador.AsistenciasPorDia(dpFechaAsistencias.SelectedDate).ToString();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Reporte 5

        public SeriesCollection R5SeriesCollection { get; set; }

        private void MostrarReporte5()
        {
            R5SeriesCollection = new SeriesCollection();

            try
            {
                List<String> categorias = _controlador.GetCategorias();
                List<int> cantidades = _controlador.IncidenciasPorCategoria(categorias);

                for (int i = 0; i < categorias.Count; i++)
                {
                    R5SeriesCollection.Add(new PieSeries
                    {
                        Title = categorias.ElementAt(i),
                        Values = new ChartValues<ObservableValue> { new ObservableValue(cantidades.ElementAt(i)) },
                        DataLabels = true
                    });
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Reporte 6

        private List<Incidencia> _incidenciasPorFecha { get; set; }
        private Incidencia _incidenciaSeleccionada { get; set; }
        private int indiceCarruselIncidencias = 0;

        private void MostrarReporte6()
        {
            _incidenciasPorFecha = new List<Incidencia>();
            _incidenciaSeleccionada = new Incidencia();
            indiceCarruselIncidencias = 0;
            try
            {
                _incidenciasPorFecha = _controlador.IncidenciasPorFecha(dpFechaIncidencias.SelectedDate);
                _incidenciaSeleccionada = _incidenciasPorFecha.Count > 0 ? _incidenciasPorFecha[0] : null;
                MostrarTarjetaActual();
            }
            catch (Exception)
            {

            }
        }

        private void MostrarTarjetaActual()
        {
            if (_incidenciaSeleccionada != null)
            {
                tbIncidenciaDescripcion.Text = $"\"{_incidenciaSeleccionada.descripcion}\"";
                tbIncidenciaHora.Text = $"Hora: {_incidenciaSeleccionada.hora}";
                tbIncidenciaCategoria.Text = $"Categoría: {_incidenciaSeleccionada.categoria}";
            }
            else
            {
                tbIncidenciaDescripcion.Text = "¡No hay incidencias en esta fecha!";
                tbIncidenciaCategoria.Text = String.Empty;
                tbIncidenciaHora.Text = String.Empty;
            }
        }

        private void Avanzar()
        {
            if (indiceCarruselIncidencias < _incidenciasPorFecha.Count() - 1)
            {
                indiceCarruselIncidencias++;
                _incidenciaSeleccionada = _incidenciasPorFecha[indiceCarruselIncidencias];
                MostrarTarjetaActual();
            }
        }

        private void Retroceder()
        {
            if (indiceCarruselIncidencias > 0)
            {
                indiceCarruselIncidencias--;
                _incidenciaSeleccionada = _incidenciasPorFecha[indiceCarruselIncidencias];
                MostrarTarjetaActual();
            }
        }

        #endregion

        private void MostrarReportes()
        {
            MostrarReporte1();
            MostrarReporte2();
            MostrarReporte3();

            dpFechaAsistencias.SelectedDate = DateTime.Now;

            MostrarReporte4();
            MostrarReporte5();

            dpFechaIncidencias.SelectedDate = DateTime.Now;

            MostrarReporte6();
        }

        #endregion

        #region Eventos de Controles

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new UsuarioPage());
        }

        private void btnIncidencias_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new IncidenciasPage());
        }

        private void btnAgregarSubcontrata_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentana(new AgregarSubcontrataWindow());
        }

        private void btnVerSubcontrata_Click(object sender, RoutedEventArgs e)
        {
            VerSubcontrata(sender);
        }

        private void dpFechaAsistencias_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarReporte4();
        }

        private void btnCarruselIncidenciasAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Avanzar();
        }

        private void btnCarruselIncidenciasRetroceder_Click(object sender, RoutedEventArgs e)
        {
            Retroceder();
        }

        private void dpFechaIncidencias_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarReporte6();
        }

        #endregion
    }
}
