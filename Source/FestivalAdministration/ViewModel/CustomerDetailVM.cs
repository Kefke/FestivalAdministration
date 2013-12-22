using FestivalAdministration.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestivalAdministration.ViewModel
{
    class CustomerDetailVM: ObservableObject, IPage
    {
        public CustomerDetailVM()
        {
            TicketTypes = TicketType.GetTicketTypes();
            Tickets = Ticket.GetTickets();

            SelectedCustomer = new Ticket();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        //************************
        // Parameters

        public string Name
        {
            get { return "Customer Detail"; }
        }

        private ObservableCollection<TicketType> _ticketTypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get { return _ticketTypes; }
            set { _ticketTypes = value; OnPropertyChanged("TicketTypes"); }
        }

        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set { _tickets = value; OnPropertyChanged("Tickets"); }
        }

        private bool _changeNotify;
        private Ticket _oldCustomer;
        private Ticket _selectedCustomer;

        public Ticket SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
                if (!_changeNotify)
                {
                    _changeNotify = !_changeNotify;
                    return;
                }
                SelectionChanged(this);
            }
        }

        private string _showedit;

        public string ShowEdit
        {
            get { return _showedit; }
            set { _showedit = value; OnPropertyChanged("ShowEdit"); }
        }

        private string _ShowCancel;

        public string ShowCancel
        {
            get { return _ShowCancel; }
            set { _ShowCancel = value; OnPropertyChanged("ShowCancel"); }
        }

        private string _ShowSave;

        public string ShowSave
        {
            get { return _ShowSave; }
            set { _ShowSave = value; OnPropertyChanged("ShowSave"); }
        }

        private bool _Enabled;

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; OnPropertyChanged("Enabled"); }
        }
        //************************

        //************************
        // Commands

        public ICommand AddCustomerCommand
        {
            get { return new RelayCommand<CustomerDetailVM>(AddCustomer); }
        }

        private void AddCustomer(CustomerDetailVM customervm)
        {
            SelectedCustomer = new Ticket();
            _oldCustomer = null;
            customervm.ShowEdit = "Hidden";
            customervm.ShowCancel = "Visible";
            customervm.ShowSave = "Visible";
            customervm.Enabled = true;
        }

        public ICommand EditCustomerCommand
        {
            get { return new RelayCommand<CustomerDetailVM>(EditCustomer); }
        }

        private void EditCustomer(CustomerDetailVM customervm)
        {
            customervm.ShowEdit = "Hidden";
            customervm.ShowCancel = "Visible";
            customervm.ShowSave = "Visible";
            customervm.Enabled = true;
        }

        public ICommand DeleteCustomerCommand
        {
            get { return new RelayCommand<CustomerDetailVM>(DeleteCustomer); }
        }

        private void DeleteCustomer(CustomerDetailVM customervm)
        {
            if (SelectedCustomer == null) return;

            // Add the removed tickets from the deleted order again
            int ticketIndex = TicketType.GetIndexByID(_oldCustomer.TicketType);
            TicketType updatedTicketType = TicketTypes[ticketIndex].Copy();
            updatedTicketType.TicketsLeft = TicketTypes[ticketIndex].TicketsLeft + SelectedCustomer.Amount;
            TicketType.UpdateTicketType(updatedTicketType);

            // Remove the order
            Ticket.DeleteTicket(SelectedCustomer);

            SelectedCustomer = new Ticket();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateCustomerCommand
        {
            get { return new RelayCommand<CustomerDetailVM>(SaveUpdateCustomer); }
        }

        private void SaveUpdateCustomer(CustomerDetailVM customervm)
        {
            // Save Changes
            if (_oldCustomer == null)
            {
                // Check if there are enough tickets left
                int ticketIndex = TicketType.GetIndexByID(SelectedCustomer.TicketType);
                if (TicketTypes[ticketIndex].TicketsLeft >= SelectedCustomer.Amount)
                {
                    // Reduce tickets left by the ordered amount
                    TicketType updatedTicketType = TicketTypes[ticketIndex].Copy();
                    updatedTicketType.TicketsLeft = TicketTypes[ticketIndex].TicketsLeft - SelectedCustomer.Amount;
                    TicketType.UpdateTicketType(updatedTicketType);

                    // Insert new order into ticket db
                    SelectedCustomer.ID = Ticket.AddTicket(SelectedCustomer);
                }
            }
            else
            {
                // Check if there is a difference in the ordered tickets or types
                if (_oldCustomer.Amount == SelectedCustomer.Amount && _oldCustomer.TicketType == SelectedCustomer.TicketType)
                {
                    // Nothing special to do, just update the ticket
                    Ticket.UpdateTicket(SelectedCustomer);
                }
                else
                {
                    // Check if there are enough tickets left
                    int newticketIndex = TicketType.GetIndexByID(SelectedCustomer.TicketType);
                    if (TicketTypes[newticketIndex].TicketsLeft >= SelectedCustomer.Amount)
                    {
                        // Reduce tickets left by the ordered amount
                        TicketType updatedTicketType = TicketTypes[newticketIndex].Copy();
                        updatedTicketType.TicketsLeft = TicketTypes[newticketIndex].TicketsLeft - SelectedCustomer.Amount;
                        TicketType.UpdateTicketType(updatedTicketType);

                        // Add the removed tickets from the changed order again
                        int oldticketIndex = TicketType.GetIndexByID(_oldCustomer.TicketType);
                        updatedTicketType = TicketTypes[oldticketIndex].Copy();
                        updatedTicketType.TicketsLeft = TicketTypes[oldticketIndex].TicketsLeft + _oldCustomer.Amount;
                        TicketType.UpdateTicketType(updatedTicketType);

                        // Insert update the order in ticket db
                        Ticket.UpdateTicket(SelectedCustomer);
                    }
                }
            }

            // Update GUI
            customervm.ShowEdit = "Visible";
            customervm.ShowCancel = "Hidden";
            customervm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateCustomerCommand
        {
            get { return new RelayCommand<CustomerDetailVM>(CancelUpdateCustomer); }
        }

        private void CancelUpdateCustomer(CustomerDetailVM customervm)
        {
            // Reset person
            _changeNotify = false;
            SelectedCustomer = _oldCustomer;

            customervm.ShowEdit = "Visible";
            customervm.ShowCancel = "Hidden";
            customervm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<CustomerDetailVM>(SelectionChanged); }
        }

        private void SelectionChanged(CustomerDetailVM customervm)
        {
            if (SelectedCustomer == null) return;
            if (ShowCancel == "Visible") CancelUpdateCustomer(this);

            _oldCustomer = SelectedCustomer.Copy();

            customervm.ShowEdit = "Visible";
            customervm.ShowCancel = "Hidden";
            customervm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************
    }
}
