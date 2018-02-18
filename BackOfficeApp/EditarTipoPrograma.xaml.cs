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
    /// Interaction logic for EditarTipoPrograma.xaml
    /// </summary>
    public partial class EditarTipoPrograma : Window
    {
        public TiposProgramaMobilidade TipoPrograma { get; set; }
        private IEnumerable<TiposProgramaMobilidade> listaTipos;
        private bool canClose;

        public EditarTipoPrograma(IEnumerable<TiposProgramaMobilidade> tipos, TiposProgramaMobilidade tipoPrograma=null)
        {
            InitializeComponent();
            listaTipos = tipos;

            TipoPrograma = tipoPrograma ?? new TiposProgramaMobilidade();

            DataContext = TipoPrograma;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            canClose = true;

            DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            canClose = true;
        }
    }
}
