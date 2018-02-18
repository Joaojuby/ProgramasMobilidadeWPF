using System;
using System.Collections.Generic;
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
    /// Lógica interna para TiposProgramasJanela.xaml
    /// </summary>
    public partial class TiposProgramasJanela : Window
    {
        private ProgramasMobilidadeEntities _context = new ProgramasMobilidadeEntities();

        public TiposProgramasJanela()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.TiposProgramaMobilidade.Load();
            lbTiposProgramas.ItemsSource = _context.TiposProgramaMobilidade.Local;
            lbTiposProgramas.SelectedValuePath = "ID";
            lbTiposProgramas.DisplayMemberPath = "Designacao";
            lbTiposProgramas.SelectedIndex = 0;
            lbTiposProgramas.IsSynchronizedWithCurrentItem = true;
            AtualizarEstado();

        }

        private void AtualizarEstado()
        {
            lblEstado.Content = string.Format("Tipo Programa {0} de {1}", lbTiposProgramas.Items.CurrentPosition + 1, lbTiposProgramas.Items.Count);
        }

        private void lbTiposProgramas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AtualizarEstado();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            EditarTipoPrograma janela = new EditarTipoPrograma(_context.TiposProgramaMobilidade) { Title = "Novo Tipo Programa" };

            if(janela.ShowDialog() == true)
            {
                _context.TiposProgramaMobilidade.Add(janela.TipoPrograma);
                _context.SaveChanges();

                lbTiposProgramas.Items.MoveCurrentToLast();

                AtualizarEstado();
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lbTiposProgramas.SelectedItem == null)
            {
                return;
            }

            TiposProgramaMobilidade tipoPrograma = lbTiposProgramas.SelectedItem as TiposProgramaMobilidade;
            EditarTipoPrograma janela = new EditarTipoPrograma(_context.TiposProgramaMobilidade, new TiposProgramaMobilidade() { ID = tipoPrograma.ID, Designacao = tipoPrograma.Designacao, Descricao = tipoPrograma.Descricao, URLImagem = tipoPrograma.URLImagem }) { Title = "Editar Tipo Programa" };

            if (janela.ShowDialog() == true && janela.TipoPrograma != tipoPrograma)
            {
                tipoPrograma.Designacao = janela.TipoPrograma.Designacao;
                tipoPrograma.Descricao = janela.TipoPrograma.Descricao;
                tipoPrograma.URLImagem = janela.TipoPrograma.URLImagem;

                _context.Entry(tipoPrograma).State = EntityState.Modified;
                _context.SaveChanges();

                lbTiposProgramas.Items.Refresh();
                AtualizarEstado();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lbTiposProgramas.SelectedItem == null)
            {
                return;
            }

            TiposProgramaMobilidade tipoPrograma = lbTiposProgramas.SelectedItem as TiposProgramaMobilidade;
            var mensagem = "Tem a certeza que quer eliminar " + tipoPrograma.Designacao + "?";

            if (MessageBox.Show(mensagem, "Apagar País?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _context.TiposProgramaMobilidade.Remove(tipoPrograma);
                _context.SaveChanges();

                lbTiposProgramas.Items.MoveCurrentToFirst();

                AtualizarEstado();
            }
        }

        private void menuFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
    }
}
