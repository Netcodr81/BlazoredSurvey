using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyAccessor.Models
{
    [Table("SurveyOptions")]
    public class SurveyOption
    {
        public int SurveyOptionId { get; set; }

        public int Fk_SurveyId { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int TotalVotes { get; set; }

        public Survey Survey { get; set; }


    }
}
