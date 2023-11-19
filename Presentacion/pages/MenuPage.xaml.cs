﻿using Datos;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Negocio;
using Presentacion.helpers;
using Presentacion.modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            List<String> marcas = _controlador.GetMarcas();
            List<int> cantidades = _controlador.MaterialesPorMarca(marcas);

            R1SeriesCollection = new SeriesCollection();

            for (int i = 0; i < marcas.Count; i++)
            {
                R1SeriesCollection.Add(new PieSeries()
                {
                    Title = marcas.ElementAt(i),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(cantidades.ElementAt(i)) }
                });
            }
        }

        #endregion

        #region Reporte 2
        private void MostrarReporte2()
        {

        }
        #endregion

        #region Reporte 3

        public SeriesCollection R3SeriesCollection { get; set; }
        public string[] R3Labels { get; set; }
        public Func<double, string> R3YFormatter { get; set; }

        private void MostrarReporte3()
        {
            R3SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            R3Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            R3YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            R3SeriesCollection.Add(new LineSeries
            {
                Title = "Series 4",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeground = Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            R3SeriesCollection[3].Values.Add(5d);
        }
        #endregion

        #region Reporte 4
        private void MostrarReporte4()
        {

        }
        #endregion

        #region Reporte 5

        public SeriesCollection R5SeriesCollection { get; set; }

        private void MostrarReporte5()
        {
            R5SeriesCollection = new SeriesCollection();

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
        #endregion

        #region Reporte 6
        private void MostrarReporte6()
        {

        }
        #endregion

        private void MostrarReportes()
        {
            MostrarReporte1();
            MostrarReporte2();
            MostrarReporte3();
            MostrarReporte4();
            MostrarReporte5();
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
        #endregion
    }
}
