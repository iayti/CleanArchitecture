namespace Application.Common.Exceptions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public  class ValidateModelException : ModelStateDictionary
    {

        public ValidateModelException() 
        {
            Errors = new Dictionary<string, List<string>>();
        }


        public ValidateModelException(ModelStateDictionary modelState)
            : this()
        {
            foreach (string key in modelState.Keys)
            {
                var property = modelState.GetValueOrDefault(key);

                List<string> errors = property.Errors.Select(error => error.ErrorMessage).ToList();

                Errors.Add(key, errors);
            }
        }

        public IDictionary<string, List<string>> Errors { get; }
    }
}
