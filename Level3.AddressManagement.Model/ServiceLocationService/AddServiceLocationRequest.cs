using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model.ServiceLocationService.Requests
{
    public class AddServiceLocationRequest
    {
        public string ErrorMessage { get; set; }
        public string CLLIId { get; set; }
        public bool HasWorkItems { get; set; }
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
        public bool IsDefaultForSite { get; set; }
        public bool IsOnNet { get; set; }
        public string MasterServiceLocationId { get; set; }
        public string OnNetDate { get; set; }
        public long PhysicalSiteEID { get; set; }
        public Primaryservicelocationdesignator PrimaryServiceLocationDesignator { get; set; }
        public string RedirectedToID { get; set; }
        public bool RequestedIdRedirected { get; set; }
        public Servicelocationdesignator[] ServiceLocationDesignators { get; set; }
        public Sitecapabilitytypedata[] SiteCapabilityTypeData { get; set; }
        public Sitedesignatorcolo[] SiteDesignatorColo { get; set; }
        public int? SiteDesignatorId { get; set; }
        public bool SiteHasValidation { get; set; }
        public string TWLocationIdSuffix { get; set; }
        public string USZip4 { get; set; }
    }

    public class Datacenter
    {
        public string ErrorMessage { get; set; }
        public int DataCenterId { get; set; }
        public string DataCenterName { get; set; }
        public string MasterSiteId { get; set; }
    }

    public class Primaryservicelocationdesignator
    {
        public string ErrorMessage { get; set; }
        public string FirstDesignatorType { get; set; }
        public string FirstDesignatorValue { get; set; }
        public string FirstDesignatorVariant { get; set; }
        public bool IsPrimary { get; set; }
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
        public int ServiceLocationDesignatorId { get; set; }
        public string ThirdDesignatorTypeId { get; set; }
    }

    public class Servicelocationaddresssource
    {
        public Address Address { get; set; }
        public Addresssource[] AddressSources { get; set; }
        public string ILECDateValidated { get; set; }
        public string USPSDateValidated { get; set; }
        public string ValidationStatusType { get; set; }
        public string ValidationStatusTypeId { get; set; }
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

    public class Addresssource
    {
        public int AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int AddressSourceId { get; set; }
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

    public class Demarcowner
    {
        public string ErrorMessage { get; set; }
        public bool IsRecordActive { get; set; }
        public string TypeId { get; set; }
        public string TypeIdAlternate { get; set; }
        public string TypeName { get; set; }
    }

    public class Demarcsubtype
    {
        public string ErrorMessage { get; set; }
        public bool IsRecordActive { get; set; }
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
        public bool IsPrimary { get; set; }
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
        public int ServiceLocationDesignatorId { get; set; }
        public string ThirdDesignatorTypeId { get; set; }
    }

    public class Servicelocationaddresssource1
    {
        public Address1 Address { get; set; }
        public Addresssource1[] AddressSources { get; set; }
        public string ILECDateValidated { get; set; }
        public string USPSDateValidated { get; set; }
        public string ValidationStatusType { get; set; }
        public string ValidationStatusTypeId { get; set; }
    }

    public class Address1
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
        public Building2 Building { get; set; }
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
        public bool IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int NumberOfBasementFloors { get; set; }
        public int NumberOfFloors { get; set; }
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
        public int FloorDistance { get; set; }
        public bool HasNoDistributionRights { get; set; }
        public bool IsCapitalNeeded { get; set; }
        public bool IsFloorServed { get; set; }
        public bool IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int SiteBuildingId { get; set; }
        public int SiteBuildingFloorId { get; set; }
    }

    public class Addresssource1
    {
        public int AddressId { get; set; }
        public string AddressSourceDescription { get; set; }
        public int AddressSourceId { get; set; }
        public string AddressSourceType { get; set; }
        public string AddressSourceTypeId { get; set; }
        public string AddressSourceVendorCombined { get; set; }
        public Building3 Building { get; set; }
        public string DateValidated { get; set; }
        public string IsActive { get; set; }
        public string IsManual { get; set; }
        public string LoginId { get; set; }
        public string Vendor { get; set; }
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
        public bool IsDedicatedTenant { get; set; }
        public string MasterSiteId { get; set; }
        public int NumberOfBasementFloors { get; set; }
        public int NumberOfFloors { get; set; }
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
        public int FloorDistance { get; set; }
        public bool HasNoDistributionRights { get; set; }
        public bool IsCapitalNeeded { get; set; }
        public bool IsFloorServed { get; set; }
        public bool IsNotAccessible { get; set; }
        public string MasterSiteId { get; set; }
        public int SiteBuildingId { get; set; }
        public int SiteBuildingFloorId { get; set; }
    }

    public class Sitecapabilitytypedata
    {
        public string BandwidthTypeId { get; set; }
        public string BandwidthTypeName { get; set; }
        public bool DefaultProtected { get; set; }
        public bool DefaultUnProtected { get; set; }
        public float DisplayOrder { get; set; }
        public string MasterSiteId { get; set; }
        public bool Protected { get; set; }
        public string SiteCapabilityTypeId { get; set; }
        public string SiteCapabilityTypeName { get; set; }
        public bool UnProtected { get; set; }
        public int? SiteDesignatorId { get; set; } // BJM Made this nillable because we found a BUG in the GLM interface that they will supposidly fix.
    }

    public class Sitedesignatorcolo
    {
        public string ColoServiceCode { get; set; }
        public string ColoServiceName { get; set; }
        public string ColoStateCode { get; set; }
        public string ColoStateName { get; set; }
    }

}
