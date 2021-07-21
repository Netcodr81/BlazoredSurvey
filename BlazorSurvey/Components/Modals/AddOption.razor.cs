using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SurveyAccessor.Context;
using SurveyAccessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Components.Modals
{
    public partial class AddOption : ComponentBase
    {
      
    [CascadingParameter]
        BlazoredModalInstance Modal { get; set; }

        [Parameter]
        public int SurveyId { get; set; }

        [Inject]
        private SurveysDbContext Context { get; set; }

        private EditContext editContext { get; set; }

        private OptionViewModel optionModel { get; set; } = new OptionViewModel();
        private SurveyOption model = new SurveyOption();

        protected override void OnInitialized()
        {

            editContext = new EditContext(optionModel);
            optionModel.Fk_SurveyId = SurveyId;
        }

        private async Task SaveOption()
        {
            model.Fk_SurveyId = optionModel.Fk_SurveyId;
            model.Description = optionModel.Description;
            model.ImagePath = optionModel.ImagePath;
            model.TotalVotes = 0;

            try
            {
                Context.SurveyOptions.Add(model);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                var ex = e;
            }



            await Modal.CloseAsync(ModalResult.Ok<SurveyOption>(model));
        }

        void Cancel() => Modal.CancelAsync();
    
}
}
