using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorSurvey.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SurveyManager.DTO;

namespace BlazorSurvey.Components.Modals
{
    public partial class NewSurveyOption : ComponentBase
    {
        [CascadingParameter]
        BlazoredModalInstance Modal { get; set; }

    
        private EditContext editContext { get; set; }

        private OptionViewModel optionModel { get; set; } = new OptionViewModel();
        private SurveyOptionDTO model = new SurveyOptionDTO();

        protected override void OnInitialized()
        {
            editContext = new EditContext(optionModel);
        }

        private Task SaveOption()
        {
           
            model.Description = optionModel.Description;
            model.ImagePath = optionModel.ImagePath;
            model.TotalVotes = 0;

            return Modal.CloseAsync(ModalResult.Ok<SurveyOptionDTO>(model));
        }

        void Cancel() => Modal.CancelAsync();
    }
}
