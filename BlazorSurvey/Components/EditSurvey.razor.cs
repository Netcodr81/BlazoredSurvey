using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.Components.Modals;
using BlazorSurvey.Utils;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using SurveyManager.DTO;
using SurveyManager.Contracts;

namespace BlazorSurvey.Components
{
    public partial class EditSurvey : ComponentBase
    {


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }

        [Inject]
        public Mapper mapper { get; set; }

        [CascadingParameter]
        IModalService Modal { get; set; }

        [Inject]
        public ISurveyManager SurveyManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [CascadingParameter]
        public int Id { get; set; }

        private bool isReady = false;

        private SurveyDTO Survey;

        private EditSurveyViewModel SurveyToUpdate { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await SurveyManager.GetSurveyAsync(Id);

            if (result.IsSuccess)
            {
                Survey = result.Value;
                SurveyToUpdate = mapper.SurveyToEditSurveyModel(Survey);
            }         
          

            isReady = true;
        }


        private async Task UpdateSurvey()
        {
            var updatedSurvey = SurveyToUpdate;

            Survey.SurveyName = SurveyToUpdate.SurveyName;
            Survey.SurveyQuestion = SurveyToUpdate.SurveyQuestion;
            Survey.FeaturedSurvey = SurveyToUpdate.FeaturedSurvey;
            Survey.Description = SurveyToUpdate.Description;
            Survey.SurveyOptions = SurveyToUpdate.SurveyOptionsToAdd;

            var result = await SurveyManager.UpdateSurveyAsync(Survey);

            if (result.IsSuccess)
            {
                ToastService.ShowSuccess("", "Survey Updated");
                NavigationManager.NavigateTo("surveylist/edit");
            }
            else
            {
                ToastService.ShowError("An error occurred while updating the survey", "Error");
            }
          
        }

        private void CancelUpdate()
        {
            NavigationManager.NavigateTo("surveylist/edit");
        }

        private async Task DeleteOption(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add("SurveyOptionId", id);
            parameters.Add("Message", "Are you sure you want to delete this option");
            var formModal = Modal.Show<Confirm>("Delete Option", parameters);
            var result = await formModal.Result;

            if (!result.Cancelled)
            {
                await SurveyToUpdate.RemoveSurveyOption(id);
            }
        }

        private async Task AddOption()
        {
            var parameters = new ModalParameters();
            parameters.Add("SurveyId", SurveyToUpdate.SurveyId);

            var formModal = Modal.Show<AddOption>("Add an Option", parameters);

            var result = await formModal.Result;

            if (!result.Cancelled)
            {
                var results = result.Data;
                SurveyToUpdate.AddSurveyOption((SurveyOptionDTO)result.Data);
            }
        }


    }
}
