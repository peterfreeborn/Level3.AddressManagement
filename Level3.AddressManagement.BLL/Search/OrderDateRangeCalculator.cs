using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.BLL
{
    public static class OrderDateRangeCalculator
    {


        public static bool CalcCurrentDateRangeDate(DateTime? dteBaseDateCST, out DateTime? dteDateRangeStartDate, out DateTime? dteDateRangeEndDate)
        {
            dteDateRangeStartDate = null;
            dteDateRangeEndDate = null;

            try
            {
                if (dteBaseDateCST.HasValue)
                {

                    // Get the date supplied
                    DateTime dteCurrentDate = dteBaseDateCST.Value;

                    // Get the day for the last two weeks
                    dteDateRangeStartDate = dteCurrentDate.Date.AddDays(-14).AddDays(1);

                    // Add a month for any pre-orders that might be loaded
                    dteDateRangeEndDate = dteCurrentDate.AddDays(1).Date.AddMilliseconds(-1);

                    // Normalize Timezone to UTC
                    dteDateRangeStartDate = DateTime.SpecifyKind(dteDateRangeStartDate.Value, DateTimeKind.Local);
                    dteDateRangeEndDate = DateTime.SpecifyKind(dteDateRangeEndDate.Value, DateTimeKind.Local);

                    return (dteDateRangeEndDate.HasValue && dteDateRangeStartDate.HasValue);
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}