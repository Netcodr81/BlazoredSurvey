using Microsoft.AspNetCore.Components;

namespace BlazorSurvey.Pages
{
    public partial class SurveyResults : ComponentBase
    {

        [Parameter]
        public int SurveyId { get; set; }

    }

}

