using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAccessor.Models
{
    [Table("Surveys")]
    public class Survey
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Description { get; set; }

        public string SurveyQuestion { get; set; }

        public int TotalVotes { get; set; }

        public bool FeaturedSurvey { get; set; }

        public int TotalTimesTaken { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<SurveyOption> SurveyOptions { get; set; }
    }
}
