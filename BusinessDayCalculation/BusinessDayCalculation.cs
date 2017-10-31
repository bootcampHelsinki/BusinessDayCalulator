using System;
using System.Globalization;

namespace BusinessDayCalculation
{
    public class BusinessDayCalculation
    {

        public int GetBusinessDays(string startDateString, string endDateString)
        {
            var startDate = ParseDate(startDateString);
            var endDate = ParseDate(endDateString);

            if (startDate > endDate)
            {
                throw new Exception($"start date: {startDate} is greater than {endDate}");
            }

            var dateDiff = (endDate - startDate).TotalDays + 1;
            var businessDayCount = 0;

            for (int day = 0; day < dateDiff; day++)
            {
                if (day == 0)
                {
                    var weekDayStartDate = (int)startDate.DayOfWeek;

                    if (weekDayStartDate != 0)
                    {
                        businessDayCount++;
                    }
                    continue;
                }

                var vacationDay = startDate.AddDays(day);

                if (vacationDay.DayOfWeek != 0)
                {
                    businessDayCount++;
                }
            }

            return businessDayCount;
        }

        public DateTime ParseDate(string dateFormat)
        {
            if (string.IsNullOrEmpty(dateFormat))
            {
                throw new ArgumentNullException(nameof(dateFormat));
            }

            DateTime date;

            if (DateTime.TryParseExact(dateFormat, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.AdjustToUniversal, out date ))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateFormat, "dd.MM.yyyy", new CultureInfo("en-US"), DateTimeStyles.AdjustToUniversal, out date))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateFormat, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.AdjustToUniversal, out date))
            {
                return date;
            }

            throw new Exception($"failed to parse date format {dateFormat}");
        }
    }
}
