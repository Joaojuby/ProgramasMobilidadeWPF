﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackOfficeApp
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SairApp(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPaises_Click(object sender, RoutedEventArgs e)
        {
            PaisesJanela paisesJanela = new PaisesJanela();
            Hide();
            if (paisesJanela.ShowDialog() == true)
            {
                Show();
            }
        }

        private void btnTiposProgramas_Click(object sender, RoutedEventArgs e)
        {
            TiposProgramasJanela tiposJanela = new TiposProgramasJanela();
            Hide();
            if (tiposJanela.ShowDialog() == true)
            {
                Show();
            }
        }
    }
}
