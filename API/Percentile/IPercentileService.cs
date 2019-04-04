using System;
using System.Collections.Generic;

namespace API
{
    public interface IPercentileService
    {
        double CalculatePercentile(double percentile, List<double> values);
    }
}
