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

        private void ButtonAddTicket(object sender, RoutedEventArgs e)
        {
            string name = CustomerName.Text;
            string email = CustomerEmail.Text;
            TicketType type = (TicketType)CustomerTicketType.SelectedItem;
            int typeid = Convert.ToInt32(type.ID);
            int quantity = Convert.ToInt32(CustomerTicketQuantity.Text);

            int index = TicketType.GetIndexByID(type.ID);
            TicketType ticketType = TicketType.GetTicketTypes()[index];

            if (ticketType.TicketsLeft - quantity >= 0)
            {
                TicketType.UpdateTicketType(index, ticketType.Name, (float)ticketType.Price, ticketType.AvailableTickets, ticketType.TicketsLeft - quantity);
                Ticket.AddTicket(name, email, typeid, quantity);
            }
        }

        private void ButtonRemoveTicket(object sender, RoutedEventArgs e)
        {
            Ticket type = (Ticket)CustomersListView.SelectedItem;
            if (type == null) return;
            int index = Ticket.GetIndexByID(type.ID);
            Ticket.DeleteTicket(index);
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
