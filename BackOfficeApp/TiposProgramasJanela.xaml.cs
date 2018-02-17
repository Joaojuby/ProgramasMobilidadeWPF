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
    }
}
