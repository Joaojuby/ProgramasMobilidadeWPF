using System;
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
        /// <summary>
        /// Inicializa uma nova instancia de <see cref="BackOfficeApp.MainWindow" />. 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Termina o processo da aplicação
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void SairApp(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Abre a janela dos países e esconde esta janela até fecharmos a anterior
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        /// <remarks></remarks>
        private void BtnPaises_Click(object sender, RoutedEventArgs e)
        {
            PaisesJanela paisesJanela = new PaisesJanela();
            Hide();
            if (paisesJanela.ShowDialog() == true)
            {
                Show();
            }
        }

        /// <summary>
        /// Abre a janela dos tipos de programas e esconde esta janela até fecharmos a anterior
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        /// <remarks></remarks>
        private void BtnTiposProgramas_Click(object sender, RoutedEventArgs e)
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
