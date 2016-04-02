namespace HijriDatePicker.Library.Calendar
{
	/// <summary>
	///     Created by Alhazmy13 on 2/3/16.
	/// </summary>
	public interface ICustomCalendarView
	{
        void plusMonth();
        void minusMonth();
        bool isCurrentMonth();
        void setMonth(int month);
        void setDay(int day);
        void setYear(int year);
        int getWeekStartFrom();
        int GetLastDayOfMonth();
        int GetDayOfMonth();
        int GetMonth();
        string GetMonthName();
        int GetYear();
        int lengthOfMonth();
        int GetCurrentMonth();
        int GetOffsetMonthCount();
        string[] GetMonths();
        int GetCurrentYear();
    }
}