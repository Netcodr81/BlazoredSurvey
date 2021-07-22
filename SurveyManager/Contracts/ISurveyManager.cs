using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Result;
using SurveyManager.DTO;

namespace SurveyManager.Contracts
{
    public interface ISurveyManager
    {
        Result<SurveyDTO> GetSurvey(int id);
        Task<Result<SurveyDTO>> GetSurveyAsync(int id);
        Result<List<SurveyDTO>> GetAllSurveys();
        Task<Result<List<SurveyDTO>>> GetAllSurveysAsync();
        Result<bool> DeleteSurvey(int id);
        Task<Result<bool>> DeleteSurveyAsync(int id);
        Result<SurveyDTO> GetRandomSurvey();
        Task<Result<SurveyDTO>> GetRandomSurveyAsync();
        Result<bool> AddSurvey(SurveyDTO survey);
        Task<Result<bool>> AddSurveyAsync(SurveyDTO survey);
        Result<bool> UpdateSurvey(SurveyDTO survey);
        Task<Result<bool>> UpdateSurveyAsync(SurveyDTO survey);
    }
}