using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorSurvey.Components.Modals
{
    public partial class Confirm : ComponentBase
    {

        [CascadingParameter]
        BlazoredModalInstance Modal { get; set; }

        [Parameter]
        public int SurveyOptionId { get; set; }

        [Parameter]
        public string Message { get; set; }

        void Accept() => Modal.CloseAsync(ModalResult.Ok(SurveyOptionId));
        void Cancel() => Modal.CancelAsync();


    }
}
