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
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class Contact : UserControl
    {
        public Contact()
        {
            InitializeComponent();
        }

        private void ButtonAddContact(object sender, RoutedEventArgs e)
        {
            string name = ContactName.Text;
            string company = ContactCompany.Text;
            ContactpersonType type = (ContactpersonType)ContactFunction.SelectedItem;
            int function = 0;
            if (type != null) function = type.ID;
            string street = ContactStreet.Text;
            string city = ContactCity.Text;
            string phone = ContactPhone.Text;
            string email = ContactEmail.Text;
            string extra = ContactInfo.Text;
            Contactperson.AddContactperson(name, company, function, street, city, phone, email, extra);
        }

        private void ButtonRemoveContact(object sender, RoutedEventArgs e)
        {
            Contactperson contact = (Contactperson)ContactListView.SelectedItem;
            if (contact == null) return;
            int index = Contactperson.GetIndexByID(contact.ID);
            Contactperson.DeleteContactperson(index);
        }
    }
}
