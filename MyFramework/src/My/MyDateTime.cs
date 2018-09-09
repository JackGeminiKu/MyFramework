using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My
{
    public class MyDateTime
    {
        public static bool IsWeekend
        {
            get
            {
                return DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
            }
        }

        public static bool IsNotWeekend
        {
            get
            {
                return !IsWeekend;
            }
        }
    }
}
