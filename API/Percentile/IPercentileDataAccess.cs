using System;
using System.Collections.Generic;

namespace API
{
    public interface IPercentileDataAccess
    {
        List<double> GetPercentileData();
    }
}
