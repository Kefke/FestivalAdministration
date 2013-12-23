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

namespace FestivalAdministration.View
{
    /// <summary>
    /// Interaction logic for Band.xaml
    /// </summary>
    public partial class Band : Window
    {
        public Band()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
