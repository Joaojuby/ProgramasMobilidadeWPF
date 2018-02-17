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

        private ProgramasMobilidadeEntities _context = new ProgramasMobilidadeEntities();

        public PaisesJanela()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Paises.Load();
            lbPaises.ItemsSource = _context.Paises.Local;
            lbPaises.SelectedValuePath = "ID";
            lbPaises.DisplayMemberPath = "Nome";
            lbPaises.SelectedIndex = 0;
            lbPaises.IsSynchronizedWithCurrentItem = true;
        }

        private void lbPaises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AtualizarEstado();
        }

        private void AtualizarEstado()
        {
            lblEstado.Content = string.Format("Tipo Programa {0} de {1}", lbPaises.Items.CurrentPosition + 1, lbPaises.Items.Count);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
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

                AtualizarEstado();
            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            EditarPaisJanela janela = new EditarPaisJanela(_context.Paises) { Title = "Novo País" };

            if(janela.ShowDialog() == true)
            {
                _context.Paises.Add(janela.Pais);
                _context.SaveChanges();

                lbPaises.Items.MoveCurrentToLast();

                AtualizarEstado();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
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
    }
}
