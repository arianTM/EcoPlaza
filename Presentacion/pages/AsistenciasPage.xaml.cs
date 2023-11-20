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
    #region ViewModel de Asistencias
    internal class AsistenciaViewModel
    {
        public int id { get; set; }
        public String trabajador { get; set; }
        public String fecha_hora { get; set; }
        public String created_by { get; set; }
        public String created_at { get; set; }
        public String updated_by { get; set; }
        public String updated_at { get; set; }
    }
    #endregion
    /// <summary>
    /// Interaction logic for AsistenciasPage.xaml
    /// </summary>
    public partial class AsistenciasPage : Page
    {
        private readonly Controlador _controlador = new Controlador();
        /// <summary>
        /// _asistenciasOC guarda los registros de asistencias con el formato de AsistenciaViewModel
        /// </summary>
        private ObservableCollection<AsistenciaViewModel> _asistenciasOC { get; set; }
        #region Constructor
        public AsistenciasPage()
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

        private void VentanaCerrada(object sender, EventArgs e)
        {
            MostrarAsistenciasTotal();
        }

        private MessageBoxResult MostrarDecision(String mensaje, String titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }

        private bool DataGridSeleccionado()
        {
            if (Validador.DataGridSinSeleccion(dgAsistencias))
            {
                MostrarError("¡Seleccione un elemento de la tabla de asistencias!");
                return false;
            }

            return true;
        }

        private int ObtenerIdSeleccionado()
        {
            AsistenciaViewModel asistenciaSeleccionada = (AsistenciaViewModel)dgAsistencias.SelectedItem;
            return asistenciaSeleccionada.id;
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

        #region Opciones de Asistencias (Mostrar datos, Registrar, Modificar y Eliminar)

        private void MostrarDatos(String trabajador = "")
        {
            List<Asistencia> asistencias = new List<Asistencia>();
            try
            {
                asistencias = trabajador == "" ? _controlador.GetAsistenciasPorSubcontrata(Administrador.GetIdSubcontrata()) : _controlador.GetAsistenciasPorSubcontrataYTrabajador(Administrador.GetIdSubcontrata(), trabajador);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            _asistenciasOC = new ObservableCollection<AsistenciaViewModel>();

            asistencias.ForEach(asistencia =>
            {
                DateTime fecha = asistencia.fecha;
                TimeSpan hora = asistencia.hora;
                DateTime fechaAsistencia = fecha.Add(hora);

                Usuario creador = _controlador.GetUsuario(asistencia.created_by);
                Usuario editor = _controlador.GetUsuario(asistencia.updated_by);

                AsistenciaViewModel materialViewModel = new AsistenciaViewModel()
                {
                    id = asistencia.id,
                    trabajador = asistencia.trabajador,
                    fecha_hora = fechaAsistencia.ToString("dd/MM/yyyy HH:mm"),
                    created_by = creador.username,
                    created_at = asistencia.created_at.ToString("dd/MM/yyyy HH:mm"),
                    updated_by = editor.username,
                    updated_at = asistencia.updated_at.ToString("dd/MM/yyyy HH:mm"),
                };

                _asistenciasOC.Add(materialViewModel);
            });

            dgAsistencias.ItemsSource = _asistenciasOC;
        }

        private void MostrarAsistenciasTotal()
        {
            MostrarDatos();
        }

        private void MostrarAsistenciaFiltro(String trabajador)
        {
            MostrarDatos(trabajador);
        }

        private void Registrar()
        {
            OcultarError();

            // ABRIR VENTANA
            AbrirVentana(new AgregarAsistenciaWindow());
        }

        private void Modificar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;

            // GUARDAR ID DE LA ASISTENCIA SELECCIONADA
            int idAsistencia = ObtenerIdSeleccionado();

            // ABRIR VENTANA
            AbrirVentana(new ModificarAsistenciaWindow(idAsistencia));
        }

        private void Eliminar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;

            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Eliminar asistencia?", "ELIMINACIÓN");

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ

            // GUARDAR ID DE LA ASISTENCIA SELECCIONADA
            int idAsistencia = ObtenerIdSeleccionado();

            try
            {
                // ELIMINAR INCIDENCIA
                _controlador.EliminarAsistencia(idAsistencia);

                // MOSTRAR DATOS
                MostrarAsistenciasTotal();
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
            MostrarAsistenciasTotal();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new SubcontrataPage());
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

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            MostrarAsistenciasTotal();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            MostrarAsistenciaFiltro(txtNombreTrabajador.Text);
        }

        #endregion
    }
}
