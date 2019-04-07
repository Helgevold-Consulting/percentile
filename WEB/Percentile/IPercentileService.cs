using System;
using System.Threading.Tasks;

namespace WEB
{
    public interface IPercentileService
    {
        Task<double> GetPercentile(double percentile);
    }
}
