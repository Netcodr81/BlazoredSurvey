using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyAccessor.Models;

namespace BlazorSurvey.Utils.CustomValidation
{
    public class RequiredNumberOfItemsAttribute : ValidationAttribute
    {
        public int RequiredNumberOfRecords { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            

            var numberOfItemsInList = ((List<SurveyOption>)value).Count();

            if (numberOfItemsInList < RequiredNumberOfRecords)
            {
                ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? $"{validationContext.MemberName} must have a minimum of {RequiredNumberOfRecords} items" : ErrorMessage;
                
                return new ValidationResult(ErrorMessage, new []{validationContext.MemberName});
             
            }

            return ValidationResult.Success;
        }
    }
}
