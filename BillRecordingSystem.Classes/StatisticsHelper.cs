using BillRecordingSystem.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillRecordingSystem.Classes
{
    public static class StatisticsHelper
    {
        public static int GetSpentForPeriod(DateTime dateFrom,DateTime dateTo, int userId)
        {
            if (dateFrom > dateTo)
                return 0;

            var list = Queries.GetFilteredListExpances(userId, dateFrom, dateTo);
            if (list.Count == 0)
                return 0;

            int result = list.Sum(m => m.MonthAmount);
            return result;
        }
        
    }

    
}
