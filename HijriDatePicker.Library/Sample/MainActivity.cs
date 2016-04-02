using Android.App;
using Android.OS;
using Android.Widget;
using HijriDatePicker.Library;

namespace Sample
{
    [Activity(Label = "Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, HijriCalendarView.OnDateSetListener
    {
        private int count = 1;

        public void onDateSet(int year, int month, int day)
        {
            var date = year + "/" + month + "/" + day;
            Toast.MakeText(this, date, ToastLength.Long).Show();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {
                //Without setting default date
                new HijriCalendarDialog.Builder(this)
                    .setOnDateSetListener(this)
                    .setMinMaxHijriYear(1430, 1450)
                    .setMinMaxGregorianYear(2013, 2020)
                    .setUILanguage(HijriCalendarDialog.Language.Arabic)
                    .setMode(HijriCalendarDialog.Mode.Hijri)
                    .show();
            };
        }
    }
}