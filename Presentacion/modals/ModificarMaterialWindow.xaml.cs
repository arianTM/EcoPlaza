﻿using Datos;
using Negocio.services;
using Presentacion.helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.modals
{
    /// <summary>
    /// Interaction logic for ModificarMaterialWindow.xaml
    /// </summary>
    public partial class ModificarMaterialWindow : Window
    {
        private NMaterial _nMaterial = new NMaterial();
        private int _idMaterial = 0;
        #region Constructor
        public ModificarMaterialWindow(int idMaterial)
        {
            InitializeComponent();
            _idMaterial = idMaterial;
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

        private void SetTextoComboBox(ComboBox cb, String txt)
        {
            foreach (object item in cb.Items)
            {
                if (item is ComboBoxItem cbItem && String.Equals(txt, cbItem.Content.ToString()))
                {
                    cb.SelectedItem = cbItem;
                    break;
                }
            }
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

        #region Opciones del formulario (Mostrar datos, Modificar y Limpiar)

        private void Mostrar()
        {
            try
            {
                Material materialSeleccionado = _nMaterial.GetMaterial(_idMaterial);
                txtNombre.Text = materialSeleccionado.nombre;
                SetTextoComboBox(cbMarca, materialSeleccionado.marca);
                txtCantidad.Text = materialSeleccionado.cantidad.ToString();
                txtCosto.Text = materialSeleccionado.costo.ToString();
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

            // CERRAR FORMULARIO
            Close();
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
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OcultarError();
            Mostrar();
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
