using System;
using System.Collections.Generic;

namespace API
{
    public class PercentileService : IPercentileService
    {
        public double CalculatePercentile(double percentile, List<double> values)
        {
            if(percentile < 0 || percentile > 1)
            {
                throw new ArgumentOutOfRangeException(string.Format("Percentile {0} is invalid. Value must be between 0 and 1", percentile));
            }

            values.Sort();

            var rank = percentile * (values.Count - 1) + 1;

            var integer = (int)Math.Truncate(rank);
            var fraction = rank - integer;

            if (integer == values.Count)
            {
                return values[values.Count - 1];
            }

            return values[integer - 1] + fraction * (values[integer] - values[integer - 1]);
        }
    }
}
