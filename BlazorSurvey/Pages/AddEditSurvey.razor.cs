using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.Pages
{
    public partial class AddEditSurvey : ComponentBase
    {

        [Parameter]
        public int Id { get; set; }

    }
}
