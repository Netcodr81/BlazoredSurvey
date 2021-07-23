using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyManager.Contracts;
using SurveyManager.DTO;
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

        [Inject]
        ISurveyManager SurveyManager { get; set; } 

        private ISurveyDTO Survey = new SurveyDTO();

        private bool isReady = false;

        [Parameter]
        public int SurveyId { get; set; }


        private async Task TakeSurvey(int surveyId)
        {
            NavigationManager.NavigateTo($"survey/{surveyId}");
        }

        protected override async Task OnInitializedAsync()
        {
            if (SurveyId != 0)
            {
              
                var result = await SurveyManager.GetSurveyAsync(SurveyId);

                if (result.IsSuccess)
                {
                    Survey = result.Value;
                }
            }
            else
            {
               
                var result = await SurveyManager.GetRandomSurveyAsync();

                if (result.IsSuccess)
                {
                    Survey = result.Value;
                }
               
            }


            isReady = true;
        }       

    }
}
