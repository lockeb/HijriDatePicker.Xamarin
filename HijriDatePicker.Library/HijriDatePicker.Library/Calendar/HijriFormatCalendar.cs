using System;
using System.Globalization;
using Android.Content;

namespace HijriDatePicker.Library.Calendar
{
	public class HijriCalendar : ICustomCalendarView
	{
	    private Context _mcontext;
		private readonly int _currentMonth;
		private readonly int _currentYear;
		private readonly string[] _monthNames;
		private readonly GregorianCalendar _gregorianCalendar;
		private readonly UmAlQuraCalendar _hijriCalendar;
		private int _countMonth;
		private int _countYear;
		private DateTime _currentDateTime;
		private DateTime _currentHijriDateTime;
        public HijriCalendar(Context context)
        {
            _mcontext = context;
            _gregorianCalendar = new GregorianCalendar(context);
            _hijriCalendar = new UmAlQuraCalendar();
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
			var y = _hijriCalendar.GetYear(DateTime.Now);
			var m = _hijriCalendar.GetMonth(DateTime.Now);
			var d = _hijriCalendar.GetDayOfMonth(DateTime.Now);
			_currentHijriDateTime = new DateTime(y, m, d, _hijriCalendar);

			_currentDateTime = DateTime.UtcNow;

			_countMonth = _hijriCalendar.GetMonth(DateTime.Now);
			_countYear = _hijriCalendar.GetYear(DateTime.Now);
			_currentMonth = _countMonth;
			_currentYear = _countYear;
		}

		public void plusMonth()
		{
			_countMonth++;
			if (_countMonth == 13)
			{
				_countMonth = 1;
				_countYear++;
			}
			_currentDateTime.AddMonths(1);
		}

		public void minusMonth()
		{
			_countMonth--;
			if (_countMonth == 0)
			{
				_countMonth = 12;
				_countYear--;
			}
			_currentDateTime.AddMonths(-1);
		}

		public bool isCurrentMonth()
		{
			return _countYear == _currentMonth && _currentYear == _countYear;
		}

		public void setMonth(int month)
		{
			_countMonth = month;
            _currentHijriDateTime = new DateTime(_countYear, _countMonth + 1, _currentDateTime.Day);

            CultureInfo arSA = new CultureInfo("ar-SA");
            arSA.DateTimeFormat.Calendar = new UmAlQuraCalendar();
            var dateValue = DateTime.ParseExact(_currentHijriDateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy", arSA);
		    _currentDateTime = dateValue;
            //_hijriCalendar.GetMonth(_currentHijriDateTime);
        }

		public void setDay(int day)
		{
			_currentHijriDateTime = new DateTime(_countYear, _countMonth  + 1, day);

            CultureInfo arSA = new CultureInfo("ar-SA");
            arSA.DateTimeFormat.Calendar = new UmAlQuraCalendar();
            var dateValue = DateTime.ParseExact(_currentHijriDateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy", arSA);
            _currentDateTime = dateValue;
            //_hijriCalendar.GetDayOfMonth(_currentHijriDateTime);
        }

		public void setYear(int year)
		{
			_countYear = year;
			_currentHijriDateTime = new DateTime(_countYear, _countMonth + 1, _currentDateTime.Day);

            CultureInfo arSA = new CultureInfo("ar-SA");
            arSA.DateTimeFormat.Calendar = new UmAlQuraCalendar();
            var dateValue = DateTime.ParseExact(_currentHijriDateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy", arSA);
            _currentDateTime = dateValue;
            //_hijriCalendar.GetYear(_currentHijriDateTime);
		}

		public int getWeekStartFrom()
		{
			var temp = new UmAlQuraCalendar();
			var tempdate = new DateTime(_countYear, _countMonth + 1, 1);

            CultureInfo arSA = new CultureInfo("ar-SA");
            arSA.DateTimeFormat.Calendar = new UmAlQuraCalendar();
            var dateValue = DateTime.ParseExact(tempdate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy", arSA);

            var weekstartfrom = (int)temp.GetDayOfWeek(dateValue);
		    if (weekstartfrom == 0)
		    {
		        return 1;
		    }
		    return weekstartfrom;

		}

		public int GetLastDayOfMonth()
		{
            var lastdayofmonth = _hijriCalendar.GetDaysInMonth(_currentDateTime.Year, _currentDateTime.Month);
            return lastdayofmonth;
		}

		public int GetDayOfMonth()
		{
		    var dayofmonth = _hijriCalendar.GetDayOfMonth(_currentDateTime);

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
			return _hijriCalendar.GetYear(_currentDateTime);
		}

		public int lengthOfMonth()
		{
			var year = _hijriCalendar.GetYear(_currentDateTime);
			var month = _hijriCalendar.GetMonth(_currentDateTime);
            var length = _hijriCalendar.GetDaysInMonth(_hijriCalendar.GetYear(_currentDateTime), _hijriCalendar.GetMonth(_currentDateTime));
		    return length;

		}

		public int GetCurrentMonth()
		{
		    var test = GetOffsetMonthCount();

            return GetOffsetMonthCount();
		}

		public int GetOffsetMonthCount()
		{
			var temp = _countMonth;
			if (temp == -1)
				temp = 11;
			else if (temp >= 12)
				temp = 0;
			return temp;
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