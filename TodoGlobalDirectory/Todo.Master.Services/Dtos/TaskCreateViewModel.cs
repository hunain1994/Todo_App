using System.ComponentModel.DataAnnotations;
using Todo.Master.Services.CustomValidations;

namespace Todo.Master.Services.Dtos
{
    public class TaskCreateViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "State of task is required")]
        [CreateTaskStateValidation]
        public string State { get; set; } = "Todo";
    }
}
