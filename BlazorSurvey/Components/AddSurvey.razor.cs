using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using BlazorSurvey.Components.Modals;
using BlazorSurvey.Utils;
using BlazorSurvey.ViewModels;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyAccessor.Models;

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
        public Mapper mapper { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [CascadingParameter]
        IModalService Modal { get; set; }

        private AddSurveyViewModel model = new AddSurveyViewModel();

        private async Task SaveSurvey()
        {
            Context.Surveys.Add(model.GenerateSurveyToSave());

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                var exception = ex;
            }

            await JSRuntime.InvokeVoidAsync("alert", "Survey Saved");
            NavigationManager.NavigateTo("surveylist/edit");
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
                model.AddSurveyOption((SurveyOption)result.Data, maxId);
                model.SurveyOptionsToAdd.Add((SurveyOption)result.Data);

                ToastService.ShowSuccess("","Option Added");

            }
        }

    }

}

