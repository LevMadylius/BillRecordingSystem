using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillRecordingSystem.Classes
{
    public static class DateHelper
    {
        public static DateTime FirstDayWeek()
        {
            DateTime result = DateTime.Today.AddDays(-7);
            return result;
        }

        public static DateTime FirstDayMonth()
        {
            DateTime result = DateTime.Today.AddDays(-30);
            return result;
        }

    }
}
