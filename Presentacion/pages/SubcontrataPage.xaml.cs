using Datos;
using Negocio.services;
using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for SubcontrataPage.xaml
    /// </summary>
    public partial class SubcontrataPage : Page
    {
        private NUsuario _nUsuario = new NUsuario();
        private NSubcontrata _nSubcontrata = new NSubcontrata();
        #region Constructor
        public SubcontrataPage()
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

        private MessageBoxResult MostrarDecision(String mensaje, String titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }

        private bool ValidarCampos()
        {
            if (Validador.TextBoxVacio(txtNombre) || Validador.RichTextBoxVacio(txtDescripcion) || Validador.TextBoxVacio(txtRuc) || Validador.TextBoxVacio(txtCelular))
            {
                MostrarError("¡Complete los campos!");
                return false;
            }

            return true;
        }

        private void SetTextToRichTextBox(RichTextBox rtx, String msg)
        {
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(msg)));
            rtx.Document = doc;
        }

        private String TextoDeRichTextBox(RichTextBox rtx)
        {
            TextRange text = new TextRange(rtx.Document.ContentStart, rtx.Document.ContentEnd);
            return text.Text;
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

        #region Opciones de Subcontrata (Ver datos, Modificar subcontrata, Eliminar subcontrata)

        private void MostrarDatos()
        {
            try
            {
                Subcontrata subcontrata = _nSubcontrata.GetSubcontrata(Administrador.GetIdSubcontrata());
                tbNombre.Text = txtNombre.Text = subcontrata.nombre;
                SetTextToRichTextBox(txtDescripcion, subcontrata.descripcion);
                txtRuc.Text = subcontrata.ruc;
                txtCelular.Text = subcontrata.celular;

                Usuario usuarioCreador = _nUsuario.GetUsuario(subcontrata.created_by);
                DateTime fechaCreacion = subcontrata.created_at;
                tbCreadoPor.Text = $"Creado por {usuarioCreador.username} el {fechaCreacion.ToString("dd/MM/yyyy")} a las {fechaCreacion.ToString("HH:mm")}";

                Usuario usuarioEditor = _nUsuario.GetUsuario(subcontrata.updated_by);
                DateTime fechaEdicion = subcontrata.updated_at;
                tbEditadoPor.Text = $"Editado por última vez por {usuarioEditor.username} el {fechaEdicion.Date.ToString("dd/MM/yyyy")} a las {fechaEdicion.ToString("HH:mm")}";
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void Modificar()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Guardar cambios?", "MODIFICACIÓN");

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ

            // GUARDAR DESCRIPCION (RICHTEXTBOX)
            String descripcion = TextoDeRichTextBox(txtDescripcion);

            // CREAR OBJETO SUBCONTRATA
            Subcontrata subcontrata = new Subcontrata()
            {
                id = Administrador.GetIdSubcontrata(),
                nombre = txtNombre.Text.Trim(),
                descripcion = descripcion.Trim(),
                ruc = txtRuc.Text.Trim(),
                celular = txtCelular.Text.Trim(),
                updated_at = DateTime.Now,
                updated_by = Administrador.GetIdUsuario(),
            };

            try
            {
                // GUARDAR CAMBIOS
                _nSubcontrata.Modificar(subcontrata);

                // MOSTRAR DATOS
                MostrarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void Eliminar()
        {
            // CONFIRMAR
            MessageBoxResult result = MostrarDecision("¿Eliminar subcontrata?", "ELIMINACIÓN");

            // REGRESAR SI LA RESPUESTA ES NO
            if (result == MessageBoxResult.No) return;

            // CONTINUA SI LA RESPUESTA ES SÍ
            try
            {
                // ELIMINAR
                _nSubcontrata.Eliminar(Administrador.GetIdSubcontrata());

                // NAVEGAR
                Navegar(new MenuPage());
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

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }

        private void btnAsistencias_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new AsistenciasPage());
        }

        private void btnMateriales_Click(object sender, RoutedEventArgs e)
        {
            Navegar(new MaterialesPage());
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtRuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtCelular_TextChanged(object sender, TextChangedEventArgs e)
        {
            OcultarError();
        }

        #endregion
    }
}
