using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    public class Ticket : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _TicketHolder;

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name has to be between 3 and 50 characters")]
        public string TicketHolder
        {
            get { return _TicketHolder; }
            set { _TicketHolder = value; }
        }

        private string _TicketHolderEmail;

        [Required(ErrorMessage = "The Email Address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        public string TicketHolderEmail
        {
            get { return _TicketHolderEmail; }
            set { _TicketHolderEmail = value; }
        }

        private int _TicketType;

        [Required(ErrorMessage = "The Ticket Type is required")]
        public int TicketType
        {
            get { return _TicketType; }
            set { _TicketType = value; }
        }

        private int _Amount;

        [Required(ErrorMessage = "The Amount of tickets is required")]
        [Range(1, int.MaxValue, ErrorMessage="Please enter a valid number")]
        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private static ObservableCollection<Ticket> _Tickets = null;

        public static ObservableCollection<Ticket> GetTickets()
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null)
            {
                try
                {
                    // Create _Tickets
                    _Tickets = new ObservableCollection<Ticket>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM ticket");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new Ticket
                        Ticket ticket = new Ticket();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) ticket.ID = -1;
                        else ticket.ID = Convert.ToInt32(record["ID"]);

                        // Get TicketHolder
                        if (DBNull.Value.Equals(record["TicketHolder"])) ticket.TicketHolder = "";
                        else ticket.TicketHolder = record["TicketHolder"].ToString();

                        // Get TicketHolderEmail
                        if (DBNull.Value.Equals(record["TicketHolderEmail"])) ticket.TicketHolderEmail = "";
                        else ticket.TicketHolderEmail = record["TicketHolderEmail"].ToString();

                        // Get TicketType
                        if (DBNull.Value.Equals(record["TicketType"])) ticket.TicketType = -1;
                        else ticket.TicketType = Convert.ToInt32(record["TicketType"].ToString());

                        // Get Amount
                        if (DBNull.Value.Equals(record["Amount"])) ticket.Amount = -1;
                        else ticket.Amount = Convert.ToInt32(record["Amount"].ToString());

                        // Add _Tickets
                        _Tickets.Add(ticket);
                    }
                    if (reader != null)
                        reader.Close();
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear Contactpersons
                    _Tickets.Clear();
                    _Tickets = null;
                }

            }

            // Return _Tickets
            return _Tickets;
        }

        public static int AddTicket(Ticket ticket)
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null) GetTickets();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@ticketholder", ticket.TicketHolder);
                DbParameter param2 = Database.AddParameter("@ticketholderemail", ticket.TicketHolderEmail);
                DbParameter param3 = Database.AddParameter("@tickettype", ticket.TicketType);
                DbParameter param4 = Database.AddParameter("@amount", ticket.Amount);
                DbDataReader reader = Database.GetData("INSERT INTO ticket(TicketHolder, TicketHolderEmail, TicketType, Amount) VALUES(@ticketholder, @ticketholderemail, @tickettype, @amount); SELECT LAST_INSERT_ID() AS ID;", param1, param2, param3, param4);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) ticket.ID = -1;
                    else ticket.ID = Convert.ToInt32(record["ID"]);
                }
                if (reader != null)
                    reader.Close();

                _Tickets.Add(ticket);
                return ticket.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void UpdateTicket(Ticket ticket)
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null) GetTickets();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", ticket.ID);
                DbParameter param2 = Database.AddParameter("@ticketholder", ticket.TicketHolder);
                DbParameter param3 = Database.AddParameter("@ticketholderemail", ticket.TicketHolderEmail);
                DbParameter param4 = Database.AddParameter("@tickettype", ticket.TicketType);
                DbParameter param5 = Database.AddParameter("@amount", ticket.Amount);
                int affectedRows = Database.ModifyData("UPDATE ticket SET TicketHolder = @ticketholder, TicketHolderEmail = @ticketholderemail, TicketType = @tickettype, Amount = @amount WHERE id = @id", param1, param2, param3, param4, param5);
                if (affectedRows == 0) return;

                // Update _Tickets
                _Tickets[GetIndexByID(ticket.ID)] = ticket;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteTicket(Ticket ticket)
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null) GetTickets();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", ticket.ID);
                int affectedRows = Database.ModifyData("DELETE FROM ticket WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _Tickets
                _Tickets.RemoveAt(GetIndexByID(ticket.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int GetIndexByID(int id)
        {
            for (int i = 0; i < _Tickets.Count; ++i)
            {
                if (_Tickets[i].ID == id) return i;
            }
            return -1;
        }

        public Ticket Copy()
        {
            return new Ticket() { ID = this.ID, TicketHolder = this.TicketHolder, TicketHolderEmail = this.TicketHolderEmail, TicketType = this.TicketType, Amount = this.Amount };
        }

        public void CreateWord(string path)
        {
            //Ticket afdrukken naar WORD
            File.Copy("C:\\NMCT\\2e Jaar\\Business Applications\\Project\\FestivalAdministration\\FestivalAdministration\\Files\\ticket-template.docx", path, true);

            WordprocessingDocument newdoc = WordprocessingDocument.Open(path, true);
            IDictionary<String, BookmarkStart> bookmarks = new Dictionary<String, BookmarkStart>();
            foreach (BookmarkStart bms in newdoc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarks[bms.Name] = bms;
            }

            long code = this.TicketType * this.Amount * 82146;
            while (code > 999999999999)
            {
                code -= 999999999999;
            }

            string codeString = code.ToString();
            while (codeString.Length < 12)
            {
                codeString = codeString.Insert(0, "0");
            }

            bookmarks["Name"].Parent.InsertAfter<Run>(new Run(new Text(this.TicketHolder)), bookmarks["Name"]);
            bookmarks["TicketType"].Parent.InsertAfter<Run>(new Run(new Text(FestivalAdministration.Model.TicketType.GetTypeByID(this.TicketType))), bookmarks["TicketType"]);
            bookmarks["Amount"].Parent.InsertAfter<Run>(new Run(new Text(this.Amount.ToString())), bookmarks["Amount"]);
            bookmarks["BarCode"].Parent.InsertAfter<Run>(new Run(new Text(codeString)), bookmarks["BarCode"]);
            bookmarks["Code"].Parent.InsertAfter<Run>(new Run(new Text(codeString)), bookmarks["Code"]);
            newdoc.Close();

            Console.WriteLine("Ticket Printed");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
