using Android.OS;

namespace HijriDatePicker.Library
{
	/// <summary>
	///     Class containing some Android version number utility methods.
	/// </summary>
	public class SdkUtils
	{
		public static bool HasGingerbread()
		{
			return Build.VERSION.SdkInt >= BuildVersionCodes.Gingerbread;
		}

		public static bool HasHoneycomb()
		{
			return Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb;
		}

		public static bool HasIceCreamSandwich()
		{
			return Build.VERSION.SdkInt >= BuildVersionCodes.IceCreamSandwich;
		}

		public static bool HasJellyBean()
		{
			return Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean;
		}

		public static bool HasKitKat()
		{
			return Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat;
		}

		public static bool HasLollipop()
		{
			return Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
		}
	}
}