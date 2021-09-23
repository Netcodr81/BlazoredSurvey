using SurveyAccessor.Models;
using SurveyManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Utils
{
    public static class Mapper
    {
        public static SurveyDTO ToSurveyDTO(Survey survey)
        {

            var result = new SurveyDTO()
            {
                SurveyId = survey.SurveyId,
                SurveyName = survey.SurveyName,
                Description = survey.Description,
                SurveyQuestion = survey.SurveyQuestion,
                FeaturedSurvey = survey.FeaturedSurvey,
                TotalVotes = survey.TotalVotes,
                TotalTimesTaken = survey.TotalTimesTaken,
                CreatedOn = survey.CreatedOn,
                SurveyOptions = ToSurveyOptionList(survey.SurveyOptions)
            };

            return result;
        }


        public static SurveyOptionDTO ToSurveyOptionDTO(SurveyOption option)
        {
            var result = new SurveyOptionDTO()
            {
                SurveyOptionId = option.SurveyOptionId,
                Description = option.Description,
                ImagePath = option.ImagePath,
                Fk_SurveyId= option.Fk_SurveyId,
                TotalVotes = option.TotalVotes
            };

            return result;
        }

        public static List<SurveyOptionDTO> ToSurveyOptionList(ICollection<SurveyOption> options)
        {
            var result = new List<SurveyOptionDTO>();

            foreach (var option in options.ToList())
            {
                result.Add(ToSurveyOptionDTO(option));
            }

            return result;
        }

        public static Survey FromSurveyDTO(SurveyDTO survey)
        {
            var result = new Survey()
            {
                SurveyId = survey.SurveyId,
                SurveyName = survey.SurveyName,
                Description = survey.Description,
                SurveyQuestion = survey.SurveyQuestion,
                FeaturedSurvey = survey.FeaturedSurvey,
                TotalVotes = survey.TotalVotes,                
                TotalTimesTaken = survey.TotalTimesTaken,
                CreatedOn = survey.CreatedOn,
                SurveyOptions = FromSurveyOptionList(survey.SurveyOptions)
            };

            return result;
        }


        public static SurveyOption FromSurveyOptionDTO(SurveyOptionDTO option)
        {
            var result = new SurveyOption()
            {
                SurveyOptionId = option.SurveyOptionId,
                Description = option.Description,
                ImagePath = option.ImagePath,
                Fk_SurveyId = option.Fk_SurveyId,
                TotalVotes = option.TotalVotes
            };

            return result;
        }

        public static List<SurveyOption> FromSurveyOptionList(List<SurveyOptionDTO> options)
        {
            var result = new List<SurveyOption>();

            foreach (var option in options.ToList())
            {
                result.Add(FromSurveyOptionDTO(option));
            }

            return result;
        }

        public static List<SurveyDTO> ToSurveyDTOList(ICollection<Survey> surveys)
        {
            var result = new List<SurveyDTO>();

            foreach (var survey in surveys)
            {
                result.Add(ToSurveyDTO(survey));
            }

            return result;
        }
       
    }
}
