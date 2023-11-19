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
    #region ViewModel de Materiales
    /// <summary>
    /// El ViewModel crea una clase que guarda los datos que se desean mostrar
    /// Todos los datos son String ya que solo se usarán para mostrar
    /// El id se guarda para la seleccion
    /// </summary>
    internal class MaterialViewModel
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public String marca { get; set; }
        public String cantidad { get; set; }
        public String costo_unitario { get; set; }
        public String costo_total { get; set; }
        public String created_by { get; set; }
        public String created_at { get; set; }
        public String updated_by { get; set; }
        public String updated_at { get; set; }
    }
    #endregion

    /// <summary>
    /// Interaction logic for MaterialesPage.xaml
    /// </summary>
    public partial class MaterialesPage : Page
    {
        private NMaterial _nMaterial = new NMaterial();
        private NUsuario _nUsuario = new NUsuario();
        /// <summary>
        /// _materialesOC guarda los registros de materiales con el formato de MaterialViewModel
        /// </summary>
        private ObservableCollection<MaterialViewModel> _materialesOC { get; set; }
        #region Constructor
        public MaterialesPage()
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
            MostrarDatos();
        }

        private MessageBoxResult MostrarDecision(String mensaje, String titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }

        private bool DataGridSeleccionado()
        {
            if (Validador.DataGridSinSeleccion(dgMateriales))
            {
                MostrarError("¡Seleccione un elemento de la tabla de materiales!");
                return false;
            }

            return true;
        }

        private int ObtenerIdSeleccionado()
        {
            MaterialViewModel materialSeleccionado = (MaterialViewModel)dgMateriales.SelectedItem;
            return materialSeleccionado.id;
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

        #region Opciones de Materiales (Mostrar datos, Registrar, Modificar y Eliminar)
        private void MostrarDatos()
        {
            List<Material> materiales = new List<Material>();
            try
            {
                materiales = _nMaterial.GetMaterialesPorSubcontrata(Administrador.GetIdSubcontrata());
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            _materialesOC = new ObservableCollection<MaterialViewModel>();

            materiales.ForEach(material =>
            {
                Usuario creador = _nUsuario.GetUsuario(material.created_by);
                Usuario editor = _nUsuario.GetUsuario(material.updated_by);

                MaterialViewModel materialViewModel = new MaterialViewModel()
                {
                    id = material.id,
                    nombre = material.nombre,
                    marca = material.marca,
                    cantidad = material.cantidad.ToString(),
                    costo_unitario = $"S/. {material.costo:0.00}",
                    costo_total = $"S/. {(material.costo * material.cantidad):0.00}",
                    created_by = creador.username,
                    created_at = material.created_at.ToString("dd/MM/yyyy HH:mm"),
                    updated_by = editor.username,
                    updated_at = material.updated_at.ToString("dd/MM/yyyy HH:mm"),
                };

                _materialesOC.Add(materialViewModel);
            });

            dgMateriales.ItemsSource = _materialesOC;
        }
        private void Registrar()
        {
            OcultarError();

            // ABRIR VENTANA
            AbrirVentana(new AgregarMaterialWindow());
        }

        private void Modificar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;

            // GUARDAR ID DEL MATERIAL SELECCIONADO
            int idMaterial = ObtenerIdSeleccionado();

            // ABRIR VENTANA
            AbrirVentana(new ModificarMaterialWindow(idMaterial));
        }

        private void Eliminar()
        {
            OcultarError();

            // VALIDAR SELECCIÓN
            if (!DataGridSeleccionado()) return;

            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Eliminar material?", "ELIMINACIÓN");

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ

            // GUARDAR ID DEL MATERIAL SELECCIONADO
            int idMaterial = ObtenerIdSeleccionado();

            try
            {
                // ELIMINAR INCIDENCIA
                _nMaterial.Eliminar(idMaterial);

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
        #endregion
    }
}
