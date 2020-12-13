using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Level3.AddressManagement.Model
{
    public class StopwatchUtil
    {
        public static string GetHumanReadableTimeElapsedString(Stopwatch objStopWatch)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan objTimeSpan = objStopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string strElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds, objTimeSpan.Milliseconds / 10);

            // return the value to the caller
            return strElapsedTime;
        }


        public static string GetHumanReadableTimeElapsedString(TimeSpan objTimeSpan)
        {
            // Format and display the TimeSpan value.
            string strElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds, objTimeSpan.Milliseconds / 10);

            // return the value to the caller
            return strElapsedTime;
        }


        public static string GetHumanReadableTimeElapsedStringFromTicks(Int64 intTicks)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan objTimeSpan = TimeSpan.FromTicks(intTicks);

            // Format and display the TimeSpan value.
            string strElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds, objTimeSpan.Milliseconds / 10);

            // return the value to the caller
            return strElapsedTime;
        }
    }
}
