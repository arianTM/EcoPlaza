﻿using Datos;
using Negocio;
using Presentacion.helpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for AgregarMaterialWindow.xaml
    /// </summary>
    public partial class AgregarMaterialWindow : Window
    {

        private readonly Controlador _controlador = new Controlador();
        #region Constructor
        public AgregarMaterialWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
        private bool ValidarCampos()
        {
            if (Validador.TextBoxVacio(txtNombre) || Validador.ComboBoxSinSeleccion(cbMarca) || Validador.TextBoxVacio(txtCantidad) || Validador.TextBoxVacio(txtCosto))
            {
                MostrarError("¡Complete los campos!");
                return false;
            }

            if (Validador.TextBoxSinFormatoIntPositivo(txtCantidad))
            {
                MostrarError("¡El campo 'Cantidad! debe ser entero positivo!");
                return false;
            }

            if (Validador.TextBoxSinFormatoDecimalPositivo(txtCosto))
            {
                MostrarError("¡El campo 'Costo! debe ser decimal positivo!");
                return false;
            }

            return true;
        }

        private void LlenarComboBox()
        {
            List<String> marcas = _controlador.GetMarcas();
            marcas.ForEach(m =>
            {
                ComboBoxItem marca = new ComboBoxItem();
                marca.Content = m;
                cbMarca.Items.Add(marca);
            });
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

        #region Opciones del formulario (Agregar y Limpiar)
        private void Registrar()
        {
            // VALIDAR CAMPOS
            if (!ValidarCampos()) return;

            // CREAR OBJETO MATERIAL
            Material material = new Material()
            {
                nombre = txtNombre.Text,
                marca = cbMarca.Text,
                cantidad = int.Parse(txtCantidad.Text),
                costo = decimal.Parse(txtCosto.Text),
                subcontrata_id = Administrador.GetIdSubcontrata(),
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                created_by = Administrador.GetIdUsuario(),
                updated_by = Administrador.GetIdUsuario(),
            };

            try
            {
                // REGISTRAR
                _controlador.RegistrarMaterial(material);

                // CERRAR FORMULARIO
                Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            cbMarca.SelectedIndex = -1;
            txtCantidad.Clear();
            txtCosto.Clear();
        }
        #endregion

        #region Eventos de Controles

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
            LlenarComboBox();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Registrar();
        }

        private void txtNombre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void cbMarca_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtCantidad_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void txtCosto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OcultarError();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        #endregion
    }
}
