using System;
using System.Collections.Generic;
using Android.Content;

namespace HijriDatePicker.Library
{
    /// <summary>
    ///     Created by Alhazmy13 on 1/6/16.
    ///     HijriDatePicker
    /// </summary>
    public class HijriCalendarDialog
    {
        public sealed class Mode
        {
            public enum InnerEnum
            {
                Hijri,
                Gregorian
            }

            public static readonly Mode Hijri = new Mode("Hijri", InnerEnum.Hijri, 1);
            public static readonly Mode Gregorian = new Mode("Gregorian", InnerEnum.Gregorian, 2);

            private static readonly IList<Mode> valueList = new List<Mode>();
            private static int nextOrdinal;
            private readonly InnerEnum innerEnumValue;

            private readonly string nameValue;
            private readonly int ordinalValue;
            internal int mode;

            static Mode()
            {
                valueList.Add(Hijri);
                valueList.Add(Gregorian);
            }

            internal Mode(string name, InnerEnum innerEnum, int mode)
            {
                this.mode = mode;

                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;
            }

            public int ModeValue
            {
                get { return mode; }
            }


            public static IList<Mode> values()
            {
                return valueList;
            }

            public InnerEnum InnerEnumValue()
            {
                return innerEnumValue;
            }

            public int ordinal()
            {
                return ordinalValue;
            }

            public override string ToString()
            {
                return nameValue;
            }

            public static Mode valueOf(string name)
            {
                foreach (var enumInstance in values())
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new ArgumentException(name);
            }
        }

        public sealed class Language
        {
            public enum InnerEnum
            {
                Arabic,
                English,
                Default
            }

            public static readonly Language Arabic = new Language("Arabic", InnerEnum.Arabic, 1);
            public static readonly Language English = new Language("English", InnerEnum.English, 2);
            public static readonly Language Default = new Language("Default", InnerEnum.Default, 3);

            private static readonly IList<Language> valueList = new List<Language>();
            private static int nextOrdinal;
            private readonly InnerEnum innerEnumValue;

            private readonly string nameValue;
            private readonly int ordinalValue;
            internal int language;

            static Language()
            {
                valueList.Add(Arabic);
                valueList.Add(English);
                valueList.Add(Default);
            }

            internal Language(string name, InnerEnum innerEnum, int language)
            {
                this.language = language;

                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;
            }

            public int LanguageValue
            {
                get { return language; }
            }


            public static IList<Language> values()
            {
                return valueList;
            }

            public InnerEnum InnerEnumValue()
            {
                return innerEnumValue;
            }

            public int ordinal()
            {
                return ordinalValue;
            }

            public override string ToString()
            {
                return nameValue;
            }

            public static Language valueOf(string name)
            {
                foreach (var enumInstance in values())
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new ArgumentException(name);
            }
        }

        public class Builder
        {
            public Builder(Context context)
            {
                GeneralAttribute.mContext = context;
                GeneralAttribute.title = "";
                GeneralAttribute.hijri_max = 1450;
                GeneralAttribute.hijri_min = 1437;
                GeneralAttribute.gregorian_max = 2050;
                GeneralAttribute.gregorian_min = 2013;
                GeneralAttribute.language = Language.Default.LanguageValue;
                GeneralAttribute.scrolling = true;
            }

            public virtual Builder setMaxHijriYear(int maxYear)
            {
                GeneralAttribute.hijri_max = maxYear;
                return this;
            }

            public virtual Builder setMinHijriYear(int minYear)
            {
                GeneralAttribute.hijri_min = minYear;
                return this;
            }

            public virtual Builder setMinMaxHijriYear(int min, int max)
            {
                GeneralAttribute.hijri_max = max;
                GeneralAttribute.hijri_min = min;
                return this;
            }

            public virtual Builder setEnableScrolling(bool scrolling)
            {
                GeneralAttribute.scrolling = scrolling;
                return this;
            }

            public virtual Builder setMaxGregorianYear(int maxYear)
            {
                GeneralAttribute.gregorian_max = maxYear;
                return this;
            }

            public virtual Builder setMinGregorianYear(int minYear)
            {
                GeneralAttribute.gregorian_min = minYear;
                return this;
            }

            public virtual Builder setMinMaxGregorianYear(int min, int max)
            {
                GeneralAttribute.gregorian_max = max;
                GeneralAttribute.gregorian_min = min;
                return this;
            }

            public virtual Builder setUILanguage(Language language)
            {
                GeneralAttribute.language = language.LanguageValue;
                return this;
            }

            public virtual Builder setOnDateSetListener(HijriCalendarView.OnDateSetListener onDateSetListener)
            {
                GeneralAttribute.OnDateSetListener = onDateSetListener;
                return this;
            }

            public virtual Builder show()
            {
                new HijriCalendarView(GeneralAttribute.mContext).Show();
                return this;
            }

            public virtual Builder setMode(Mode mode)
            {
                GeneralAttribute.mode = mode;
                return this;
            }

            public virtual Builder setDefaultHijriDate(int day, int month, int year)
            {
                if (month > 11 || month < 0)
                {
                    throw new Exception("Month must be between 0-11");
                }
                GeneralAttribute.setDefaultDate = true;
                GeneralAttribute.defaultDay = day;
                GeneralAttribute.defaultMonth = month > 11 ? 0 : month;
                GeneralAttribute.defaultYear = year;
                return this;
            }
        }
    }
}