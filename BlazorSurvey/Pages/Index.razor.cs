using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Pages
{
    public partial class Index : ComponentBase
    {

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }


        public async Task TakeRandomSurvey()
        {
            Random rnd = new Random();
            var surveyList = await Context.Surveys.ToListAsync();

            var randomSurvey = surveyList.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault();

            if (randomSurvey != null)
            {
                NavigationManager.NavigateTo($"survey/{randomSurvey.SurveyId}");
            }
           
        }

        public void ViewSurveys()
        {
            NavigationManager.NavigateTo("surveys");            
        }

    }
}
