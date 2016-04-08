using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using HijriDatePicker.Library.Calendar;
using Java.Lang;
using Java.Util;

namespace HijriDatePicker.Library
{
    public class HijriCalendarView : Dialog, View.IOnClickListener, MonthDialog.IOnMonthChanged,
        YearDialog.IOnYearChanged
    {
        private readonly Context _context;
        private CalendarInstance calendarInstance;
        private string[] days;
        private TableRow daysHeader;
        private TextView dayTextView, monthTextView, yearTextView, lastSelectedDay;
        private Button doneButton, cancelButton;
        private TableLayout tableLayout;
        private List<TextView> textViewList;

        public HijriCalendarView(Context context, bool cancelable, EventHandler cancelHandler)
            : base(context, cancelable, cancelHandler)
        {
        }

        public HijriCalendarView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public HijriCalendarView(Context context) : base(context)
        {
            _context = context;
            RequestWindowFeature((int) WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.dialog_hijri_calendar);

            FlowFunctions();
        }

        public HijriCalendarView(Context context, bool cancelable, IDialogInterfaceOnCancelListener cancelListener)
            : base(context, cancelable, cancelListener)
        {
        }

        public HijriCalendarView(Context context, int themeResId) : base(context, themeResId)
        {
        }

        public void OnClick(View v)
        {
            var temp = (TextView) v;
            if (!string.IsNullOrEmpty(temp.Text))
            {
                if (lastSelectedDay != null)
                {
                    lastSelectedDay.SetTextColor(Color.DarkGray);
                    lastSelectedDay.SetBackgroundColor(Color.Transparent);
                }

                temp.SetBackgroundColor(
                    new Color(ContextCompat.GetColor(_context, Resource.Color.hijri_date_picker_accent_color)));
                temp.SetTextColor(Color.White);
                lastSelectedDay = temp;
                dayTextView.Text = temp.Text;
                calendarInstance.setDay(Integer.ParseInt(temp.Text));
            }
        }


        public void onMonthChanged(int month)
        {
            calendarInstance.setMonth(month + 1);
            InitDays();
        }

        public void OnYearChanged(int year)
        {
            calendarInstance.setYear(year);
            InitDays();
        }

        private void FlowFunctions()
        {
            InitViews();
            InitHeaderOfCalender();
            InitDays();
            monthTextView.Touch += (sender, args) =>
            {
                var handled = false;
                if (args.Event.Action == MotionEventActions.Down)
                {
                    var monthDialog = new MonthDialog(_context);
                    monthDialog.setOnDateChanged(this);
                    monthDialog.SetMonthNames(calendarInstance.GetMonths());
                    monthDialog.SetCurrentMonth(calendarInstance.GetCurrentMonth());
                    monthDialog.Show();
                    handled = true;
                }
                args.Handled = handled;
            };
            yearTextView.Touch += (sender, args) =>
            {
                var handled = false;
                if (args.Event.Action == MotionEventActions.Down)
                {
                    var yearDialog = new YearDialog(_context);
                    yearDialog.SetOnYearChanged(this);
                    yearDialog.SetYear(Integer.ParseInt(yearTextView.Text.ToString()));
                    yearDialog.Show();
                    handled = true;
                }
                args.Handled = handled;
            };
            doneButton.Click += delegate
            {
                if (GeneralAttribute.OnDateSetListener != null)
                {
                    GeneralAttribute.OnDateSetListener.onDateSet(calendarInstance.GetYear(), calendarInstance.GetMonth(),
                        calendarInstance.GetDayOfMonth());
                }
                Dismiss();
            };
            cancelButton.Click += delegate { Dismiss(); };
        }

        private void InitViews()
        {
            tableLayout = FindViewById<TableLayout>(Resource.Id.calendarTableLayout);
            dayTextView = FindViewById<TextView>(Resource.Id.dayTextView);
            monthTextView = FindViewById<TextView>(Resource.Id.monthTextView);
            yearTextView = FindViewById<TextView>(Resource.Id.yearTextView);
            doneButton = FindViewById<Button>(Resource.Id.doneButton);
            cancelButton = FindViewById<Button>(Resource.Id.closeButton);
            //SetButtonTint(doneButton);
            //SetButtonTint(cancelButton);
            days = _context.Resources.GetStringArray(Resource.Array.hijri_date_picker_days);
            textViewList = new List<TextView>();
            if (GeneralAttribute.language == HijriCalendarDialog.Language.Arabic.LanguageValue)
            {
                CallSwitchLang("ar");
            }
            else if (GeneralAttribute.language == HijriCalendarDialog.Language.English.LanguageValue)
            {
                CallSwitchLang("en");
            }
            calendarInstance = new CalendarInstance(_context, GeneralAttribute.mode.ModeValue);
            if (GeneralAttribute.setDefaultDate)
            {
                calendarInstance.setDay(GeneralAttribute.defaultDay);
                calendarInstance.setMonth(GeneralAttribute.defaultMonth);
                calendarInstance.setYear(GeneralAttribute.defaultYear);
            }
        }

        public Button SetButtonTint(Button button)
        {
            if (SdkUtils.HasLollipop() && button is AppCompatButton)
            {
                ((AppCompatButton) button).SupportBackgroundTintList =
                    ColorStateList.ValueOf(
                        new Color(ContextCompat.GetColor(_context, Resource.Color.hijri_date_picker_accent_color)));
            }
            else
            {
                ViewCompat.SetBackgroundTintList(button,
                    ColorStateList.ValueOf(
                        new Color(ContextCompat.GetColor(_context, Resource.Color.hijri_date_picker_accent_color))));
            }
            return button;
        }

        private void CallSwitchLang(string langCode)
        {
            var locale = new Locale(langCode);
            Locale.Default = locale;
            var config = new Configuration {Locale = locale};
            _context.Resources.UpdateConfiguration(config, _context.Resources.DisplayMetrics);
            OnCreate(null);
        }

        private void InitHeaderOfCalender()
        {
            var layout = new TableRow.LayoutParams(0, ViewGroup.LayoutParams.WrapContent, 1f);
            layout.SetMargins(0, 8, 0, 8);
            daysHeader = new TableRow(_context);
            daysHeader.SetGravity(GravityFlags.Center);
            for (var i = 0; i < 7; i++)
            {
                var textView = new TextView(_context);
                textView.LayoutParameters = layout;
                textView.TextSize = 11;
                textView.SetTextColor(
                    new Color(ContextCompat.GetColor(_context, Resource.Color.hijri_date_picker_accent_color)));
                textView.Gravity = GravityFlags.Center;
                textView.Text = days[i];
                daysHeader.AddView(textView);
            }
        }

        private void InitDays()
        {
            tableLayout.RemoveAllViews();
            tableLayout.AddView(daysHeader);
            UpdateCalenderInformation();

            var count = 1;
            var firstTime = true;
            var layout = new TableRow.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent, 1f);
            layout.SetMargins(0, 8, 0, 8);
            for (var i = 0; i < 5; i++)
            {
                var row = new TableRow(_context);
                row.SetGravity(GravityFlags.Center);
                for (var j = 1; j <= 7; j++)
                {
                    var textView = new TextView(_context)
                    {
                        LayoutParameters = layout,
                        Gravity = GravityFlags.Center
                    };
                    textView.SetOnClickListener(this);
                    textView.SetTextColor(Color.DarkGray);
                    if (count <= calendarInstance.lengthOfMonth())
                    {
                        if (firstTime && j == calendarInstance.getWeekStartFrom())
                        {
                            textView.Text = GeneralAttribute.language ==
                                            HijriCalendarDialog.Language.Arabic.LanguageValue
                                ? Utility.ToArabicNumbers(count + "")
                                : count + "";
                            firstTime = false;
                            count++;
                        }
                        else if (!firstTime)
                        {
                            textView.Text = GeneralAttribute.language ==
                                            HijriCalendarDialog.Language.Arabic.LanguageValue
                                ? Utility.ToArabicNumbers(count + "")
                                : count + "";
                            count++;
                        }
                        else
                        {
                            textView.Text = " ";
                        }
                    }
                    else
                    {
                        textView.Text = " ";
                    }

                    if ((calendarInstance.GetCurrentMonth() == GeneralAttribute.defaultMonth ||
                         (calendarInstance.GetCurrentMonth() == GeneralAttribute.defaultMonth &&
                          calendarInstance.GetCurrentYear() == GeneralAttribute.defaultYear)) &&
                        count - 1 == calendarInstance.GetDayOfMonth())
                    {
                        textView.SetBackgroundColor(
                            new Color(ContextCompat.GetColor(_context, Resource.Color.hijri_date_picker_accent_color)));
                        textView.SetTextColor(Color.White);
                        lastSelectedDay = textView;
                    }
                    textViewList.Add(textView);
                    row.AddView(textView);
                }
                tableLayout.AddView(row);
            }
        }

        private void UpdateCalenderInformation()
        {
            dayTextView.Text = GeneralAttribute.language == HijriCalendarDialog.Language.Arabic.LanguageValue
                ? Utility.ToArabicNumbers(calendarInstance.GetDayOfMonth() + "")
                : calendarInstance.GetDayOfMonth() + "";
            monthTextView.Text = calendarInstance.GetMonthName();
            yearTextView.Text = GeneralAttribute.language == HijriCalendarDialog.Language.Arabic.LanguageValue
                ? Utility.ToArabicNumbers(calendarInstance.GetYear() + "")
                : calendarInstance.GetYear() + "";
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            throw new NotImplementedException();
        }

        public interface OnDateSetListener
        {
            void onDateSet(int year, int month, int day);
        }
    }
}