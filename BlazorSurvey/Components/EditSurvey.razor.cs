using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.Components.Modals;
using BlazorSurvey.Utils;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using SurveyAccessor.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        [CascadingParameter]
        public int Id { get; set; }

        private bool isReady = false;

        private SurveyAccessor.Models.Survey Survey;

        private EditSurveyViewModel SurveyToUpdate { get; set; }


        private bool ShowDeleteOption { get; set; } = false;

        private static int SelectedOption { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {

            Survey = await Context.Surveys.Where(x => x.SurveyId == Id).Include(x => x.SurveyOptions).FirstOrDefaultAsync();
            SurveyToUpdate = mapper.SurveyToEditSurveyModel(Survey);

            isReady = true;
        }


        private async Task UpdateSurvey()
        {
            var updatedSurvey = SurveyToUpdate;

            Survey.SurveyName = SurveyToUpdate.SurveyName;
            Survey.SurveyQuestion = SurveyToUpdate.SurveyQuestion;
            Survey.FeaturedSurvey = SurveyToUpdate.FeaturedSurvey;
            Survey.Description = SurveyToUpdate.Description;

            Context.Attach(Survey);
            Context.Entry(Survey).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            await JSRuntime.InvokeVoidAsync("alert", "Survey Updated");
        }

        private void CancelUpdate()
        {
            NavigationManager.NavigateTo("surveylist/edit");
        }

        private void UpdateOptionSelection(ChangeEventArgs e)
        {
            var selectedValue = e.Value;

            if (!string.IsNullOrWhiteSpace(selectedValue.ToString()))
            {
                SurveyToUpdate.ShowDeleteOption = true;
                SelectedOption = Int32.Parse(selectedValue.ToString());
            }
            else
            {
                SurveyToUpdate.ShowDeleteOption = false;
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
                SurveyToUpdate.RemoveSurveyOption(id);
                SelectedOption = 0;
                await JSRuntime.InvokeVoidAsync("selectList.SetSelectedItem", "surveyOptions", "");


            }
        }

        private async Task AddOption()
        {
            var maxId = SurveyToUpdate.SurveyOptions.Count == 0 ? 1 : Int32.Parse(SurveyToUpdate.SurveyOptions.OrderByDescending(x => x.Value).FirstOrDefault().Value);

            var parameters = new ModalParameters();
            parameters.Add("SurveyId", SurveyToUpdate.SurveyId);

            var formModal = Modal.Show<AddOption>("Add an Option", parameters);

            var result = await formModal.Result;

            if (!result.Cancelled)
            {
                var results = result.Data;
                SurveyToUpdate.AddSurveyOption((SurveyOption)result.Data);
                await JSRuntime.InvokeVoidAsync("selectList.SetSelectedItem", "surveyOptions", "");
            }
        }


    }
}
