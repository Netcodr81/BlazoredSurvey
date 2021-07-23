using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyManager.Contracts;
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
        public ISurveyManager SurveyManager { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }


        public async Task TakeRandomSurvey()
        {
            var result = await SurveyManager.GetRandomSurveyAsync();


            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo($"survey/{result.Value.SurveyId}");
            }
            else
            {
                ToastService.ShowError("Unable to get random survey at this time", "");
            }

        }

        public void ViewSurveys()
        {
            NavigationManager.NavigateTo("surveys");
        }

    }
}
