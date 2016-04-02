using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace HijriDatePicker.Library
{
    public class MonthDialog : Dialog, View.IOnTouchListener
    {
        private int _currentMonth;
        private readonly Context _mContext;
        private string[] _monthNames;
        private IOnMonthChanged _onMonthChanged;


        internal List<TextView> TextViews = new List<TextView>();

        /// <param name="context"> </param>
        public MonthDialog(Context context) : base(context)
        {
            _mContext = context;
            RequestWindowFeature((int) WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.dialog_months);

            TextViews.Add(FindViewById<TextView>(Resource.Id.m1TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m2TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m3TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m4TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m5TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m6TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m7TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m8TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m9TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m10TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m11TextView));
            TextViews.Add(FindViewById<TextView>(Resource.Id.m12TextView));
            for (var i = 0; i < TextViews.Count; i++)
            {
                TextViews[i].SetOnTouchListener(this);
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                var temp = (TextView) v;
                if (_onMonthChanged != null)
                {
                    _onMonthChanged.onMonthChanged(TextViews.IndexOf(temp) - 1);
                }
                Dismiss();
                return true;
            }

            return false;
        }

        public void setOnDateChanged(IOnMonthChanged listen)
        {
            _onMonthChanged = listen;
        }

        public void SetCurrentMonth(int currentMonth)
        {
            _currentMonth = currentMonth;
            TextViews[currentMonth].SetBackgroundResource(Resource.Drawable.hijri_date_picker_card_selected);
            TextViews[currentMonth].SetTextColor(Color.White);
        }

        public void SetMonthNames(string[] monthNames)
        {
            for (var i = 0; i < monthNames.Length; i++)
            {
                TextViews[i].Text = monthNames[i];
                TextViews[i].SetTextColor(_mContext.Resources.GetColor(Resource.Color.hijri_date_picker_accent_color));
            }
        }

        public virtual void UpdateMonthTexts()
        {
        }

        /// 
        public interface IOnMonthChanged
        {
            void onMonthChanged(int month);
        }
    }
}