using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{

    public class GetAddressResponse
    {
        public ResponseItem[] Property1 { get; set; }
    }

    public class ResponseItem
    {
        public string ErrorMessage { get; set; }
        public string[] Address { get; set; }
        public string AddressLine1Combined { get; set; }
        public string BuildingExtension { get; set; }
        public string BuildingInfo { get; set; }
        public string[] BuildingProgram { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string DemarcType { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string[] Network { get; set; }
        public string OnNet { get; set; }
        public string PostalCode { get; set; }
        public string PreferredSiteCode { get; set; }
        public string ReleaseToLOACFA { get; set; }
        public string[] SiteCodes { get; set; }
        public string SiteId { get; set; }
        public string SiteStatus { get; set; }
        public string SiteType { get; set; }
        public string State { get; set; }
    }

}
