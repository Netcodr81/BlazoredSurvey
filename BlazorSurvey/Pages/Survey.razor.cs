using BlazorSurvey.Utils;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;

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
        public IToastService ToastService { get; set; }

        private SurveyViewModel SurveyVM = new SurveyViewModel();

        private SurveyAccessor.Models.Survey survey;

        private bool isReady = false;

        protected override async Task OnInitializedAsync()
        {
            survey = await Context.Surveys.Where(x => x.SurveyId == Id).Include(x => x.SurveyOptions).FirstOrDefaultAsync();


            SurveyVM = mapper.SurveyToSurveyViewModel(survey);

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

            Context.Attach(survey);
            Context.Entry(survey).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            ToastService.ShowInfo("Thank you for taking the survey", "Thank You");

            NavigationManager.NavigateTo("/");

        }

        private void UpdateSelectedValue(string selectedOptionId)
        {
            SurveyVM.SelectedOption = selectedOptionId;
        }


    }
}
