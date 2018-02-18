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
using System.Windows.Shapes;

namespace BackOfficeApp
{
    /// <summary>
    /// Lógica interna para EditarTipoPrograma.xaml
    /// </summary>
    public partial class EditarTipoPrograma : Window
    {
        /// <summary>
        /// Gets ou sets a propriedade TipoPrograma.
        /// </summary>
        public TiposProgramaMobilidade TipoPrograma { get; set; }

        /// <summary>
        /// lista de tipos de programa utilizada pela listbox
        /// </summary>
        private IEnumerable<TiposProgramaMobilidade> listaTipos;

        /// <summary>
        /// Flag de controlo para fechar a janela
        /// </summary>
        private bool canClose;

        /// <summary>
        /// Inicializa uma nova instancia de <see cref="BackOfficeApp.EditarTipoPrograma" /> class. 
        /// </summary>
        /// <param name="tipos">Required. </param>
        /// <param name="tipoPrograma">Optional. The default value is null.</param>
        public EditarTipoPrograma(IEnumerable<TiposProgramaMobilidade> tipos, TiposProgramaMobilidade tipoPrograma = null)
        {
            InitializeComponent();
            listaTipos = tipos;

            TipoPrograma = tipoPrograma ?? new TiposProgramaMobilidade();

            DataContext = TipoPrograma;
        }

        /// <summary>
        /// Cancela o evento de fecho se a aplicação não estiver no estado adequado.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.ComponentModel.CancelEventArgs" /> que contém a informação do evento.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }

        /// <summary>
        /// Fecha a janela com resultado positivo.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        /// <remarks></remarks>
        private void BtnGravar_Click(object sender, RoutedEventArgs e)
        {
            canClose = true;

            DialogResult = true;
        }

        /// <summary>
        /// Cancela a edição ou criação do tipo de programa.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        /// <remarks></remarks>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            canClose = true;
        }
    }
}
