using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyManager.Contracts;
using SurveyManager.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorSurvey.Pages
{
    public partial class Surveys : ComponentBase
    {

        [Inject]
        public SurveysDbContext Context { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ISurveyManager SurveyManager { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<SurveyDTO> SurveyList { get; set; }

        private bool isReady = false;

        protected override async Task OnInitializedAsync()
        {
            var result = await SurveyManager.GetAllSurveysAsync();

            if (result.IsSuccess)
            {
                SurveyList = result.Value;
            }
            
            isReady = true;
        }

        private void TakeSurvey(int id)
        {
            NavigationManager.NavigateTo($"survey/{id}");
        }

        private void ViewSurveyResults(int id)
        {
            NavigationManager.NavigateTo($"survey/results/{id}");
        }

    }
}
