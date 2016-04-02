using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace HijriDatePicker.Library
{
	/// <summary>
	///     Created by Alhazmy13 on 1/6/16.
	///     HijriDatePicker
	/// </summary>
	public class YearDialog : Dialog, IDialogInterfaceOnDismissListener, View.IOnClickListener
	{
		private Context _mContext;
		private NumberPicker _numberPicker;
		private Button _ok, _close;
		private Button _okButton;
		private IOnYearChanged _onYearChanged;
		private int _year;

		public YearDialog(Context context) : base(context)
		{
			_mContext = context;
			RequestWindowFeature((int)WindowFeatures.NoTitle);
			SetContentView(Resource.Layout.dialog_year);
			SetOnDismissListener(this);
			InitViews();
			if (GeneralAttribute.mode.ModeValue == HijriCalendarDialog.Mode.Hijri.ModeValue)
			{
				_numberPicker.MaxValue = GeneralAttribute.hijri_max;
				_numberPicker.MinValue = GeneralAttribute.hijri_min;
			}
			else
			{
				_numberPicker.MaxValue = GeneralAttribute.gregorian_max;
				_numberPicker.MinValue = GeneralAttribute.gregorian_min;
			}
		}

		public void OnDismiss(IDialogInterface dialog)
		{
			_onYearChanged.OnYearChanged(_numberPicker.Value);
		}

		public void OnClick(View v)
		{
			_onYearChanged.OnYearChanged(_numberPicker.Value);
			Dismiss();
		}

		public void SetOnYearChanged(IOnYearChanged listen)
		{
			_onYearChanged = listen;
		}

		private void InitViews()
		{
			_numberPicker = FindViewById<NumberPicker>(Resource.Id.numberPicker);
			_okButton = FindViewById<Button>(Resource.Id.okBT);
			_okButton.SetOnClickListener(this);
		}

		public void setTitle(string title)
		{
			FindViewById<TextView>(Resource.Id.title).Text = title;
		}

		public void SetYear(int year)
		{
			_numberPicker.Value = year;
		}

		public interface IOnYearChanged
		{
			void OnYearChanged(int year);
		}
	}
}