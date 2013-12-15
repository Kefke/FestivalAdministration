using FestivalAdministration.Model;
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
    /// Interaction logic for Tickets.xaml
    /// </summary>
    public partial class Tickets : UserControl
    {
        public Tickets()
        {
            InitializeComponent();
        }

        private void ButtonAddTicketType(object sender, RoutedEventArgs e)
        {
            string name = TicketName.Text;
            double price = Convert.ToDouble(TicketPrice.Text);
            int quantity = Convert.ToInt32(TicketQuantity.Text);
            TicketType.AddTicketType(name, (float)price, quantity);
        }

        private void ButtonRemoveTicketType(object sender, RoutedEventArgs e)
        {
            TicketType type = (TicketType)TicketsListView.SelectedItem;
            if (type == null) return;
            int index = TicketType.GetIndexByID(type.ID);
            TicketType.DeleteTicketType(index);
        }
    }
}
