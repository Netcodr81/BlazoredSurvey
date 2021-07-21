using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Components
{
    public partial class SurveyCount : ComponentBase
    {

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }

        private int? NumberOfSurveys { get; set; }



        protected override async Task OnParametersSetAsync()
        {
            var surveys = Context.Surveys.Select(x => x.SurveyId);
            NumberOfSurveys = await surveys.CountAsync();
        }

    }
}
