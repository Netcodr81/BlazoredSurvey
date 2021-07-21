using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorSurvey.Pages
{
    public partial class SurveyResults : ComponentBase
    {

        [Parameter]
        public int SurveyId { get; set; }

    }

}

