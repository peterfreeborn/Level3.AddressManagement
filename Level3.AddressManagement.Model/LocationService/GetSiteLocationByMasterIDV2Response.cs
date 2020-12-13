using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model.LocationService
{


    public class GetSiteLocationByMasterIDV2Response
    {
        public SiteLocationV2[] ArrayOfSiteLocationV2 { get; set; }
    }

    public class SiteLocationV2
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool? HasWorkItems { get; set; }
        public string MasterSiteId { get; set; }
        public string SiteStatusType { get; set; }
        public string SiteStatusTypeId { get; set; }
        public string SiteType { get; set; }
        public string SiteTypeId { get; set; }
        public DateTime? DemarcDateModified { get; set; }
        public string DemarcModifiedById { get; set; }
        public bool? IsDemarcCertified { get; set; }
        public Addressline1 AddressLine1 { get; set; }
        public Address[] Addresses { get; set; }
        public string BillingGeoCode { get; set; }
        public Building2 Building { get; set; }
        public Buildingaccess BuildingAccess { get; set; }
        public string BuildingExtension { get; set; }
        public Buildingprogram[] BuildingProgram { get; set; }
        public string CloneSiteId { get; set; }
        public Coloaccesstype[] ColoAccessTypes { get; set; }
        public Competitiveenvironment CompetitiveEnvironment { get; set; }
        public string DemarcCode { get; set; }
        public string DemarcName { get; set; }
        public string DispatchAreaId { get; set; }
        public string DispatchAreaName { get; set; }
        public string DispatchGatewayClli { get; set; }
        public string E911CommunityName { get; set; }
        public string FtRevenueMarketCityState { get; set; }
        public int?  FtRevenueMarketId { get; set; }
        public string FtRevenueMarketName { get; set; }
        public string FtRevenueMarketPCATId { get; set; }
        public string HCoordinate { get; set; }
        public Inventorysystem[] InventorySystems { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public bool? IsGeocodeManualOverride { get; set; }
        public string LATA { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MarketCityState { get; set; }
        public int?  MarketId { get; set; }
        public string MarketName { get; set; }
        public string MarketPCATId { get; set; }
        public string MarketProfitCenterId { get; set; }
        public string MarketState { get; set; }
        public string NPA { get; set; }
        public string NXX { get; set; }
        public Networkentity[] NetworkEntities { get; set; }
        public string OffNetOption { get; set; }
        public string OffNetOptionTypeId { get; set; }
        public Overbuildprogram[] OverBuildProgram { get; set; }
        public Preapprovedtype[] PreApprovedTypes { get; set; }
        public string PrecisionCode { get; set; }
        public string PrecisionName { get; set; }
        public string PreferredSiteCode { get; set; }
        public bool? PricingAreaEthernetCapable { get; set; }
        public string PricingAreaId { get; set; }
        public string PricingAreaName { get; set; }
        public bool? PricingAreaTdmCapable { get; set; }
        public Ratecenter RateCenter { get; set; }
        public string RedirectedToID { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string ReleaseToLOACFA { get; set; }
        public bool? RequestedIdRedirected { get; set; }
        public string ServedBy { get; set; }
        public string[] Serves { get; set; }
        public Servicelocation[] ServiceLocations { get; set; }
        public string ServingAreaId { get; set; }
        public string ServingAreaName { get; set; }
        public Sitecolo[] SiteCOLO { get; set; }
        public Sitecapabilitytypedata1[] SiteCapabilityTypeData { get; set; }
        public Sitecapabilitytype[] SiteCapabilityTypes { get; set; }
        public Sitecode[] SiteCodes { get; set; }
        public string SiteCreatedBy { get; set; }
        public string SiteCreationDate { get; set; }
        public string SiteIsInServiceAreaType { get; set; }
        public string SiteIsInServiceAreaTypeId { get; set; }
        public bool? SiteIsOnNet { get; set; }
        public Sitelateraldistance SiteLateralDistance { get; set; }
        public Siteleastcostroute SiteLeastCostRoute { get; set; }
        public string SiteName { get; set; }
        public Sitenetworkdata SiteNetworkData { get; set; }
        public Sitenetworkdistance SiteNetworkDistance { get; set; }
        public string SiteOnNetDate { get; set; }
        public Siteusage[] SiteUsages { get; set; }
        public string SubRegion { get; set; }
        public Twtctype[] TWTCTypes { get; set; }
        public Timezonedata TimeZoneData { get; set; }
        public string VCoordinate { get; set; }
        public string WireCenterCLLI { get; set; }
    }

    public class Addressline1
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool? HasWorkItems { get; set; }
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
        public int?  AddressId { get; set; }
        public string AddressLine1Combined { get; set; }
        public string AddressLine1and2Combined { get; set; }
        public string AddressLine2BuildingInfo { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeId { get; set; }
        public Building Building { get; set; }
        public string City { get; set; }
        public string CityInt { get; set; }
        public string CombinedAddressLine { get; set; }
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
        public int?  ResultMatch { get; set; }
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
        public string StreetNameSuffixType { get; set; }
        public string StreetNameSuffixTypeId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetNumberFraction { get; set; }
        public string StreetNumberPrefix { get; set; }
        public string SubBuildingNumber { get; set; }
        public string SubBuildingType { get; set; }
        public string SubCity { get; set; }
        public string USZip4 { get; set; }
        public Addresssource[] AddressSources { get; set; }
        public string BillingDateValidated { get; set; }
        public string ILECDateValidated { get; set; }
        public string MSAGDateValidated { get; set; }
        public string USPSDateValidated { get; set; }
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
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
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
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Addresssource
    {
        public int?  AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int?  AddressSourceId { get; set; }
        public string AddressSourceType { get; set; }
        public string AddressSourceTypeId { get; set; }
        public string AddressSourceVendorCombined { get; set; }
        public Building1 Building { get; set; }
        public string DateValidated { get; set; }
        public string IsActive { get; set; }
        public string IsManual { get; set; }
        public string LoginId { get; set; }
        public string Vendor { get; set; }
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
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
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
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Building2
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor2[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor2
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Buildingaccess
    {
        public string ErrorMessage { get; set; }
        public string DoorCode { get; set; }
        public bool? FieldControlled { get; set; }
        public string GovernmentBuildingType { get; set; }
        public string GovernmentBuildingTypeId { get; set; }
        public string KeyLocation { get; set; }
        public string ManagementInfo { get; set; }
        public string ManagementType { get; set; }
        public string ManagementTypeId { get; set; }
        public string MasterSiteId { get; set; }
        public string MrcExtensionType { get; set; }
        public string MrcExtensionTypeId { get; set; }
        public string SpecialInstructions { get; set; }
    }

    public class Competitiveenvironment
    {
        public string EthernetTypeId { get; set; }
        public string InternetTypeId { get; set; }
        public string MasterSiteId { get; set; }
        public string TDMTypeId { get; set; }
        public string TransportTypeId { get; set; }
        public string WaveTypeId { get; set; }
        public string EthernetType { get; set; }
        public string InternetType { get; set; }
        public string TDMType { get; set; }
        public string TransportType { get; set; }
        public string WaveType { get; set; }
    }

    public class Ratecenter
    {
        public string MasterSiteId { get; set; }
        public string RateCenterLATA { get; set; }
        public string RateCenterLATAName { get; set; }
        public string RateCenterName { get; set; }
        public int?  RateCenterId { get; set; }
    }

    public class Sitelateraldistance
    {
        public string MasterSiteId { get; set; }
        public float? PrimaryLateralDistance { get; set; }
        public float? SecondaryLateralDistance { get; set; }
        public string SiteLateralType { get; set; }
    }

    public class Siteleastcostroute
    {
        public string ErrorMessage { get; set; }
        public string IsActive { get; set; }
        public string LeastCostRouteSource { get; set; }
        public string LeastCostRouteUnit { get; set; }
        public string MasterSiteId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public float? RouteCost { get; set; }
        public float? TotalMeters { get; set; }
    }

    public class Sitenetworkdata
    {
        public string OffNetOption { get; set; }
        public string OffNetOptionTypeId { get; set; }
        public Siteconnectiontype[] SiteConnectionTypes { get; set; }
        public bool? SiteIsOnNet { get; set; }
        public Sitenetwork[] SiteNetworks { get; set; }
        public string SiteOnNetDate { get; set; }
    }

    public class Siteconnectiontype
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Sitenetwork
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Sitenetworkdistance
    {
        public string ErrorMessage { get; set; }
        public string DistanceSource { get; set; }
        public string DistanceUnit { get; set; }
        public float? DtnSearchRadius { get; set; }
        public string InSearchRadius { get; set; }
        public string IsActive { get; set; }
        public string MasterSiteId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public float? NetCapDistance { get; set; }
        public float? NetRouteDistance { get; set; }
        public float? SourceNetCapDistance { get; set; }
        public float? SourceNetRouteDistance { get; set; }
    }

    public class Timezonedata
    {
        public string ErrorMessage { get; set; }
        public string MasterSiteId { get; set; }
        public float? Offset { get; set; }
        public int?  TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
    }

    public class Address
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool? HasWorkItems { get; set; }
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
        public int?  AddressId { get; set; }
        public string AddressLine1Combined { get; set; }
        public string AddressLine1and2Combined { get; set; }
        public string AddressLine2BuildingInfo { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeId { get; set; }
        public Building3 Building { get; set; }
        public string City { get; set; }
        public string CityInt { get; set; }
        public string CombinedAddressLine { get; set; }
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
        public int?  ResultMatch { get; set; }
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
        public string StreetNameSuffixType { get; set; }
        public string StreetNameSuffixTypeId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetNumberFraction { get; set; }
        public string StreetNumberPrefix { get; set; }
        public string SubBuildingNumber { get; set; }
        public string SubBuildingType { get; set; }
        public string SubCity { get; set; }
        public string USZip4 { get; set; }
        public Addresssource1[] AddressSources { get; set; }
        public string BillingDateValidated { get; set; }
        public string ILECDateValidated { get; set; }
        public string MSAGDateValidated { get; set; }
        public string USPSDateValidated { get; set; }
    }

    public class Building3
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor3[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor3
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Addresssource1
    {
        public int?  AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int?  AddressSourceId { get; set; }
        public string AddressSourceType { get; set; }
        public string AddressSourceTypeId { get; set; }
        public string AddressSourceVendorCombined { get; set; }
        public Building4 Building { get; set; }
        public string DateValidated { get; set; }
        public string IsActive { get; set; }
        public string IsManual { get; set; }
        public string LoginId { get; set; }
        public string Vendor { get; set; }
    }

    public class Building4
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor4[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor4
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Buildingprogram
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Coloaccesstype
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Inventorysystem
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Networkentity
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool? HasWorkItems { get; set; }
        public string MasterSiteId { get; set; }
        public string SiteStatusType { get; set; }
        public string SiteStatusTypeId { get; set; }
        public string SiteType { get; set; }
        public string SiteTypeId { get; set; }
        public string CLLIIdPrefix { get; set; }
        public string CLLIIdSuffix { get; set; }
        public string CLONESCLLIIdSuffix { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public string MasterNetworkEntityId { get; set; }
        public string Name { get; set; }
        public Networkentitydesignator NetworkEntityDesignator { get; set; }
        public string NetworkEntityType { get; set; }
        public string NetworkEntityTypeId { get; set; }
        public string RedirectedToID { get; set; }
        public bool? RequestedIdRedirected { get; set; }
        public int?  SiteCodeId { get; set; }
        public int?  SiteDesignatorId { get; set; }
        public bool? SiteHasValidation { get; set; }
        public string TWLocationIdSuffix { get; set; }
        public string USZip4 { get; set; }
    }

    public class Networkentitydesignator
    {
        public string ErrorMessage { get; set; }
        public string FirstDesignatorType { get; set; }
        public string FirstDesignatorValue { get; set; }
        public string FirstDesignatorVariant { get; set; }
        public bool? IsPrimary { get; set; }
        public string SecondDesignatorType { get; set; }
        public string SecondDesignatorValue { get; set; }
        public string SecondDesignatorVariant { get; set; }
        public string ThirdDesignatorType { get; set; }
        public string ThirdDesignatorValue { get; set; }
        public string ThirdDesignatorVariant { get; set; }
        public string CombinedDesignators { get; set; }
        public string FirstDesignatorTypeId { get; set; }
        public string MasterNetworkEntityId { get; set; }
        public int?  NetworkEntityDesignatorId { get; set; }
        public string SecondDesignatorTypeId { get; set; }
        public string ThirdDesignatorTypeId { get; set; }
    }

    public class Overbuildprogram
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Preapprovedtype
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Servicelocation
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool? HasWorkItems { get; set; }
        public string MasterSiteId { get; set; }
        public string SiteStatusType { get; set; }
        public string SiteStatusTypeId { get; set; }
        public string SiteType { get; set; }
        public string SiteTypeId { get; set; }
        public string BuildingExtension { get; set; }
        public string CLLIIdPrefix { get; set; }
        public string CLLIIdSuffix { get; set; }
        public string CLONESCLLIIdSuffix { get; set; }
        public string DMARCType { get; set; }
        public string DMARCTypeName { get; set; }
        public Datacenter DataCenter { get; set; }
        public Demarcowner[] DemarcOwners { get; set; }
        public Demarcsubtype[] DemarcSubTypes { get; set; }
        public string Description { get; set; }
        public string Disposition { get; set; }
        public string DispositionName { get; set; }
        public bool? IsDefaultForSite { get; set; }
        public bool? IsOnNet { get; set; }
        public string MasterServiceLocationId { get; set; }
        public string OnNetDate { get; set; }
        public long?  PhysicalSiteEID { get; set; }
        public Primaryservicelocationdesignator PrimaryServiceLocationDesignator { get; set; }
        public string RedirectedToID { get; set; }
        public bool? RequestedIdRedirected { get; set; }
        public Servicelocationdesignator[] ServiceLocationDesignators { get; set; }
        public Sitecapabilitytypedata[] SiteCapabilityTypeData { get; set; }
        public Sitedesignatorcolo[] SiteDesignatorColo { get; set; }
        public int?  SiteDesignatorId { get; set; }
        public bool? SiteHasValidation { get; set; }
        public string TWLocationIdSuffix { get; set; }
        public string USZip4 { get; set; }
    }

    public class Datacenter
    {
        public string ErrorMessage { get; set; }
        public int?  DataCenterId { get; set; }
        public string DataCenterName { get; set; }
        public string MasterSiteId { get; set; }
    }

    public class Primaryservicelocationdesignator
    {
        public string ErrorMessage { get; set; }
        public string FirstDesignatorType { get; set; }
        public string FirstDesignatorValue { get; set; }
        public string FirstDesignatorVariant { get; set; }
        public bool? IsPrimary { get; set; }
        public string SecondDesignatorType { get; set; }
        public string SecondDesignatorValue { get; set; }
        public string SecondDesignatorVariant { get; set; }
        public string ThirdDesignatorType { get; set; }
        public string ThirdDesignatorValue { get; set; }
        public string ThirdDesignatorVariant { get; set; }
        public string CombinedDesignators { get; set; }
        public string FirstDesignatorTypeId { get; set; }
        public string MasterServiceLocationId { get; set; }
        public string SecondDesignatorTypeId { get; set; }
        public Servicelocationaddresssource ServiceLocationAddressSource { get; set; }
        public int?  ServiceLocationDesignatorId { get; set; }
        public string ThirdDesignatorTypeId { get; set; }
    }

    public class Servicelocationaddresssource
    {
        public Address1 Address { get; set; }
        public Addresssource3[] AddressSources { get; set; }
        public string ILECDateValidated { get; set; }
        public string USPSDateValidated { get; set; }
        public string ValidationStatusType { get; set; }
        public string ValidationStatusTypeId { get; set; }
    }

    public class Address1
    {
        public string __type { get; set; }
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool? HasWorkItems { get; set; }
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
        public int?  AddressId { get; set; }
        public string AddressLine1Combined { get; set; }
        public string AddressLine1and2Combined { get; set; }
        public string AddressLine2BuildingInfo { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeId { get; set; }
        public Building5 Building { get; set; }
        public string City { get; set; }
        public string CityInt { get; set; }
        public string CombinedAddressLine { get; set; }
        public bool? CoordinateLocation { get; set; }
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
        public int?  ResultMatch { get; set; }
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
        public Addresssource2[] AddressSources { get; set; }
        public string BillingDateValidated { get; set; }
        public string CustomName { get; set; }
        public string ILECDateValidated { get; set; }
        public string MSAGDateValidated { get; set; }
        public string PrimaryPromotion { get; set; }
        public string USPSDateValidated { get; set; }
    }

    public class Building5
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor5[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor5
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Addresssource2
    {
        public int?  AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int?  AddressSourceId { get; set; }
        public string AddressSourceType { get; set; }
        public string AddressSourceTypeId { get; set; }
        public string AddressSourceVendorCombined { get; set; }
        public Building6 Building { get; set; }
        public string DateValidated { get; set; }
        public string IsActive { get; set; }
        public string IsManual { get; set; }
        public string LoginId { get; set; }
        public string Vendor { get; set; }
    }

    public class Building6
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor6[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor6
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Addresssource3
    {
        public int?  AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int?  AddressSourceId { get; set; }
        public string AddressSourceType { get; set; }
        public string AddressSourceTypeId { get; set; }
        public string AddressSourceVendorCombined { get; set; }
        public Building7 Building { get; set; }
        public string DateValidated { get; set; }
        public string IsActive { get; set; }
        public string IsManual { get; set; }
        public string LoginId { get; set; }
        public string Vendor { get; set; }
    }

    public class Building7
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor7[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor7
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Demarcowner
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Demarcsubtype
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Servicelocationdesignator
    {
        public string ErrorMessage { get; set; }
        public string FirstDesignatorType { get; set; }
        public string FirstDesignatorValue { get; set; }
        public string FirstDesignatorVariant { get; set; }
        public bool? IsPrimary { get; set; }
        public string SecondDesignatorType { get; set; }
        public string SecondDesignatorValue { get; set; }
        public string SecondDesignatorVariant { get; set; }
        public string ThirdDesignatorType { get; set; }
        public string ThirdDesignatorValue { get; set; }
        public string ThirdDesignatorVariant { get; set; }
        public string CombinedDesignators { get; set; }
        public string FirstDesignatorTypeId { get; set; }
        public string MasterServiceLocationId { get; set; }
        public string SecondDesignatorTypeId { get; set; }
        public Servicelocationaddresssource1 ServiceLocationAddressSource { get; set; }
        public int?  ServiceLocationDesignatorId { get; set; }
        public string ThirdDesignatorTypeId { get; set; }
    }

    public class Servicelocationaddresssource1
    {
        public Address2 Address { get; set; }
        public Addresssource5[] AddressSources { get; set; }
        public string ILECDateValidated { get; set; }
        public string USPSDateValidated { get; set; }
        public string ValidationStatusType { get; set; }
        public string ValidationStatusTypeId { get; set; }
    }

    public class Address2
    {
        public string __type { get; set; }
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool? HasWorkItems { get; set; }
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
        public int?  AddressId { get; set; }
        public string AddressLine1Combined { get; set; }
        public string AddressLine1and2Combined { get; set; }
        public string AddressLine2BuildingInfo { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeId { get; set; }
        public Building8 Building { get; set; }
        public string City { get; set; }
        public string CityInt { get; set; }
        public string CombinedAddressLine { get; set; }
        public bool? CoordinateLocation { get; set; }
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
        public int?  ResultMatch { get; set; }
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
        public Addresssource4[] AddressSources { get; set; }
        public string BillingDateValidated { get; set; }
        public string CustomName { get; set; }
        public string ILECDateValidated { get; set; }
        public string MSAGDateValidated { get; set; }
        public string PrimaryPromotion { get; set; }
        public string USPSDateValidated { get; set; }
    }

    public class Building8
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor8[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor8
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Addresssource4
    {
        public int?  AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int?  AddressSourceId { get; set; }
        public string AddressSourceType { get; set; }
        public string AddressSourceTypeId { get; set; }
        public string AddressSourceVendorCombined { get; set; }
        public Building9 Building { get; set; }
        public string DateValidated { get; set; }
        public string IsActive { get; set; }
        public string IsManual { get; set; }
        public string LoginId { get; set; }
        public string Vendor { get; set; }
    }

    public class Building9
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor9[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor9
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Addresssource5
    {
        public int?  AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int?  AddressSourceId { get; set; }
        public string AddressSourceType { get; set; }
        public string AddressSourceTypeId { get; set; }
        public string AddressSourceVendorCombined { get; set; }
        public Building10 Building { get; set; }
        public string DateValidated { get; set; }
        public string IsActive { get; set; }
        public string IsManual { get; set; }
        public string LoginId { get; set; }
        public string Vendor { get; set; }
    }

    public class Building10
    {
        public string ErrorMessage { get; set; }
        public string BuildingName { get; set; }
        public string BuildingProgramTypeId { get; set; }
        public string BuildingStructureTypeId { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeValue { get; set; }
        public string DiverseEntranceTypeId { get; set; }
        public string IsAllFloorsServedTypeId { get; set; }
        public bool? IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int?  NumberOfBasementFloors { get; set; }
        public int?  NumberOfFloors { get; set; }
        public Sitebuildingfloor10[] SiteBuildingFloors { get; set; }
        public string BuildingProgramType { get; set; }
        public string BuildingStructureType { get; set; }
        public string BuildingType { get; set; }
        public string CommonDMARC { get; set; }
        public string DiverseEntranceType { get; set; }
        public string IsAllFloorsServedType { get; set; }
    }

    public class Sitebuildingfloor10
    {
        public string ErrorMessage { get; set; }
        public string Floor { get; set; }
        public int?  FloorDistance { get; set; }
        public bool? HasNoDistributionRights { get; set; }
        public bool? IsCapitalNeeded { get; set; }
        public bool? IsFloorServed { get; set; }
        public bool? IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int?  SiteBuildingId { get; set; }
        public int?  SiteBuildingFloorId { get; set; }
    }

    public class Sitecapabilitytypedata
    {
        public string BandwidthTypeId { get; set; }
        public string BandwidthTypeName { get; set; }
        public bool? DefaultProtected { get; set; }
        public bool? DefaultUnProtected { get; set; }
        public float? DisplayOrder { get; set; }
        public string MasterSiteId { get; set; }
        public bool? Protected { get; set; }
        public string SiteCapabilityTypeId { get; set; }
        public string SiteCapabilityTypeName { get; set; }
        public bool? UnProtected { get; set; }
        public int?  SiteDesignatorId { get; set; }
    }

    public class Sitedesignatorcolo
    {
        public string ColoServiceCode { get; set; }
        public string ColoServiceName { get; set; }
        public string ColoStateCode { get; set; }
        public string ColoStateName { get; set; }
    }

    public class Sitecolo
    {
        public string ColoServiceCode { get; set; }
        public string ColoServiceName { get; set; }
        public string ColoStateCode { get; set; }
        public string ColoStateName { get; set; }
    }

    public class Sitecapabilitytypedata1
    {
        public string BandwidthTypeId { get; set; }
        public string BandwidthTypeName { get; set; }
        public bool? DefaultProtected { get; set; }
        public bool? DefaultUnProtected { get; set; }
        public float? DisplayOrder { get; set; }
        public string MasterSiteId { get; set; }
        public bool? Protected { get; set; }
        public string SiteCapabilityTypeId { get; set; }
        public string SiteCapabilityTypeName { get; set; }
        public bool? UnProtected { get; set; }
        public int?  SiteDesignatorId { get; set; }
    }

    public class Sitecapabilitytype
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Sitecode
    {
        public string __type { get; set; }
        public string ErrorMessage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPreferred { get; set; }
        public string MasterSiteId { get; set; }
        public string SiteCode { get; set; }
        public string SiteCodeStatus { get; set; }
        public string SiteCodeStatusName { get; set; }
        public string SiteCodeType { get; set; }
        public string SiteCodeTypeName { get; set; }
        public bool? IsPreferredChange { get; set; }
        public int?  SiteCodeId { get; set; }
    }

    public class Siteusage
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Twtctype
    {
        public string ErrorMessage { get; set; }
        public bool? IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

}
