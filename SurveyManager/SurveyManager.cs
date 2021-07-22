using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using SurveyManager.Contracts;
using SurveyManager.DTO;

namespace SurveyManager
{
    public class SurveyManager : ISurveyManager
    {
        public Result<SurveyDTO> GetSurvey(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<SurveyDTO>> GetSurveyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Result<List<SurveyDTO>> GetAllSurveys()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<SurveyDTO>>> GetAllSurveysAsync()
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeleteSurvey(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> DeleteSurveyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Result<SurveyDTO> GetRandomSurvey()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<SurveyDTO>> GetRandomSurveyAsync()
        {
            throw new NotImplementedException();
        }

        public Result<bool> AddSurvey(SurveyDTO survey)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> AddSurveyAsync(SurveyDTO survey)
        {
            throw new NotImplementedException();
        }

        public Result<bool> UpdateSurvey(SurveyDTO survey)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> UpdateSurveyAsync(SurveyDTO survey)
        {
            throw new NotImplementedException();
        }
    }
}
