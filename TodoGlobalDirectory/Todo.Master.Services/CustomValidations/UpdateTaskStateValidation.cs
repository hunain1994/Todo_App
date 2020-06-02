using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Todo.Master.Services.Dtos;

namespace Todo.Master.Services.CustomValidations
{
    public class UpdateTaskStateValidation: ValidationAttribute
    {
        public UpdateTaskStateValidation()
        {

        }
        public string GetErrorMessage() => "State of task must be Todo, In Progress or Complete.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            //var todoItem = (TaskUpdateViewModel)validationContext.ObjectInstance;
            var state = ((string)value);

            if (state != "Todo" && state != "In Progress" && state != "Complete")
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
