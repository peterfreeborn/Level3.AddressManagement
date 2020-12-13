using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.BLL
{
   public class AddressMigrator
    {
        // Public Props
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        // Private members
        private Model.SourceAddress _objAddress;

        private static string SITE_LOCATION_SERVICE_URI = "LocationService.svc/SiteLocation/v2/";


        // Constructor
        public AddressMigrator()
        {
            _lstErrorMessages = new List<string>();
        }



        // Public Methods
        public bool MigrateAddress(Model.SourceAddress objAddress)
        {
            _objAddress = objAddress;

            try
            {

                /// -----------------------
                /// PSUEDO CODE:
                /// -----------------------
                /// 
                /// (??  Which Address MODEL class is being fed into -->  private void callGLMForSiteInfo(Address address)  = which kicks off all things, so... the source address - conversion table?  this class will read all addresses from the conversion table that have not yet been processed
                /// (??? Why are we hard-coded to ENV3 - which Environments should we be using ???)
                /// (??? Why ware we hard-coded to US Addresses - will this hold firm ???)
                /// 
                /// 
                /// 
                /// Foreach SOURCE ADDRESS:
                /// 
                /// CHECK to see if this is a "special" address, and if so, translate the Address1 field value accordingly
                ///     Logical Check:  If field 'Address1'.ToLower() contains "level 3" or "centurylink"  
                ///     Translation:    Overwrite the 'Address1' field with the contents of 'Address2' (i.e. - Leave 'Address2' as-is)  
                ///     (??? What's the significance of these "special" addresses ???)
                ///     (??? Should we be trimming any address field values, woudl it matter ???)
                ///     
                /// 
                /// CHECK to see if the GLMMasterSiteID has been supplied on the [SOURCE ADDRESS] ...
                /// 
                ///     IF the [GLMMasterSiteID] HAS been supplied (if exists & > 0) ...
                ///     (??? Will this ever happen - the model class for the source address object does not have a prop for this value ???)
                ///     
                ///         [Java Method: getSiteLocationByMasterSiteId()] 
                ///         MAKE an HTTP GET to GLM to retrieve the [SITE LOCATION] resource (supplying the GLMMasterSiteID in the ROUTE, and with a set of boolean typed QS params as shown below)
                ///             Note:  QS params -> The call to GLM requests that the search INCLDUES: SERVICE LOCATIONS ... and EXCLUDES: AllData, BuildingLocations, NetworkEntities, SiteNotes, AllAddresses, or BuildingAccess
                ///             Note:  GLM ROUTE -> "GLMSWebServices/LocationService.svc/SiteLocation/v2/{ID}" - (Java Method: getSiteLocationByMasterSiteId) 
                ///                         full url structure =     http://glm.level3.com/GLMSWebServices/LocationService.svc/SiteLocation/v2/{ID}?ad={INCLUDEALLDATA}&bd={INCLUDEBUILDINGDETAILDATA}&sd={INCLUDESERVICELOCATIONS}&nd={INCLUDENETWORKENTITIES}&sn={INCLUDESITENOTES}&sa={INCLUDEALLADDRESSES}&ba={INCLUDEBUILDINGACCESS}
                ///                         full raw route =         LocationService.svc/SiteLocation/v2/{masterSiteID}?sd=true&ad=false&bd=false&nd=false&sn=false&ba=false
                ///                         raw QS params values =   sd=true & ad=false & bd=false & nd=false & sn=false & ba=false 
                ///                         http headers:            "Authorization:", "X-GLM-API-Authorization:"
                ///                 
                /// 
                ///             IF the MASTER SITE does NOT exist or CANNOT be retrieved with the ID supplied... we continue to look for the address by up-to 2 additional means....
                ///             [Java Method: getSiteLocation() --> getSiteLocationByClliCode(), then if not found, getSiteLocationByAddress()] 
                ///                 
                ///                 CHECK to see if the SOURCE ADDRESS has a CLII code...
                ///                 (??? Will this ever happen - the model class for the source address object does not have a prop for this value ???)
                ///
                ///                     If a CLII Code has been supplied on the [SOURCE ADDRESS]... try to get the SITE RESOURCE using that value....
                ///                         
                ///                         [Java Method: getSiteLocationByClliCode()] 
                ///                         MAKE an HTTP call to GLM to retrieve the [SITE LOCATION] resource using the [CLII] Code
                ///                         Note:  QS params -> The call to GLM requests that the search INCLDUES: SERVICE LOCATIONS ... and EXCLUDES: AllData, BuildingLocations, NetworkEntities, SiteNotes, AllAddresses, or BuildingAccess                            
                ///                         Note:  GLM ROUTE -> "GLMSWebServices/LocationService.svc/SiteLocation/v2/{ID}" - (Java Method: getSiteLocationByMasterSiteId) 
                ///                                     full url structure =     http://glm.level3.com/GLMSWebServices/LocationService.svc/SiteLocation/v2/{ID}?ad={INCLUDEALLDATA}&bd={INCLUDEBUILDINGDETAILDATA}&sd={INCLUDESERVICELOCATIONS}&nd={INCLU
                ///                                     full raw route =         LocationService.svc/SiteLocation/v2/{CLIICode}?ad=true
                ///                                     raw QS params values =   ad=true
                ///                                     http headers:            "Authorization:", "X-GLM-API-Authorization:"
                /// 
                ///                         
                ///                     IF a CLII Code has NOT been supplied 
                ///                     -OR- 
                ///                     IF the Clii supplied returned NO RESULT and/or an ERROR...
                ///                         
                ///                             MAKE an HTTP POST to GLM to retrieve the [SITE LOCATION] by [ADDRESS] (a search by the raw address fields, supplied as part of the CONTENT BODY)
                ///                                 Note:  Fields supplied -> Name3, Address1, City, State, Zip, Country, Floor, Room, Suite, and '70' 
                ///                                         (address fields:  address, city, state, zip, country, floor, room, suite, minscore)  
                ///                                         (?? should '70', the 'minscore' ever need to be adjusted; does this belong as a config value ???  If so, is this a global change, or would iterative calls with different minscores be needed?)
                ///                                 Note:  If the response starts with "77u/", then strip that value.  
                ///                                         (??? Huh, what's up with this goofiness???)
                ///                                 Note:  QS params -> (??? Do we need to supply "i" as a QS param??? = i = REMOVE INTERNATIONALIZATION)                          
                ///                                 Note:  GLM ROUTE -> "GLMSWebServices/SearchLocationService.svc/AdvancedLocationQuery/v2?i={true/false}"
                ///                                             full url structure =     http://glm.level3.com/GLMSWebServices/SearchLocationService.svc/AdvancedLocationQuery/v2?i={REMOVEINTERNATIONALIZATION}
                ///                                             full raw route =         LocationService.svc/SiteLocation/v2/{CLIICode}?ad=true
                ///                                             raw QS params values =   i=true/fase (?? Needed ??)
                ///                                             http headers:            "Authorization:", "X-GLM-API-Authorization:"
                ///                                             
                /// 
                ///                             CHECK to see if the [SITE LOCATION] was retrieved
                ///                             
                ///                                 IF the [SITE LOCATION] was NOT retrieved by the time we get here...
                ///                                 
                ///                                     CHECK to see if this is a "special" address condition...
                ///                                         Logical Check:  If field 'Address1'.ToLower() contains "level 3"  
                ///                                         Translation:    Overwrite the 'Address1' field with the contents of 'Address2' AND overwrite 'Address2' with a blank-value/empty-string)  
                ///
                ///                                     ??? Does this make a difference?  As part of the first step in this code, should we just blank out 'Address2', thus allowing us to skip this recheck???
                ///                                     (In essence, the only difference than the first pass through is that we rerun it with 'Address2' supplied as blank).  
                ///
                ///                                     IF Address1 contains 'level 3", then REPEAT the entire above logic, but this time substituting Address2 for Address1:    
                ///                                          Overwriting the value in 'Address1' with the value in 'Address2'
                ///                                          Setting 'Address2' to Blank/No Value
                ///                                     
                ///                 
                /// IF we have NOT located an address in GLM up to this point, attempt to CREATE IT
                /// 
                ///     Cleanup Address Lines 1 and 2
                ///     (??? Can we get this code - not shown in code provided ???)    
                /// 
                ///     MAKE at HTTP POST to GLM to CREATE/ADD the [SERVICE LOCATION]
                ///         createServiceLocation
                /// 
                /// Else
                ///     (??? WHAT SHOULD HAPPEN IF WE GET HERE ???)
                ///     


                // TODO:  Add calls to GLM
                //objGLMClient.DoStuff();

            }
            catch (Exception ex)
            {
                string strErrorMessage = String.Format("There was an error while trying to migrate the addresses.  Error Message = [{0}]", ex.Message);
                _lstErrorMessages.Add(strErrorMessage);
            }

            return (_lstErrorMessages.Count == 0);
        }


    }
}
