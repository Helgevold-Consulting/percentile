using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages
{
    public class IndexModel : PageModel
    {
        private IPercentileService _percentileService;
        public string percentileFormatted;
        public string errorMessage;

        public IndexModel(IPercentileService percentileService)
        {
            this._percentileService = percentileService;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var percentile =  await this._percentileService.GetPercentile(.995);
                this.percentileFormatted = string.Format("{0:F13}", percentile);
            }
            catch(Exception)
            {
                this.errorMessage = "There was an error";
                // TODO Log error
            }
            return Page();
        }
    }
}
