using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
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

        [CascadingParameter]
        IModalService Modal { get; set; }

        private static int SelectedOption { get; set; } = 0;

        private AddSurveyViewModel model = new AddSurveyViewModel();

        private async Task SaveSurvey()
        {
            await JSRuntime.InvokeVoidAsync("alert", "Survey Saved");
            NavigationManager.NavigateTo("surveylist/edit");
        }

        private void CancelAdd()
        {
            NavigationManager.NavigateTo("/");
        }

        private void UpdateOptionSelection(ChangeEventArgs e)
        {
            var selectedValue = e.Value;

            if (!string.IsNullOrWhiteSpace(selectedValue.ToString()))
            {
                model.ShowDeleteOption = true;
                SelectedOption = Int32.Parse(selectedValue.ToString());
            }
            else
            {
                model.ShowDeleteOption = false;
                SelectedOption = 0;
            }
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
                SelectedOption = 0;
                await JSRuntime.InvokeVoidAsync("selectList.SetSelectedItem", "surveyOptions", "");


            }
        }

        private async Task AddOption()
        {
            var maxId = model.SurveyOptions.Count == 1 ? 1 : Int32.Parse(model.SurveyOptions.OrderByDescending(x => x.Value).FirstOrDefault().Value);
            
            var formModal = Modal.Show<NewSurveyOption>("Add an Option");

            var result = await formModal.Result;

            if (!result.Cancelled)
            {
                var results = result.Data;
                model.AddSurveyOption((SurveyOption)result.Data, maxId);
                model.SurveyOptionsToAdd.Add((SurveyOption)result.Data);
                model.SurveyOptions.First().Text = "Please select an option...";
                await JSRuntime.InvokeVoidAsync("selectList.SetSelectedItem", "surveyOptions", "");
            }
        }

    }

}

