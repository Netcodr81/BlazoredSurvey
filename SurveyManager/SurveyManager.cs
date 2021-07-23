using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using SurveyAccessor.Context;
using SurveyAccessor.Models;
using SurveyManager.Contracts;
using SurveyManager.DTO;
using SurveyManager.Utils;

namespace SurveyManager
{
    public class SurveyManager : ISurveyManager
    {
        SurveysDbContext _context;
        
        public SurveyManager(SurveysDbContext context)
        {
            _context = context;
        }
        public Result<SurveyDTO> GetSurvey(int id)
        {
            try
            {
                var survey = _context.Surveys.Where(x => x.SurveyId == id).Include(x => x.SurveyOptions).FirstOrDefault();

                if (survey == null)
                {
                    return  Result<SurveyDTO>.NotFound();
                }

                var result = Mapper.ToSurveyDTO(survey);

                return Result<SurveyDTO>.Success(result);
            }
            catch (Exception)
            {

                return Result<SurveyDTO>.Error("An error occurred while getting the survey");
            } 
           
        }

        public async Task<Result<SurveyDTO>> GetSurveyAsync(int id)
        {
            try
            {
                var survey = await _context.Surveys.Where(x => x.SurveyId == id).Include(x => x.SurveyOptions).FirstOrDefaultAsync();

                if (survey == null)
                {
                    return Result<SurveyDTO>.NotFound();
                }

                var result = Mapper.ToSurveyDTO(survey);

                return Result<SurveyDTO>.Success(result);
            }
            catch (Exception)
            {

                return Result<SurveyDTO>.Error("An error occurred while getting the survey");
            }
        }

        public Result<List<SurveyDTO>> GetAllSurveys()
        {
            try
            {
                var surveys = _context.Surveys.Include(x => x.SurveyOptions).ToList();

                var result = Mapper.ToSurveyDTOList(surveys);

                return Result<List<SurveyDTO>>.Success(result);
            }
            catch (Exception)
            {

                return Result<List<SurveyDTO>>.Error("An error occurred while getting the list of surveys");
            }
        }

        public async Task<Result<List<SurveyDTO>>> GetAllSurveysAsync()
        {
            try
            {
                var surveys = await _context.Surveys.Include(x => x.SurveyOptions).ToListAsync();

                var result = Mapper.ToSurveyDTOList(surveys);

                return Result<List<SurveyDTO>>.Success(result);
            }
            catch (Exception)
            {

                return Result<List<SurveyDTO>>.Error("An error occurred while getting the list of surveys");
            }
        }

        public Result<bool> DeleteSurvey(int id)
        {
            try
            {
                var survey = _context.Surveys.Where(x => x.SurveyId == id).Include(x => x.SurveyOptions).FirstOrDefault();

                if (survey == null)
                {
                    return Result<bool>.NotFound();
                }

                _context.Attach(survey);
                _context.Entry(survey).State = EntityState.Deleted;

                _context.SaveChanges();

                return Result<bool>.Success(true);
            }
            catch (Exception)
            {

                return Result<bool>.Error("An error occurred. Survey wasn't deleted");
            }
           


        }

        public async Task<Result<bool>> DeleteSurveyAsync(int id)
        {
            try
            {
                var survey = await _context.Surveys.Where(x => x.SurveyId == id).Include(x => x.SurveyOptions).FirstOrDefaultAsync();

                if (survey == null)
                {
                    return Result<bool>.NotFound();
                }

                _context.Attach(survey);
                _context.Entry(survey).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception)
            {

                return Result<bool>.Error("An error occurred. Survey wasn't deleted");
            }
        }

        public Result<SurveyDTO> GetRandomSurvey()
        {
            try
            {
                Random rnd = new Random();
                var result = new SurveyDTO();
                var surveys = _context.Surveys.Where(x => x.FeaturedSurvey == true).Include(x => x.SurveyOptions).ToList();

                result = Mapper.ToSurveyDTO(surveys.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault());

                return Result<SurveyDTO>.Success(result);
            }
            catch (Exception)
            {

                return Result<SurveyDTO>.Error("An error occurred while trying to get a random survey");
            }
        }

        public async Task<Result<SurveyDTO>> GetRandomSurveyAsync()
        {
            try
            {
                Random rnd = new Random();
                var result = new SurveyDTO();
                var surveys = await _context.Surveys.Where(x => x.FeaturedSurvey == true).Include(x => x.SurveyOptions).ToListAsync();

                result = Mapper.ToSurveyDTO(surveys.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault());

                return Result<SurveyDTO>.Success(result);
            }
            catch (Exception)
            {

                return Result<SurveyDTO>.Error("An error occurred while trying to get a random survey");
            }
        }

        public Result<SurveyDTO> AddSurvey(SurveyDTO survey)
        {
            try
            {
                var surveyToAdd = Mapper.FromSurveyDTO(survey);
                survey.CreatedOn = DateTime.Now;
                _context.Surveys.Add(surveyToAdd);
                _context.SaveChanges();

                survey = Mapper.ToSurveyDTO(surveyToAdd);

                return Result<SurveyDTO>.Success(survey);
            }
            catch (Exception)
            {

                return Result<SurveyDTO>.Error("An error occurred when adding the survey");
            }
        }

        public async Task<Result<SurveyDTO>> AddSurveyAsync(SurveyDTO survey)
        {
            try
            {
                var surveyToAdd = Mapper.FromSurveyDTO(survey);
                survey.CreatedOn = DateTime.Now;
               await _context.Surveys.AddAsync(surveyToAdd);
               await _context.SaveChangesAsync();

                survey = Mapper.ToSurveyDTO(surveyToAdd);

                return Result<SurveyDTO>.Success(survey);
            }
            catch (Exception)
            {

                return Result<SurveyDTO>.Error("An error occurred when adding the survey");
            }
        }

        public Result<bool> UpdateSurvey(SurveyDTO survey)
        {
            try
            {
                var surveyToUpdate = Mapper.FromSurveyDTO(survey);
                _context.Attach(surveyToUpdate);
                _context.Entry(surveyToUpdate).State = EntityState.Modified;

                _context.SaveChanges();

                return Result<bool>.Success(true);
            }
            catch (Exception)
            {

                return Result<bool>.Error("An error occurred while updating the survey");
            }
        }

        public async Task<Result<bool>> UpdateSurveyAsync(SurveyDTO survey)
        {
            try
            {
                var surveyToUpdate = Mapper.FromSurveyDTO(survey);
                _context.Attach(surveyToUpdate);
                _context.Entry(surveyToUpdate).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception)
            {

                return Result<bool>.Error("An error occurred while updating the survey");
            }
        }     
    }
}
