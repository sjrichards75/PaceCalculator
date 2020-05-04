using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace PaceCalculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            string[] DUnits = { "Miles", "KM" };

            DistanceUnits = new SelectList(DUnits);
        }

        [BindProperty(SupportsGet = true)]
        public float Distance { get; set; }
        [BindProperty(SupportsGet = true), DataType(DataType.Time), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm:ss}")]
        public DateTime GoalTime { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Units { get; set; }

        [DataType(DataType.Time), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString="{0:H:mm:ss}")]
        public string TargetPace { get; set; }
        public string TargetUnits { get; set; }

        public SelectList DistanceUnits { get; }
           
        public void OnGet()
        {
            if(!ModelState.IsValid)
            {
                // return Page();
                return;
            }

            if (Distance > 0 && GoalTime.TimeOfDay.TotalSeconds > 0)
            {
                TimeSpan ts = TimeSpan.FromSeconds(GoalTime.TimeOfDay.TotalSeconds / Distance);
                TargetPace = ts.ToString(@"hh\:mm\:ss");
                if (Units == "KM")
                {
                    TargetUnits = "KM";
                }
                else
                {
                    TargetUnits = "mile";
                }
            }
            else
            {
                TargetPace = "<calc>";
                TargetUnits = "<calc>";
            }
        }
    }
}
