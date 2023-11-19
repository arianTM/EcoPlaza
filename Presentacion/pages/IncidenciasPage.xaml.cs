using Datos;
using Negocio;
using Presentacion.helpers;
using Presentacion.modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    #region ViewModel de Incidencias
    /// <summary>
    /// El ViewModel crea una clase que guarda los datos que se desean mostrar
    /// Todos los datos son String ya que solo se usarán para mostrar
    /// El id se guarda para la seleccion
    /// </summary>
    internal class IncidenciaViewModel
    {
        public int id { get; set; }
        public String descripcion { get; set; }
        public String categoria { get; set; }
        public String fecha_hora { get; set; }
        public String created_by { get; set; }
        public String created_at { get; set; }
        public String updated_by { get; set; }
        public String updated_at { get; set; }
    }
    #endregion

    /// <summary>
    /// Interaction logic for IncidenciasPage.xaml
    /// </summary>
    public partial class IncidenciasPage : Page
    {
        private readonly Controlador _controlador = new Controlador();
        /// <summary>
        /// _incidenciasOC guarda los registros de incidencias con el formato de IncidenciaViewModel
        /// </summary>
        private ObservableCollection<IncidenciaViewModel> _incidenciasOC { get; set; }

        #region Constructor
        public IncidenciasPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Módulos
        /// </summary>
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

        private MessageBoxResult MostrarDecision(String mensaje, String titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }

        private bool DataGridSeleccionado()
        {
            if (Validador.DataGridSinSeleccion(dgIncidencias))
            {
                MostrarError("¡Seleccione un elemento de la tabla de incidencias!");
                return false;
            }

            return true;
        }

        private void VentanaCerrada(object sender, EventArgs e)
        {
            MostrarDatos();
        }

        private int ObtenerIdSeleccionado()
        {
            IncidenciaViewModel incidenciaSeleccionada = (IncidenciaViewModel)dgIncidencias.SelectedItem;
            return incidenciaSeleccionada.id;
        }

        #endregion

        #region Errores
        private void OcultarError()
        {
            tbError.Text = String.Empty;
            tbError.Visibility = Visibility.Hidden;
        }

        private void MostrarError(String mensaje)
        {
            tbError.Text = mensaje;
            tbError.Visibility = Visibility.Visible;
        }
        #endregion

        #region Opciones de Incidencias (Mostrar, Registrar, Modificar y Eliminar)

        private void MostrarDatos()
        {
            List<Incidencia> incidencias = new List<Incidencia>();
            try
            {
                incidencias = _controlador.GetIncidencias();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            _incidenciasOC = new ObservableCollection<IncidenciaViewModel>();

            incidencias.ForEach(incidencia =>
            {
                DateTime fecha = incidencia.fecha;
                TimeSpan hora = incidencia.hora;
                DateTime fechaIncidencia = fecha.Add(hora);

                Usuario creador = _controlador.GetUsuario(incidencia.created_by);
                Usuario editor = _controlador.GetUsuario(incidencia.updated_by);

                IncidenciaViewModel incidenciaViewModel = new IncidenciaViewModel()
                {
                    id = incidencia.id,
                    descripcion = incidencia.descripcion,
                    categoria = incidencia.categoria,
                    fecha_hora = fechaIncidencia.ToString("dd/MM/yyyy HH:mm"),
                    created_by = creador.username,
                    created_at = incidencia.created_at.ToString("dd/MM/yyyy HH:mm"),
                    updated_by = editor.username,
                    updated_at = incidencia.updated_at.ToString("dd/MM/yyyy HH:mm"),
                };

                _incidenciasOC.Add(incidenciaViewModel);
            });

            dgIncidencias.ItemsSource = _incidenciasOC;
        }

        private void Registrar()
        {
            OcultarError();

            // ABRIR VENTANA
            AbrirVentana(new AgregarIncidenciaWindow());
        }

        private void Modificar()
        {
            OcultarError();

            //VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;

            // GUARDAR ID DE LA INCIDENCIA SELECCIONADA
            int idIncidencia = ObtenerIdSeleccionado();

            // ABRIR VENTANA
            AbrirVentana(new ModificarIncidenciaWindow(idIncidencia));
        }

        private void Eliminar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;

            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Eliminar incidencia?", "ELIMINACIÓN");

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ

            // GUARDAR ID DE LA INCIDENCIA SELECCIONADA
            int idIncidencia = ObtenerIdSeleccionado();

            try
            {
                // ELIMINAR INCIDENCIA
                _controlador.EliminarIncidencia(idIncidencia);

                // MOSTRAR DATOS
                MostrarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
        #endregion

        #region Eventos de Controles
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
            MostrarDatos();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MenuPage());
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Registrar();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }
        #endregion
    }
}
