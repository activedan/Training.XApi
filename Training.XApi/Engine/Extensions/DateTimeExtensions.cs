using System;

namespace Training.XApi.Engine.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetHumanReadableDate(this DateTime date, bool titleCase = false)
        {
            const int Second = 1;
            const int Minute = 60 * Second;
            const int Hour = 60 * Minute;
            const int Day = 24 * Hour;
            const int Week = 7 * Day;
            const int Month = 30 * Day;

            const int JustNowThreshold = 5 * Second;

            var now = DateTime.UtcNow;

            var ts = date - now;
            double delta = Math.Abs(ts.TotalSeconds);
            var absHours = Math.Abs(ts.Hours);
            var absMinutes = Math.Abs(ts.Minutes);
            var absSeconds = Math.Abs(ts.Seconds);
            var absDays = Math.Abs(ts.Days);

            var humanDate = "";

            if (delta < JustNowThreshold)
                return titleCase ? "Just now" : "just now";

            else if (delta < Minute)
                humanDate = absSeconds + " seconds";

            else if (delta < Hour)
                if (absMinutes == 1)
                    humanDate = titleCase ? "A minute" : "a minute";
                else
                    humanDate = absMinutes + " minutes";

            else if (delta < Day)
                if (absHours == 1)
                    humanDate = titleCase ? "An hour" : "a hour";
                else
                    humanDate = absHours + " hours";

            else if ((now.Date - date.Date).Days == 1)
                return titleCase ? "Yesterday" : "yesterday";

            else if (delta < Week)
                if (absDays == 1)
                    humanDate = titleCase ? "A day" : "a day";
                else
                    humanDate = absDays + " days";

            else if (delta < Month)
            {
                int weeks = Convert.ToInt32(Math.Floor((double)absDays / 7));
                if (weeks < 2)
                    humanDate = titleCase ? "A week" : "a week";
                else
                    humanDate = weeks + " weeks";
            }
            else if (delta < 12 * Month)
            {
                int month = Convert.ToInt32(Math.Floor((double)absDays / 30));
                if (month < 2)
                    humanDate = titleCase ? "A month" : "a month";
                else
                    humanDate = month + " months";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)absDays / 365));
                if (years < 2)
                    humanDate = titleCase ? "A year" : "a year";
                else
                    humanDate = years + " years";
            }

            if (ts.TotalSeconds < 0)
            {
                humanDate += " ago";
            }

            return humanDate;
        }
    }
}
