using SurveyManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlazorSurvey.ViewModels
{
    public class SurveyViewModel
    {
        public int SurveyId { get; set; }

        [Required(ErrorMessage = "Survey name is required")]
        public string SurveyName { get; set; }
        [Required(ErrorMessage ="Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Survey question is required")]
        public string SurveyQuestion { get; set; }

        public int TotalVotes { get; set; }

        public bool FeaturedSurvey { get; set; }        

        public int TotalTimesTaken { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required(ErrorMessage = "You must select an option")]
        public string SelectedOption { get; set; }

        public List<SurveyOptionDTO> SurveyOptions { get; set; } = new List<SurveyOptionDTO>();


        public void SurveyTaken()
        {
            TotalVotes += 1;
            TotalTimesTaken += 1;
        }

        public void TallyVote()
        {
            var selectedValue = Int32.Parse(SelectedOption);
            var optionSelected = SurveyOptions.Where(x => x.SurveyOptionId == selectedValue).FirstOrDefault();

            optionSelected.TotalVotes += 1;           
        }

        

        
    }
}
