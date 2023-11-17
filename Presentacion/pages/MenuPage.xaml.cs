﻿using Datos;
using Negocio.services;
using Presentacion.helpers;
using Presentacion.modals;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion.pages
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private NUsuario _nUsuario = new NUsuario();
        private NSubcontrata _nSubcontrata = new NSubcontrata();

        #region Constructor
        public MenuPage()
        {
            InitializeComponent();
            MostrarDatosUsuario();
            MostrarSubcontratas();
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
            window.ShowDialog();
        }
        #endregion

        #region Usuario
        private void MostrarDatosUsuario()
        {
            try
            {
                Usuario usuario = _nUsuario.GetUsuario(Administrador.GetIdUsuario());
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
                List<Subcontrata> subcontratas = _nSubcontrata.GetSubcontratas();
                icSubcontratas.ItemsSource = subcontratas;
            }
            catch (Exception)
            {

            }
        }

        private void VerSubcontrata(object sender)
        {
            Button btnVerSubcontrata = (Button)sender;
            int idSeleccionado = int.Parse(btnVerSubcontrata.Tag.ToString());
            MessageBox.Show($"El id es: {idSeleccionado}");
            // NAVEGAR
            //Navegar(new SubcontrataPage());
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
        #endregion

        private void btnVerSubcontrata_Click(object sender, RoutedEventArgs e)
        {
            VerSubcontrata(sender);
        }
    }
}
