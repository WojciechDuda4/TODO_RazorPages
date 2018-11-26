using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
    public class CompletedTaskViewModel : IArchiveViewModel
    {
        [Required]
        public string Description { get; set; }

        public DateTime WriteStamp { get; set; }
    }
}
