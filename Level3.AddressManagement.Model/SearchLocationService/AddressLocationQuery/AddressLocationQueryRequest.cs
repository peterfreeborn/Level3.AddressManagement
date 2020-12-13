using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model.SearchLocationService.AddressLocationQuery
{
  
    public class AddressLocationQueryRequest
    {
        public bool CreateSiteIfNotFound { get; set; }
        public bool ExactMatchSearch { get; set; }
        public bool ReturnAllAddresses { get; set; }
        public bool ReturnEntitiesWithCodes { get; set; }
        public bool ReturnNetworkEntities { get; set; }
        public bool ReturnServiceLocations { get; set; }
        public bool ReturnSiteswithCodes { get; set; }
        public Searchaddressline2values[] SearchAddressLine2Values { get; set; }
        public bool ShowDuplicateEntities { get; set; }
        public string AddressLine1 { get; set; }
        public string BuildingName { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
        public string StateCode { get; set; }
        public string SubCity { get; set; }
    }

    public class Searchaddressline2values
    {
        public string DesignatorType { get; set; }
        public string DesignatorValue { get; set; }
    }

}
