using System;
using System.Collections.Generic;
using SurveyManager.DTO;

namespace SurveyManager.Contracts
{
    public interface ISurveyDTO
    {
        int SurveyId { get; set; }
        string SurveyName { get; set; }
        string Description { get; set; }
        string SurveyQuestion { get; set; }
        int TotalVotes { get; set; }
        bool FeaturedSurvey { get; set; }
        int TotalTimesTaken { get; set; }
        DateTime CreatedOn { get; set; }
        List<SurveyDTO> SurveyOptions { get; set; }
    }
}