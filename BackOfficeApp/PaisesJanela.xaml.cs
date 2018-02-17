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
    }
}
