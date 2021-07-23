using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SurveyAccessor.Context;
using SurveyManager.Contracts;
using SurveyManager.DTO;
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

        [Inject]
        public ISurveyManager SurveyManager { get; set; }

        private EditContext editContext { get; set; }

        private OptionViewModel optionModel { get; set; } = new OptionViewModel();
        private SurveyOptionDTO model = new SurveyOptionDTO();

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

            await Modal.CloseAsync(ModalResult.Ok<SurveyOptionDTO>(model));
        }

        void Cancel() => Modal.CancelAsync();
    
}
}
