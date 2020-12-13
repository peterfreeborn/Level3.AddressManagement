using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model.LocationService.OrderLocation
{
     public class OrderNotificationResponse
    {
        public string ErrorMessage { get; set; }
        public string MasterId { get; set; }
        public string RedirectedToID { get; set; }
        public bool? RequestedIdRedirected { get; set; }
        public int? WorkQueueId { get; set; }
    }


}
