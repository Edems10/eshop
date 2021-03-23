using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Validation
{
    public class UniqueCharactersAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int uniq;

        public UniqueCharactersAttribute(int uniq)
        {
            this.uniq = uniq;
        }



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is string temp)
            {
                if (temp.Distinct().Count()>=6)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErroMessage(), new List<string> { validationContext.MemberName });
                }

            }
            throw new NotImplementedException($"The attribute{nameof(FileContentTypeAttribute)} is not implemented for object {value.GetType()}. {nameof(IFormFile)} only is implemneted");
        }




        protected string GetErroMessage()
        {
            return $"Password needs to have at least 6 unique characters!";
        }

        public void AddValidation(ClientModelValidationContext context)
        {
           ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-password", GetErroMessage());
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-password-type", uniq.ToString());

        }


  
    }
}
