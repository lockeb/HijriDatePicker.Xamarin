using Android.Content;

namespace HijriDatePicker.Library
{
    public class GeneralAttribute
    {
        public static Context mContext;
        public static string title;
        public static int language;
        public static HijriCalendarView.OnDateSetListener OnDateSetListener;
        public static HijriCalendarDialog.Mode mode = HijriCalendarDialog.Mode.Hijri;
        public static int hijri_min;
        public static int hijri_max;
        public static int gregorian_min;
        public static int gregorian_max;
        public static bool setDefaultDate = false;
        public static int defaultDay;
        public static int defaultMonth;
        public static int defaultYear;
        public static bool scrolling;
    }
}