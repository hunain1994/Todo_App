using System.ComponentModel.DataAnnotations;
using Todo.Master.Services.CustomValidations;

namespace Todo.Master.Services.Dtos
{
    public class TaskUpdateViewModel
    {
        [Required(ErrorMessage = "State of task is required")]
        [UpdateTaskStateValidation]
        public string State { get; set; }
    }
}
