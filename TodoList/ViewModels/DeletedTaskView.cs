using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
    public class DeletedTaskView
    {
        [Required]
        public string Description { get; set; }

        public DateTime DeletionStamp { get; set; }

    }
}
