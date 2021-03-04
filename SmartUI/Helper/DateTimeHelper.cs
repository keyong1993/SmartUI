using System;

namespace SmartUI.Common
{
    public static class DateTimeHelper
    {
        public static long? ToTimestamp(this DateTime dateTime)
        {
            if (dateTime == null)
                return null;
            TimeSpan ts = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static long? ToTimestamp(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            return ((DateTime)dateTime).ToUniversalTime().ToTimestamp();
        }

        public static DateTime ToDateTime(this long? timeStamp)
        {
            if (timeStamp != null)
            {
                return ((long)timeStamp).ToDateTime();
            }
            return default;
        }

        public static DateTime ToDateTime(this long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            // DateTime dtStart = new DateTime(1970, 1, 1);
            TimeSpan toNow = new TimeSpan(timeStamp * 10000000);
            return dtStart.Add(toNow);
        }

        public static long GetNowTimeStamp()
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}

