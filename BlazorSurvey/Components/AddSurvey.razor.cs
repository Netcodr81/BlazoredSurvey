using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using BlazorSurvey.Components.Modals;
using BlazorSurvey.Utils;
using BlazorSurvey.ViewModels;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyManager.Contracts;
using SurveyManager.DTO;

namespace BlazorSurvey.Components
{
    public partial class AddSurvey : ComponentBase
    {


        private bool isReady = true;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public SurveysDbContext Context { get; set; }

        [Inject]
        public ISurveyManager SurveyManager { get; set; }

        [Inject]
        public Mapper mapper { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [CascadingParameter]
        IModalService Modal { get; set; }

        private AddSurveyViewModel model = new AddSurveyViewModel();

        private async Task SaveSurvey()
        {
            var result = await SurveyManager.AddSurveyAsync(model.GenerateSurveyToSave());

            if (result.IsSuccess)
            {
                ToastService.ShowSuccess("Survey added successfully", "Success");
                NavigationManager.NavigateTo("surveylist/edit");
            }
            else
            {
                ToastService.ShowError("An error occurred while saving survey", "Error");
            }   

          
        }

        private void CancelAdd()
        {
            NavigationManager.NavigateTo("/");
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
                
                model.RemoveSurveyOption(id);
                ToastService.ShowSuccess("","Option Deleted");
            }
        }

        private async Task AddOption()
        {
            var maxId = model.GetMaxId();
            var formModal = Modal.Show<NewSurveyOption>("Add an Option");

            var result = await formModal.Result;

            if (!result.Cancelled)
            {
                var results = result.Data;
                model.AddSurveyOption((SurveyOptionDTO)result.Data, maxId);
                model.SurveyOptionsToAdd.Add((SurveyOptionDTO)result.Data);             

            }
        }

    }

}

