using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Sample.Api.Models.Errors;

namespace Sample.Api.Models.Validations
{
    public class ValidationResultModel : GlobalErrorModel
    {
        //public string Message { get; } 
        
        [JsonProperty(Order = 3)]
        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity; 
            StatusText = "Unprocessable Entity";
            Message = "Validation error in " + modelState.ErrorCount.ToString() + " field(s)";
            Errors = modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList();
        }
    }
}