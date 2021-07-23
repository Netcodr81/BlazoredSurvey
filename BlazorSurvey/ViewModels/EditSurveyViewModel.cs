using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorSurvey.Utils.CustomValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveyAccessor.Context;
using SurveyManager.DTO;

namespace BlazorSurvey.ViewModels
{
    public class EditSurveyViewModel
    {
        public EditSurveyViewModel(SurveysDbContext context)
        {
            Context = context;
        }
        public int SurveyId { get; set; }

        [Required(ErrorMessage="Survey name is required")]
        public string SurveyName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Survey question is required")]
        [MaxLength(255, ErrorMessage = "Survey question is too long")]
        public string SurveyQuestion { get; set; }     

        public bool FeaturedSurvey { get; set; }

        [RequiredNumberOfSelectItems(RequiredNumberOfRecords = 2, ErrorMessage = "At least 2 options are required")]
        public List<SelectListItem> SurveyOptions { get; set; } = new List<SelectListItem>();
      
        public List<SurveyOptionDTO> SurveyOptionsToAdd { get; set; } = new List<SurveyOptionDTO>();
      
        public SurveysDbContext Context { get; }

        public void AddSurveyOption(SurveyOptionDTO option)
        {
            SelectListItem optionToAdd = new SelectListItem {Selected = false, Text = option.Description, Value = option.SurveyOptionId.ToString()};
            SurveyOptions.Add(optionToAdd);
            SurveyOptionsToAdd.Add(new SurveyOptionDTO()
            {
                Fk_SurveyId = SurveyId,
                Description = option.Description,
                ImagePath = option.ImagePath,
                TotalVotes = 0
            });
          
        }

        public async Task RemoveSurveyOption(int optionId)
        {
           
            string description = SurveyOptions.FirstOrDefault(x => x.Value == optionId.ToString()).Text;

            SurveyOptions.Remove(SurveyOptions.FirstOrDefault(x => x.Value == optionId.ToString()));

            if (SurveyOptionsToAdd.Any(x => x.SurveyOptionId == optionId))
            {
                SurveyOptionsToAdd.Remove(SurveyOptionsToAdd.FirstOrDefault(x => x.SurveyOptionId == optionId));
            }
            else
            {
                SurveyOptionsToAdd.Remove(SurveyOptionsToAdd.FirstOrDefault(x => x.Description == description && x.SurveyOptionId == 0));
            }         
         
        }
    }
}
