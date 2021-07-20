using SurveyAccessor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSurvey.ViewModels
{
    public class EditSurveyViewModel
    {
        public int SurveyId { get; set; }

        [Required(ErrorMessage="Survey name is required")]
        public string SurveyName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Survey question is required")]
        public string SurveyQuestion { get; set; }     

        public bool FeaturedSurvey { get; set; }

     

        public List<SurveyOption> SurveyOptions { get; set; }
    }
}
