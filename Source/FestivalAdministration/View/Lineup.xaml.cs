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

namespace FestivalAdministration.View
{
    /// <summary>
    /// Interaction logic for Lineup.xaml
    /// </summary>
    public partial class Lineup : UserControl
    {
        public Lineup()
        {
            InitializeComponent();
        }

        private void Button_Band_Add_Click(object sender, RoutedEventArgs e)
        {
            View.Band bandWindow = new View.Band();
            bandWindow.Show();
        }
    }
}
