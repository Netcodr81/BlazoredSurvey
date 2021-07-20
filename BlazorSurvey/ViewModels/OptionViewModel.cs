using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.ViewModels
{
    public class OptionViewModel
    {
        public int SurveyOptionId { get; set; }

        public int Fk_SurveyId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string ImagePath { get; set; } = "default.jpg";
        public int TotalVotes { get; set; } = 0;
    }
}
