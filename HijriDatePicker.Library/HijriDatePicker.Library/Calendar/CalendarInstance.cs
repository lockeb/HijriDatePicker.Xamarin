using Android.Content;

namespace HijriDatePicker.Library.Calendar
{
	/// <summary>
	///     Created by Alhazmy13 on 2/3/16.
	/// </summary>
	public class CalendarInstance : ICustomCalendarView
	{
		private readonly GregorianCalendar _georgian;
		private readonly HijriCalendar _hijri;
		private readonly int _mMode;
		private int _currentYear;
		private Context _mContext;

		public CalendarInstance(Context context, int mode)
		{
			_hijri = new HijriCalendar(context);
			_georgian = new GregorianCalendar(context);
			_mMode = mode;
		}

		//
		public void plusMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
			{
				_hijri.plusMonth();
				return;
			}
			_georgian.plusMonth();
		}

		public void minusMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
			{
				_hijri.minusMonth();
				return;
			}
			_georgian.minusMonth();
		}

		public bool isCurrentMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.isCurrentMonth();
			return _georgian.isCurrentMonth();
		}

		public void setMonth(int month)
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
			{
				_hijri.setMonth(month);
				return;
			}
			_georgian.setMonth(month);
		}

		public void setDay(int day)
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
			{
				_hijri.setDay(day);
				return;
			}
			_georgian.setDay(day);
		}

		public void setYear(int year)
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
			{
				_hijri.setYear(year);
				return;
			}
			_georgian.setYear(year);
		}

		public int getWeekStartFrom()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.getWeekStartFrom();
			return _georgian.getWeekStartFrom();
		}

		public int GetLastDayOfMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetLastDayOfMonth();
			return _georgian.GetLastDayOfMonth();
		}

		public int GetDayOfMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetDayOfMonth();
			return _georgian.GetDayOfMonth();
		}

		public int GetMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetMonth();
			return _georgian.GetMonth();
		}

		public string GetMonthName()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetMonthName();
			return _georgian.GetMonthName();
		}

		public int GetYear()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetYear();
			return _georgian.GetYear();
		}

		public int lengthOfMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.lengthOfMonth();
			return _georgian.lengthOfMonth();
		}

		public int GetCurrentMonth()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetCurrentMonth();
			return _georgian.GetCurrentMonth();
		}

		public int GetOffsetMonthCount()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetOffsetMonthCount();
			return _georgian.GetOffsetMonthCount();
		}

		public string[] GetMonths()
		{
			var temp1 = _hijri.GetMonths();
			var temp2 = _georgian.GetMonths();
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetMonths();
			return _georgian.GetMonths();
		}

		public int GetCurrentYear()
		{
			if (_mMode == HijriCalendarDialog.Mode.Hijri.ModeValue)
				return _hijri.GetCurrentYear();
			return _georgian.GetCurrentYear();
		}
	}
}