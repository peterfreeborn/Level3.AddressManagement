using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    public class OrderAddressSearchControlState
    {
        private List<string> _lstOrderSystemOfRecords;
        public List<string> OrderSystemOfRecords
        {
            get { return _lstOrderSystemOfRecords; }
            set { _lstOrderSystemOfRecords = value; }
        }


        private List<string> _lstMigrationStatuses;
        public List<string> MigrationStatuses
        {
            get { return _lstMigrationStatuses; }
            set { _lstMigrationStatuses = value; }
        }


        private List<string> _lstOrderAddressTypes;
        public List<string> OrderAddressTypes
        {
            get { return _lstOrderAddressTypes; }
            set { _lstOrderAddressTypes = value; }
        }


        private DateTime? _dteOrderDate_Start;
        public DateTime? OrderDate_Start
        {
            get { return _dteOrderDate_Start; }
            set { _dteOrderDate_Start = value; }
        }


        private DateTime? _dteOrderDate_End;
        public DateTime? OrderDate_End
        {
            get { return _dteOrderDate_End; }
            set { _dteOrderDate_End = value; }
        }


        private DateTime? _dteDateCreated_Start;
        public DateTime? DateCreated_Start
        {
            get { return _dteDateCreated_Start; }
            set { _dteDateCreated_Start = value; }
        }


        private DateTime? _dteDateCreated_End;
        public DateTime? DateCreated_End
        {
            get { return _dteDateCreated_End; }
            set { _dteDateCreated_End = value; }
        }


        private string _strCustomerOrderNumber;
        public string CustomerOrderNumber
        {
            get { return _strCustomerOrderNumber; }
            set { _strCustomerOrderNumber = value; }
        }


        private string _strAddressLine1;
        public string AddressLine1
        {
            get { return _strAddressLine1; }
            set { _strAddressLine1 = value; }
        }


        private string _strWhereClause;
        public string WhereClause
        {
            get { return _strWhereClause; }
            set { _strWhereClause = value; }
        }

        // Constructor
        public OrderAddressSearchControlState()
        {

        }

    }
}
