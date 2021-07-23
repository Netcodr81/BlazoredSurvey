namespace SurveyManager.Contracts
{
    public interface ISurveyOptionDTO
    {
        int SurveyOptionId { get; set; }
        int Fk_SurveyId { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
        int TotalVotes { get; set; }
    }
}