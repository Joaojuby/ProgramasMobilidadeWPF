using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Lógica interna para PaisesJanela.xaml
    /// </summary>
    public partial class PaisesJanela : Window
    {

        /// <summary>
        /// O contexto da base de dados
        /// </summary>
        private ProgramasMobilidadeEntities _context = new ProgramasMobilidadeEntities();

        /// <summary>
        /// Inicializa uma nova instancia de <see cref="BackOfficeApp.PaisesJanela" />. 
        /// </summary>
        /// <remarks></remarks>
        public PaisesJanela()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa a listbox e atualiza a barra de estado
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Paises.Load();
            lbPaises.ItemsSource = _context.Paises.Local;
            lbPaises.SelectedValuePath = "ID";
            lbPaises.DisplayMemberPath = "Nome";
            lbPaises.SelectedIndex = 0;
            lbPaises.IsSynchronizedWithCurrentItem = true;

            AtualizarEstado();
        }

        /// <summary>
        /// Atualiza a barra de estado ao selecionar um item na listbox
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void LbPaises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AtualizarEstado();
        }

        /// <summary>
        /// Método para atualizar a barra de estado
        /// </summary>
        private void AtualizarEstado()
        {
            lblEstado.Content = string.Format("Tipo Programa {0} de {1}", lbPaises.Items.CurrentPosition + 1, lbPaises.Items.Count);
        }

        /// <summary>
        /// Abre uma nova janela para editar o país selecionado.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lbPaises.SelectedItem == null)
            {
                return;
            }

            Paises pais = lbPaises.SelectedItem as Paises;
            EditarPaisJanela janela = new EditarPaisJanela(_context.Paises, new Paises() { ID = pais.ID, CodigoISO = pais.CodigoISO, CodigoPais = pais.CodigoPais, Nome = pais.Nome, URLBandeira = pais.URLBandeira }) { Title = "Editar País" };

            if (janela.ShowDialog() == true && janela.Pais != pais)
            {
                pais.CodigoPais = janela.Pais.CodigoPais;
                pais.CodigoISO = janela.Pais.CodigoISO;
                pais.Nome = janela.Pais.Nome;
                pais.URLBandeira = janela.Pais.URLBandeira;

                _context.Entry(pais).State = EntityState.Modified;
                _context.SaveChanges();

                lbPaises.Items.Refresh();

                AtualizarEstado();
            }
        }

        /// <summary>
        /// Abre uma nova janela para criar um novo país
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void BtnNovo_Click(object sender, RoutedEventArgs e)
        {
            EditarPaisJanela janela = new EditarPaisJanela(_context.Paises) { Title = "Novo País" };

            if (janela.ShowDialog() == true)
            {
                _context.Paises.Add(janela.Pais);
                _context.SaveChanges();

                lbPaises.Items.MoveCurrentToLast();

                AtualizarEstado();
            }
        }

        /// <summary>
        /// Elimina o país selecionado após confirmação.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lbPaises.SelectedItem == null)
            {
                return;
            }

            Paises pais = lbPaises.SelectedItem as Paises;
            var mensagem = "Tem a certeza que quer eliminar " + pais.Nome + "?";

            if (MessageBox.Show(mensagem, "Apagar País?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _context.Paises.Remove(pais);
                _context.SaveChanges();

                lbPaises.Items.MoveCurrentToFirst();

                AtualizarEstado();
            }
        }

        /// <summary>
        /// Fecha a janela atual
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void MenuFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Altera o resultado da janela para true.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Um <see cref="System.Windows.RoutedEventArgs" /> que contém a informação do evento.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
    }
}
