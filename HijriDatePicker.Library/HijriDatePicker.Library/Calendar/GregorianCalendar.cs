using Android.Content;
using Java.Util;

namespace HijriDatePicker.Library.Calendar
{
	public class GregorianCalendar : ICustomCalendarView
	{
		private readonly int _currentMonth;
		private readonly int _currentYear;
		private Java.Util.Calendar _calendar;
		private int _countMonth;
		private int _countYear;
		private readonly string[] _monthNames;

		public GregorianCalendar(Context context)
		{
			_calendar = Java.Util.Calendar.Instance;
			_monthNames = new[]
			{
				context.Resources.GetString(Resource.String.January),
				context.Resources.GetString(Resource.String.February),
				context.Resources.GetString(Resource.String.March), context.Resources.GetString(Resource.String.April),
				context.Resources.GetString(Resource.String.May),
				context.Resources.GetString(Resource.String.June),
				context.Resources.GetString(Resource.String.July),
				context.Resources.GetString(Resource.String.August),
				context.Resources.GetString(Resource.String.September),
				context.Resources.GetString(Resource.String.October),
				context.Resources.GetString(Resource.String.November),
				context.Resources.GetString(Resource.String.December)
			};
			_countMonth = _calendar.Get(CalendarField.Month);
			_countYear = _calendar.Get(CalendarField.Year);
			_currentMonth = _countMonth;
			_currentYear = _countYear;
		}

		public void plusMonth()
		{
			_countMonth++;
			if (_countMonth == 12)
			{
				_countMonth = 0;
				_countYear++;
			}
			_calendar = Java.Util.Calendar.Instance;
			_calendar.Set(CalendarField.Year, _countYear);
			_calendar.Set(CalendarField.Month, _countMonth);
		}

		public void minusMonth()
		{
			_countMonth--;
			if (_countMonth == -1)
			{
				_countMonth = 11;
				_countYear--;
			}
			_calendar = Java.Util.Calendar.Instance;
			_calendar.Set(CalendarField.Year, _countYear);
			_calendar.Set(CalendarField.Month, _countMonth);
		}

		public bool isCurrentMonth()
		{
			return _countMonth == _currentMonth && _currentYear == _countYear;
		}

		public void setMonth(int month)
		{
			_countMonth = month;
			_calendar.Set(CalendarField.Month, month);
		}

		public void setDay(int day)
		{
			_calendar.Set(CalendarField.DayOfMonth, day);
		}

		public void setYear(int year)
		{
			_countMonth = year;
			_calendar.Set(CalendarField.Year, year);
		}

		public int getWeekStartFrom()
		{
			var temp = Java.Util.Calendar.Instance;
			temp.Set(_calendar.Get(CalendarField.Year), _calendar.Get(CalendarField.Month), 1);
            var weekstartfrom = temp.Get(CalendarField.DayOfWeek);
		    return weekstartfrom;

		}

		public int GetLastDayOfMonth()
		{
            var lastdayofmoth = _calendar.GetActualMaximum(CalendarField.DayOfMonth);
            return lastdayofmoth;
		}

		public int GetDayOfMonth()
		{
            var dayofmonth = _calendar.Get(CalendarField.DayOfMonth);
		    return dayofmonth;

		}

		public int GetMonth()
		{
			return _countMonth + 1;
		}

		public string GetMonthName()
		{
			return _monthNames[GetOffsetMonthCount()];
		}

		public int GetYear()
		{
			return _calendar.Get(CalendarField.Year);
		}

		public int lengthOfMonth()
		{
			var length = _calendar.GetActualMaximum(CalendarField.DayOfMonth);
			return length;
		}

		public int GetCurrentMonth()
		{
			return GetOffsetMonthCount();
		}

		public int GetOffsetMonthCount()
		{
			if (_countMonth < 0)
				return 11;
			if (_countMonth > 11)
				return 0;
			return _countMonth;
		}

		public string[] GetMonths()
		{
			return _monthNames;
		}

		public int GetCurrentYear()
		{
			return _countYear;
		}
	}
}