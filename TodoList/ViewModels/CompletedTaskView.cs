using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
    public class CompletedTaskView
    {
        [Required]
        public string Description { get; set; }

        public DateTime CompletionStamp { get; set; }

    }
}
