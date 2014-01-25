﻿using Dimensional.TinyReturns.Core.DateExtend;

namespace Dimensional.TinyReturns.Core
{
    public class CalculateReturnRequestFactory
    {
        public static CalculateReturnRequest ThreeMonth(
            MonthYear endMonth)
        {
            return new CalculateReturnRequest(
                endMonth, 3);
        }

        public static CalculateReturnRequest YearToDate(
            MonthYear endMonth)
        {
            var firstMonthOfYear = new MonthYear(endMonth.Year, 1);

            var diffMonths = endMonth - firstMonthOfYear + 1;

            return new CalculateReturnRequest(
                endMonth, diffMonths);
        }
    }
}