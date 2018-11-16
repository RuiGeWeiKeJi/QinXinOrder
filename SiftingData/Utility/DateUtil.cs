using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class DateUtil
    {
        /// <summary>
        /// java long值转 c# datetime 
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static DateTime Java_LongToCSharp_DateTime(long tick)
        {
            DateTime javaStartDateTime = new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc);
            DateTime datetime = javaStartDateTime.Add ( new TimeSpan(  tick * TimeSpan.TicksPerMillisecond ) ).ToLocalTime();
            return datetime;
        }
    }
}
