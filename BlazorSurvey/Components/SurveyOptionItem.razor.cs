using Microsoft.AspNetCore.Components;
using SurveyAccessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Components
{
    public partial class SurveyOptionItem : ComponentBase 
    {
              
        [Parameter]
        public SurveyOption Item { get; set; }

        [Parameter]
        public int TotalSurveyVotes { get; set; }

        private string imageSrc { get; set; }

        private int totalPercentage { get; set; }

        protected override void OnInitialized()
        {
            imageSrc = $"images/{Item.ImagePath}";
            double calculatedPercentage = 0;
            if (Item.TotalVotes != 0)
            {
                calculatedPercentage = ((double)Item.TotalVotes / TotalSurveyVotes) * 100;
            }

            totalPercentage = (int)(Math.Round(calculatedPercentage));
        }
    }
}

