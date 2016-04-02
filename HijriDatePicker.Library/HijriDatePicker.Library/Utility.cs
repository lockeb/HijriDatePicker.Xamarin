namespace HijriDatePicker.Library
{

    public class Utility
    {
        public static string ToEnglishNumbers(string day)
        {
            var englishNumbers =
                day.Replace("١", "1")
                    .Replace("٢", "2")
                    .Replace("٣", "3")
                    .Replace("٤", "4")
                    .Replace("٥", "5")
                    .Replace("٦", "6")
                    .Replace("٧", "7")
                    .Replace("٨", "8")
                    .Replace("٩", "9")
                    .Replace("٠", "0");
            return englishNumbers;
        }

        public static string ToArabicNumbers(string day)
        {
            var arabicNumbers =
                day.Replace("1", "١")
                    .Replace("2", "٢")
                    .Replace("3", "٣")
                    .Replace("4", "٤")
                    .Replace("5", "٥")
                    .Replace("6", "٦")
                    .Replace("7", "٧")
                    .Replace("8", "٨")
                    .Replace("9", "٩")
                    .Replace("0", "٠");
            return arabicNumbers;
        }
    }
}