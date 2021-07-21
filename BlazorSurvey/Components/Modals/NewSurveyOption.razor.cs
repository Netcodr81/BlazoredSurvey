using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveyAccessor.Context;
using SurveyAccessor.Models;

namespace BlazorSurvey.Components.Modals
{
    public partial class NewSurveyOption : ComponentBase
    {
        [CascadingParameter]
        BlazoredModalInstance Modal { get; set; }

    
        private EditContext editContext { get; set; }

        private OptionViewModel optionModel { get; set; } = new OptionViewModel();
        private SurveyOption model = new SurveyOption();

        protected override void OnInitialized()
        {
            editContext = new EditContext(optionModel);
        }

        private Task SaveOption()
        {
           
            model.Description = optionModel.Description;
            model.ImagePath = optionModel.ImagePath;
            model.TotalVotes = 0;

            return Modal.CloseAsync(ModalResult.Ok<SurveyOption>(model));
        }

        void Cancel() => Modal.CancelAsync();
    }
}
