using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.Components.Modals;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using SurveyManager.Contracts;
using SurveyManager.DTO;

namespace BlazorSurvey.Pages
{
    public partial class AdminSurveyList : ComponentBase
    {

        [Inject]
        public SurveysDbContext Context { get; set; }

        [CascadingParameter]
        IModalService Modal { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ISurveyManager SurveyManager { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<SurveyDTO> SurveyList { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

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

        private void EditSurvey(int id)
        {
            NavigationManager.NavigateTo($"survey/edit/{id}");
        }

        private async Task DeleteSurvey(int id)
        {
            ModalParameters parameters = new ModalParameters();
            parameters.Add("Message", "Are you sure you want to delete this survey?");
            var formModal = Modal.Show<Confirm>("Delete Survey", parameters);

            var result = await formModal.Result;

            if (!result.Cancelled)
            {

            
                var deleteResult = await SurveyManager.DeleteSurveyAsync(id);

                if (deleteResult.IsSuccess)
                {
                   
                    var newSurveyList = await SurveyManager.GetAllSurveysAsync();

                    if (newSurveyList.IsSuccess)
                    {
                        SurveyList = newSurveyList.Value;
                        ToastService.ShowSuccess("", "Survey Deleted");
                    }
                    else
                    {
                        ToastService.ShowError("", "An error occurred while deleting this survey");
                    }
                
                }
            }


            NavigationManager.NavigateTo($"surveylist/edit");
        }

    }
}
