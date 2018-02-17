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
        public Paises Pais { get; set; }
        private IEnumerable<Paises> listaPaises;
        private bool canClose;

        public EditarPaisJanela(IEnumerable<Paises> paises, Paises pais=null)
        {
            InitializeComponent();
            listaPaises = paises;

            Pais = pais ?? new Paises();

            DataContext = Pais;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            canClose = true;
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            canClose = true;
            if(!ActualizarPais())
            {
                canClose = false;
            }

            DialogResult = true;
        }

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
