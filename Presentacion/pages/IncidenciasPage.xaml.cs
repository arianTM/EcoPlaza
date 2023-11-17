using Datos;
using Negocio.services;
using Presentacion.helpers;
using Presentacion.modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for IncidenciasPage.xaml
    /// </summary>
    public partial class IncidenciasPage : Page
    {
        private NUsuario _nUsuario = new NUsuario();
        private NIncidencia _nIncidencia = new NIncidencia();
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
            try
            {
                List<Incidencia> incidencias = _nIncidencia.GetIncidencias();

                ObservableCollection<object> incidenciasOC = new ObservableCollection<object>();

                incidencias.ForEach(incidencia =>
                {
                    DateTime fecha = incidencia.fecha;
                    TimeSpan hora = incidencia.hora;

                    DateTime fechaIncidencia = fecha.Add(hora);

                    Usuario creador = _nUsuario.GetUsuario(incidencia.created_by);
                    Usuario editor = _nUsuario.GetUsuario(incidencia.updated_by);

                    var incidenciaViewModel = new
                    {
                        descripcion = incidencia.descripcion,
                        categoria = incidencia.categoria,
                        fecha_hora = fechaIncidencia.ToString("dd/MM/yyyy HH:mm"),
                        created_by = creador.username,
                        created_at = incidencia.created_at.ToString("dd/MM/yyyy HH:mm"),
                        updated_by = editor.username,
                        updated_at = incidencia.updated_at.ToString("dd/MM/yyyy HH:mm"),
                    };

                    incidenciasOC.Add(incidenciaViewModel);
                });

                dgIncidencias.ItemsSource = incidenciasOC;
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
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

            // VALIDAR SELECCIÓN
            //if (!DataGridSeleccionado()) return;

            // ABRIR VENTANA
            AbrirVentana(new ModificarIncidenciaWindow());
        }

        private void Eliminar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;
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
