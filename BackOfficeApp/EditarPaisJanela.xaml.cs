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
    /// Lógica interna para EditarPaisJanela.xaml
    /// </summary>
    public partial class EditarPaisJanela : Window
    {
        /// <summary>
        /// Gets ou sets a propriedade País.
        /// </summary>
        public Paises Pais { get; set; }

        /// <summary>
        /// lista de paises utilizada pela listbox
        /// </summary>
        private IEnumerable<Paises> listaPaises;
        
        /// <summary>
        /// Flag de controlo para fechar a janela
        /// </summary>
        private bool canClose;

        /// <summary>
        /// Inicializa uma nova instancia de <see cref="BackOfficeApp.EditarPaisJanela" />. 
        /// </summary>
        /// <param name="paises">Required. </param>
        /// <param name="pais">Optional. O Valor por defeito é null.</param>
        public EditarPaisJanela(IEnumerable<Paises> paises, Paises pais = null)
        {
            InitializeComponent();
            listaPaises = paises;

            Pais = pais ?? new Paises();

            DataContext = Pais;
        }

        /// <summary>
        /// Cancela o evento de fecho se a aplicação não estiver no estado adequado.
        /// </summary>
        /// <param name="sender">A fonte do evento</param>
        /// <param name="e">Um <see cref="System.ComponentModel.CancelEventArgs" /> que contém a informação do evento.</param>
        /// <remarks></remarks>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }

        /// <summary>
        /// Cancela a edição ou criação do país.
        /// </summary>
        /// <param name="sender">A fonte do evento</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        /// <remarks></remarks>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            canClose = true;
        }

        /// <summary>
        /// Valida e fecha a janela com resultado positivo.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        /// <remarks>Verifica que os dados do país estão corretos</remarks>
        private void BtnGravar_Click(object sender, RoutedEventArgs e)
        {
            canClose = true;
            if (!ActualizarPais())
            {
                canClose = false;
            }

            DialogResult = true;
        }

        /// <summary>
        /// Verifica se os dados são válidos
        /// </summary>
        /// <returns>Devolve true caso os dados sejam válidos ou false caso contrário</returns>
        /// <remarks>Não pode existir outro país com o mesmo nome, código ISO ou código de país</remarks>
        private bool ActualizarPais()
        {

            if (listaPaises.Any(pais => pais.Nome == Pais.Nome && pais.ID != Pais.ID))
            {
                MessageBox.Show("Já existe um país com este nome", "Nome Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listaPaises.Any(pais => pais.CodigoISO == Pais.CodigoISO && pais.ID != Pais.ID))
            {
                MessageBox.Show("Já existe um país com este código ISO", "Código ISO Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listaPaises.Any(pais => pais.CodigoPais == Pais.CodigoPais && pais.ID != Pais.ID))
            {
                MessageBox.Show("Já existe um país com este código", "Código País Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
