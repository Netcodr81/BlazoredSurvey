using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveyAccessor.Models;

namespace BlazorSurvey.Utils.CustomValidation
{
    public class RequiredNumberOfSelectItemsAttribute : ValidationAttribute
    {
        public int RequiredNumberOfRecords { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {


            var numberOfItemsInList = ((List<SelectListItem>)value).Count();

            if (numberOfItemsInList < RequiredNumberOfRecords)
            {
                ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? $"{validationContext.MemberName} must have a minimum of {RequiredNumberOfRecords} items" : ErrorMessage;

                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });

            }

            return ValidationResult.Success;
        }
    }
}
