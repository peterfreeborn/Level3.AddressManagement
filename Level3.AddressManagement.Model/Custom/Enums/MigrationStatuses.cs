using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    // EXECUTE THIS AGAINST THE DB TO PRODUCE THE ENUM VALUES SHOWN BELOW
    // SELECT CONCAT(REPLACE(MigrationStatusDesc,' ','_'),' = ', MigrationStatusID, ',' ) FROM tblMigrationStatus 

    public enum MigrationStatuses
    {
        STAGED_for_Processing = 1,
        GLM_SITE_FOUND_or_CREATED = 2,
        API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK = 3,
        ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors = 4,
        STAGED_for_SITE_CODE_existence_CHECK = 5,
        API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK = 6,
        ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors = 7,
        SITE_CODE_FOUND_in_GLM = 8,
        SITE_CODE_does_NOT_EXIST_in_GLM = 9,
        STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM = 10,
        SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM = 11,
        API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM = 12,
        ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors = 13,
        WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT = 14,
        STAGED_for_SAP_SITE_LOCATION_SEARCH = 15,
        API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH = 16,
        ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors = 17,
        Site_Location_ADDRESS_FOUND_in_SAP = 18,
        Site_Location_ADDRESS_does_NOT_EXIST_in_SAP = 19,
        READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP = 20,
        API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP = 21,
        ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors = 22,
        READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION = 23,
        STAGED_for_GLM_SERVICE_LOCATION_SEARCH = 24,
        API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH = 25,
        ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors = 26,
        SERVICE_LOCATION_FOUND_in_GLM = 27,
        SERVICE_LOCATION_does_NOT_EXIST_in_GLM = 28,
        STAGED_for_SERVICE_LOCATION_CREATION_in_GLM = 29,
        SERVICE_LOCATION_Successfully_CREATED_in_GLM = 30,
        API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt = 31,
        ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors = 32,
        STAGED_for_S_CODE_existence_CHECK = 33,
        API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK = 34,
        ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors = 35,
        S_CODE_FOUND_in_GLM = 36,
        S_CODE_does_NOT_EXIST_in_GLM = 37,
        STAGED_for_S_CODE_CREATION_in_GLM = 38,
        S_CODE_Successfully_CREATED_in_GLM = 39,
        API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt = 40,
        ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors = 41,
        WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION = 42,
        STAGED_for_SAP_SERVICE_LOCATION_SEARCH = 43,
        API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH = 44,
        ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors = 45,
        Service_Location_ADDRESS_FOUND_in_SAP = 46,
        Service_Location_ADDRESS_does_NOT_EXIST_in_SAP = 47,
        READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP = 48,
        API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP = 49,
        ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors = 50,
        Processing_COMPLETE = 51,
        IGNORED_indefinitely = 52
    }
}
