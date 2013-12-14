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
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class General : UserControl
    {
        public General()
        {
            InitializeComponent();
        }

        private void ButtonAddContactType(object sender, RoutedEventArgs e)
        {
            string name = ContactTypeTextbox.Text;
            ContactpersonType.AddContactpersonType(name);
        }

        private void ButtonRemoveContactType(object sender, RoutedEventArgs e)
        {
            ContactpersonType type = (ContactpersonType)ContactTypeListview.SelectedItem;
            if (type == null) return;
            int index = ContactpersonType.GetIndexByID(type.ID);
            ContactpersonType.DeleteContactpersonType(index);
        }
    }
}
