using SurveyManager.Contracts;

namespace SurveyManager.DTO
{
   public class SurveyOptionDTO : ISurveyOptionDTO
   {
        public int SurveyOptionId { get; set; }
        public int Fk_SurveyId { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int TotalVotes { get; set; }

       
    }
}
