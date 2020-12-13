using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    public enum BatchType
    {
        AllAddressesNeedingProcessing,
        AlreadyInWorkflow,
        OnlyNewFromCDW,
        Errors,
        SAPChecks,
        AlreadyInWorkflowWithNoErrors
    }
}
