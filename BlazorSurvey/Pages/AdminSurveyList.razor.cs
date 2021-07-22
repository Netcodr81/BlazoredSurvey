using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.Components.Modals;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SurveyAccessor.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;

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
        public NavigationManager NavigationManager { get; set; }

        public List<SurveyAccessor.Models.Survey> SurveyList { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        private bool isReady = false;

        protected override async Task OnInitializedAsync()
        {
            SurveyList = await Context.Surveys.ToListAsync();
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
                var surveyToDelete = await Context.Surveys.Where(x => x.SurveyId == id).Include(x => x.SurveyOptions).FirstOrDefaultAsync();

                if (surveyToDelete != null)
                {
                    Context.Attach(surveyToDelete);
                    Context.Entry(surveyToDelete).State = EntityState.Deleted;

                    try
                    {
                        await Context.SaveChangesAsync();

                        SurveyList = await Context.Surveys.ToListAsync();
                        ToastService.ShowSuccess("", "Survey Deleted");
                    }
                    catch (Exception ex)
                    {

                        ToastService.ShowError("", "An error occurred while deleting this survey");
                    }
                }
            }


            NavigationManager.NavigateTo($"surveylist/edit");
        }

    }
}
