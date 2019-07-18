using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CounterpartProjects.Common
{
    public static class GeneralUtility
    {
        public static object CopyAllPropertiesFromTo(object from, object to, IList<string> exempt)
        {
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(from))
            {
                // do not copies properties that are in the 'exempt' array
                if (exempt.Contains(item.Name))
                    continue;
                else
                    item.SetValue(to, item.GetValue(from));
            }

            return to;
        }

        public static object CopyAllPropertiesFromToDiff(object from, object to, IList<string> exempt)
        {
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(from))
            {
                // do not copies properties that are in the 'exempt' array
                if (exempt.Contains(item.Name))
                    continue;
                else
                {
                    var srcValue = GetPropValue(from, item.Name);
                    SetPropValue(to, item.Name, srcValue);
                }
            }

            return to;
        }

        public static void ForEachWithIndex<T>(IList<T> list, Action<T, int> handler)
        {
            int idx = 0;
            foreach (T item in list)
                handler(item, idx++);
        }

        public static void ForEachWithIndex<T>(IEnumerable<T> enumerable, Action<T, int> handler)
        {
            int idx = 0;
            foreach (T item in enumerable)
                handler(item, idx++);
        }

        public static T Max<T>(T first, T second)
        {
            if (first == null && second == null)
                return default(T);
            else if (first == null)
                return second;
            else if (second == null)
                return first;
            else
            {
                if (Comparer<T>.Default.Compare(first, second) > 0)
                    return first;
                return second;
            }
        }

        public static T Min<T>(T first, T second)
        {
            if (first == null && second == null)
                return default(T);
            else if (first == null)
                return second;
            else if (second == null)
                return first;
            else
            {
                if (Comparer<T>.Default.Compare(first, second) < 0)
                    return first;
                return second;
            }
        }

        public static T Min<T>(IList<T> list)
        {
            T min = default(T);

            if (list.Count > 0)
                min = list[0];

            for (int i = 1, _max = list.Count; i < _max; i++)
                min = GeneralUtility.Min(min, list[i]);

            return min;
        }

        public static T Max<T>(IList<T> list)
        {
            T max = default(T);

            if (list.Count > 0)
                max = list[0];

            for (int i = 1, _max = list.Count; i < _max; i++)
                max = GeneralUtility.Max(max, list[i]);

            return max;
        }

        public static double Diff(DateTime d1, DateTime d2)
        {
            return Math.Ceiling((d1 - d2).TotalDays);
        }

        /// <summary>
        /// Number of months between two dates
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }

        /// <summary>
        /// Ready value of property of the object
        /// </summary>
        /// <param name="src">The source object</param>
        /// <param name="propName">Property name</param>
        /// <returns>It returns an object; needs to be type casted to correct object type</returns>
        public static object GetPropValue(object src, string propName)
        {
            var propertyInfo = src.GetType().GetProperty(propName);
            if (propertyInfo != null)
            {
                if (propertyInfo.GetGetMethod().IsStatic)
                    return propertyInfo.GetValue(null, null);
                else
                    return propertyInfo.GetValue(src, null);
            }

            return null;
        }

        public static decimal? GetSumOfPropValue<T>(List<T> src, string propName)
        {
            decimal? sum = null;

            foreach (var item in src)
                sum = GeneralUtility.AddTwoNumbers(sum, (decimal?)GeneralUtility.GetPropValue(item, propName));

            return sum;
        }

        /// <summary>
        /// Set value to property of the object
        /// </summary>
        /// <param name="src">The target object</param>
        /// <param name="propName">Property name</param>
        public static void SetPropValue(object tgt, string propName, object value)
        {
            var propertyInfo = tgt.GetType().GetProperty(propName);
            if (propertyInfo != null)
                propertyInfo.SetValue(tgt, GeneralUtility.ChangeType(value, propertyInfo.PropertyType), null);
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }

        public static decimal? AddTwoNumbers(decimal? num1, decimal? num2)
        {
            if (num1.HasValue && num2.HasValue)
                return num1 + num2;
            else if (num1.HasValue && !num2.HasValue)
                return num1;
            else if (!num1.HasValue && num2.HasValue)
                return num2;
            return null;
        }      

        public static object Map<T>(T value)
        {
            if (value.GetType() == typeof(bool))
            {
                if (value == null)
                    return false;
                else
                    Boolean.Parse(value.ToString());
            }

            return null;
        }

        public static T ConvertType<T>(string val)
        {
            Type destiny = typeof(T);

            // See if we can cast           
            try
            {
                return (T)(object)val;
            }
            catch { }

            // See if we can parse
            try
            {
                return (T)destiny.InvokeMember("Parse", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public, null, null, new object[] { val });
            }
            catch { }

            // See if we can convert
            try
            {
                Type convertType = typeof(Convert);
                return (T)convertType.InvokeMember("To" + destiny.Name, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public, null, null, new object[] { val });
            }
            catch { }

            // Give up
            return default(T);
        }

        /// <summary>
        /// Get number of business days between given dates; exclude Saturdays and Sundays
        /// </summary>
        /// <param name="start">From date</param>
        /// <param name="end">To date</param>
        /// <param name="includeStartDate">Number of working days: int</param>
        public static int GetNumberOfWorkingDays(DateTime? start, DateTime? end, bool includeStartDate)
        {
            if (start == null || end == null)
                return 0;

            DateTime _start = (DateTime)start;
            DateTime _end = (DateTime)end;

            // End Date cannot be lesser than Start Date
            if (_end.Date < _start.Date)
                return 0;

            int days = 0;

            // Loop through while StartDate is lesser than EndDate
            while (_start.Date <= _end.Date)
            {
                // Decrease days on weekends
                if (_start.DayOfWeek == DayOfWeek.Saturday || _start.DayOfWeek == DayOfWeek.Sunday)
                    days--;

                // Increase date on each loop
                days++;

                _start = _start.AddDays(1);
            }


            // Subtract 1 from total number of days if we do have to include Start Date in the calculation
            if (!includeStartDate)
                days = days > 0 ? days - 1 : days;

            return days;
        }

        /// <summary>
        /// Add number of business days to a given date; exclude Saturdays and Sundays
        /// </summary>
        /// <param name="start">From date</param>
        /// <param name="noOfDaysToBeAdded">Number of working days to be added: int</param>
        public static DateTime AddWorkingDaysToDate(DateTime start, int noOfDaysToBeAdded)
        {
            int days = 0;
            while (days < noOfDaysToBeAdded)
            {
                start = start.AddDays(1);
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                    days++;
            }
            return start;
        }

        /// <summary>
        /// If current day is a Working day then this function will 
        /// return the same date else it will return the next working day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetNextWorkingDate(this DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }

            return date;    
        }
    }
}
