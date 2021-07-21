using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyAccessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Components
{
    public partial class MostPopularSurvey : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }

        private int? TotalTimesTaken { get; set; }

        private string MostPopularSurveyName { get; set; }

        private Survey Survey { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            Survey = await Context.Surveys.OrderByDescending(x => x.TotalTimesTaken).FirstOrDefaultAsync();
        }
    }
}
