using Blazored.Toast.Services;
using BlazorSurvey.Utils;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyManager.Contracts;
using SurveyManager.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Pages
{
    public partial class Survey : ComponentBase
    {


        [Parameter]
        public int Id { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public Mapper mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISurveyManager SurveyManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        private SurveyViewModel SurveyVM = new SurveyViewModel();

        private SurveyDTO survey;

        private bool isReady = false;

        protected override async Task OnInitializedAsync()
        {
            var result = await SurveyManager.GetSurveyAsync(Id);

            if (result.IsSuccess)
            {
                survey = result.Value;
                SurveyVM = mapper.SurveyToSurveyViewModel(survey);
            }         

            isReady = true;
        }

        private async Task SubmitSurvey()
        {
            var model = SurveyVM;

            SurveyVM.SurveyTaken();
            SurveyVM.TallyVote();

            survey.TotalTimesTaken = SurveyVM.TotalTimesTaken;
            survey.SurveyOptions = SurveyVM.SurveyOptions;
            survey.TotalVotes = SurveyVM.TotalVotes;

            var result = await SurveyManager.UpdateSurveyAsync(survey);

            if (result.IsSuccess)
            {
                ToastService.ShowInfo("Thank you for taking the survey", "Thank You");

                NavigationManager.NavigateTo("/");
            }
            else
            {
                ToastService.ShowError("An Error occurred", "");
            }        

        }

        private void UpdateSelectedValue(string selectedOptionId)
        {
            SurveyVM.SelectedOption = selectedOptionId;
        }


    }
}
