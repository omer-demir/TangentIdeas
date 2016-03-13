using System;
using System.Globalization;

namespace TangentIdeas.Web.Data.Helpers {
    public static class DataUtilities {
        public static string YYYY = "yyyy";
        public static string MM = "MM";
        public static string DD = "dd";
        public static string HH = "HH";
        public static string mm = "mm";
        public static string SS = "ss";

        public static string yyyyMMdd = YYYY + MM + DD;
        public static string yyyyMMddHHmm = yyyyMMdd + HH + mm;
        public static string yyyyMMddHHmmss = yyyyMMddHHmm + SS;

        public static long DateTimeNowLong() {
            long val = default(long);
            if(long.TryParse(DateTime.Now.ToString("yyyyMMddHHmmss"),out val)) { }

            return val;

        }

        public static long DateTimeNowLong(string format) {
            long val = default(long);
            if(long.TryParse(DateTime.Now.ToString(format),out val)) { }

            return val;

        }

        public static long ConvertToLong(this DateTime dt) {
            long val = default(long);
            if(long.TryParse(DateTime.Now.ToString("yyyyMMddHHmmss"),out val)) { }

            return val;

        }


        public static DateTime ConvertStringToDate(this string date) {
            DateTime result = DateTime.MinValue;
            try {

                if(DateTime.TryParseExact(date,new[] { "yyyyMMddHHmmss","yyyyMMddHHmm" },CultureInfo.InvariantCulture,
                    DateTimeStyles.None,out result)) {
                    //date parsed
                }
            } catch(Exception) {
                //couldnt parse possibly null
            }
            return result;
        }
    }
}
