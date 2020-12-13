using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model.SearchLocationService
{

    public class AdvancedLocationQueryV2Response
    {
        public SearchResultV2[] ArrayOfSearchResultV2 { get; set; }
    }

    public class SearchResultV2
    {
        public string ErrorMessage { get; set; }
        public string AddressLine2BuildingName { get; set; }
        public Addressline2entitiesinfo[] AddressLine2EntitiesInfo { get; set; }
        public Address[] Addresses { get; set; }
        public string Building { get; set; }
        public string CLLIId { get; set; }
        public string CoordinateLocation { get; set; }
        public string Directions { get; set; }
        public bool HasWorkItems { get; set; }
        public bool IsInGLMS { get; set; }
        public bool IsNewRecord { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MasterSiteId { get; set; }
        public string MatchScore { get; set; }
        public string PrecisionCode { get; set; }
        public string PrecisionName { get; set; }
        public string PricingAreaName { get; set; }
        public Primaryaddress PrimaryAddress { get; set; }
        public Ratecenter RateCenter { get; set; }
        public string RedirectedToID { get; set; }
        public string Region { get; set; }
        public bool RequestedIdRedirected { get; set; }
        public bool SiteIsOnNet { get; set; }
        public string SiteStatus { get; set; }
        public string SiteStatusType { get; set; }
        public string SiteType { get; set; }
        public string SiteTypeId { get; set; }
        public string WireCenterCLLI { get; set; }
    }

    public class Primaryaddress
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool HasWorkItems { get; set; }
        public string MasterSiteId { get; set; }
        public string SiteStatusType { get; set; }
        public string SiteStatusTypeId { get; set; }
        public string SiteType { get; set; }
        public string SiteTypeId { get; set; }
        public string AddressBlock1 { get; set; }
        public string AddressBlock2 { get; set; }
        public string AddressBlock3 { get; set; }
        public string AddressBlock4 { get; set; }
        public string AddressBlock5 { get; set; }
        public string AddressBlock6 { get; set; }
        public string AddressBlock7 { get; set; }
        public string AddressBlock8 { get; set; }
        public string AddressBlock9 { get; set; }
        public string AddressFormatType { get; set; }
        public int AddressId { get; set; }
        public string AddressLine1Combined { get; set; }
        public string AddressLine1and2Combined { get; set; }
        public string AddressLine2BuildingInfo { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeId { get; set; }
        public Building Building { get; set; }
        public string City { get; set; }
        public string CityInt { get; set; }
        public string CombinedAddressLine { get; set; }
        public bool CoordinateLocation { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeId { get; set; }
        public string CountryCodeId2 { get; set; }
        public string CountryCodeId3 { get; set; }
        public string CountryCodeName { get; set; }
        public string County { get; set; }
        public string Directions { get; set; }
        public string GeoPoliticalCountryInfo { get; set; }
        public string GeoPoliticalStateInfo { get; set; }
        public string GeopoliticalInfo { get; set; }
        public string Neighborhood { get; set; }
        public string ParentState { get; set; }
        public string ParentStateId { get; set; }
        public string PostalCode { get; set; }
        public int ResultMatch { get; set; }
        public string StateCode { get; set; }
        public string StateId { get; set; }
        public string StateLabelType { get; set; }
        public string StateLabelTypeId { get; set; }
        public string StateName { get; set; }
        public string StateNameInt { get; set; }
        public string StateType { get; set; }
        public string StateTypeId { get; set; }
        public string StreetDirectionPrefixType { get; set; }
        public string StreetDirectionPrefixTypeId { get; set; }
        public string StreetDirectionSuffixType { get; set; }
        public string StreetDirectionSuffixTypeId { get; set; }
        public string StreetName { get; set; }
        public string StreetNameInt { get; set; }
        public string StreetNameSuffixDescription { get; set; }
        public string StreetNameSuffixType { get; set; }
        public string StreetNameSuffixTypeId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetNumberFraction { get; set; }
        public string StreetNumberPrefix { get; set; }
        public string SubBuildingNumber { get; set; }
        public string SubBuildingType { get; set; }
        public string SubCity { get; set; }
        public string USZip4 { get; set; }
    }

    public class Building
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int NumberOfBasementFloors { get; set; }
        public int NumberOfFloors { get; set; }
        public Sitebuildingfloor[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int FloorDistance { get; set; }
        public bool HasNoDistributionRights { get; set; }
        public bool IsCapitalNeeded { get; set; }
        public bool IsFloorServed { get; set; }
        public bool IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int SiteBuildingId { get; set; }
        public int SiteBuildingFloorId { get; set; }
    }

    public class Ratecenter
    {
        public int RateCenterId { get; set; }
        public string RateCenterLATA { get; set; }
        public string RateCenterLATAName { get; set; }
        public string RateCenterName { get; set; }
    }

    public class Addressline2entitiesinfo
    {
        public string CLLIId { get; set; }
        public string CLLIIdSuffix { get; set; }
        public string CombinedDesignators { get; set; }
        public string Description { get; set; }
        public Entitydesignator[] EntityDesignators { get; set; }
        public bool IsDefault { get; set; }
        public string MasterNetworkEntityId { get; set; }
        public string MasterServiceLocationId { get; set; }
    }

    public class Entitydesignator
    {
        public string DesignatorType { get; set; }
        public string DesignatorValue { get; set; }
    }

    public class Address
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool HasWorkItems { get; set; }
        public string MasterSiteId { get; set; }
        public string SiteStatusType { get; set; }
        public string SiteStatusTypeId { get; set; }
        public string SiteType { get; set; }
        public string SiteTypeId { get; set; }
        public string AddressBlock1 { get; set; }
        public string AddressBlock2 { get; set; }
        public string AddressBlock3 { get; set; }
        public string AddressBlock4 { get; set; }
        public string AddressBlock5 { get; set; }
        public string AddressBlock6 { get; set; }
        public string AddressBlock7 { get; set; }
        public string AddressBlock8 { get; set; }
        public string AddressBlock9 { get; set; }
        public string AddressFormatType { get; set; }
        public int AddressId { get; set; }
        public string AddressLine1Combined { get; set; }
        public string AddressLine1and2Combined { get; set; }
        public string AddressLine2BuildingInfo { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeId { get; set; }
        public Building1 Building { get; set; }
        public string City { get; set; }
        public string CityInt { get; set; }
        public string CombinedAddressLine { get; set; }
        public bool CoordinateLocation { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeId { get; set; }
        public string CountryCodeId2 { get; set; }
        public string CountryCodeId3 { get; set; }
        public string CountryCodeName { get; set; }
        public string County { get; set; }
        public string Directions { get; set; }
        public string GeoPoliticalCountryInfo { get; set; }
        public string GeoPoliticalStateInfo { get; set; }
        public string GeopoliticalInfo { get; set; }
        public string Neighborhood { get; set; }
        public string ParentState { get; set; }
        public string ParentStateId { get; set; }
        public string PostalCode { get; set; }
        public int ResultMatch { get; set; }
        public string StateCode { get; set; }
        public string StateId { get; set; }
        public string StateLabelType { get; set; }
        public string StateLabelTypeId { get; set; }
        public string StateName { get; set; }
        public string StateNameInt { get; set; }
        public string StateType { get; set; }
        public string StateTypeId { get; set; }
        public string StreetDirectionPrefixType { get; set; }
        public string StreetDirectionPrefixTypeId { get; set; }
        public string StreetDirectionSuffixType { get; set; }
        public string StreetDirectionSuffixTypeId { get; set; }
        public string StreetName { get; set; }
        public string StreetNameInt { get; set; }
        public string StreetNameSuffixDescription { get; set; }
        public string StreetNameSuffixType { get; set; }
        public string StreetNameSuffixTypeId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetNumberFraction { get; set; }
        public string StreetNumberPrefix { get; set; }
        public string SubBuildingNumber { get; set; }
        public string SubBuildingType { get; set; }
        public string SubCity { get; set; }
        public string USZip4 { get; set; }
    }

    public class Building1
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int NumberOfBasementFloors { get; set; }
        public int NumberOfFloors { get; set; }
        public Sitebuildingfloor1[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor1
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int FloorDistance { get; set; }
        public bool HasNoDistributionRights { get; set; }
        public bool IsCapitalNeeded { get; set; }
        public bool IsFloorServed { get; set; }
        public bool IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int SiteBuildingId { get; set; }
        public int SiteBuildingFloorId { get; set; }
    }

}
