using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyAccessor.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Components
{
    public partial class FeaturedSurvey : ComponentBase
    {


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }

        private Survey Survey = new Survey();

        private bool isReady = false;


        private async Task TakeSurvey(int surveyId)
        {
            NavigationManager.NavigateTo($"survey/{surveyId}");
        }

        protected override async Task OnInitializedAsync()
        {
            Random rnd = new Random();
            var featuredSurveys = await Context.Surveys.Where(x => x.FeaturedSurvey == true).Include(x => x.SurveyOptions).ToListAsync();

            Survey = featuredSurveys.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault();

            isReady = true;
        }

    }
}
