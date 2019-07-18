using System;
using System.Collections.Generic;
using System.Text;

namespace CounterpartProjects.PlanningTool
{
    public class PlanTaskComparator<T> : Comparer<T> where T: IBasicPlanTask
    {
        public override int Compare(T x, T y)
        {
            var xParts = x.TaskId.Split(new[] { '.' });
            var yParts = y.TaskId.Split(new[] { '.' });

            int index = 0;
            while (true)
            {
                bool xHasValue = xParts.Length > index;
                bool yHasValue = yParts.Length > index;

                if (xHasValue && !yHasValue)
                    return 1;   // x bigger

                if (!xHasValue && yHasValue)
                    return -1;  // y bigger

                if (!xHasValue && !yHasValue)
                    return 0;   // no more values -- same

                var xValue = decimal.Parse(xParts[index]);
                var yValue = decimal.Parse(yParts[index]);

                if (xValue > yValue)
                    return 1;   // x bigger

                if (xValue < yValue)
                    return -1;  // y bigger
                index++;
            }
        }
    }
}
