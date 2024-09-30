using PersianTools.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Learning.Application.Convertors
{
    public static class DateConvertor
    {
        public static  string ToShamsi(this DateTime value)
        {
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(value) + "/" + 
                pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }
        public static DateTime DateShamsiToMiladi(this string value)
        {
            PersianCalendar pc = new PersianCalendar();
            string[] std = value.Split('/');
            DateTime dt = new DateTime(int.Parse(std[0]),
                int.Parse(std[1]),
                int.Parse(std[2]),
                new PersianCalendar()
                );
            return dt;
        }
        public static bool PersianNumberToEnglishNumber(this string persianStr, out string date)
        {
            try
            {
                Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
                {
                    ['۰'] = '0',
                    ['۱'] = '1',
                    ['۲'] = '2',
                    ['۳'] = '3',
                    ['۴'] = '4',
                    ['۵'] = '5',
                    ['۶'] = '6',
                    ['۷'] = '7',
                    ['۸'] = '8',
                    ['۹'] = '9',
                    ['/'] = '/'
                };
                foreach (var item in persianStr)
                {
                    persianStr = persianStr.Replace(item, LettersDictionary[item]);
                }
                date = persianStr;
                return true;
            }
            catch
            {
                date = "";
                return false;
            }

        }
    }
}
