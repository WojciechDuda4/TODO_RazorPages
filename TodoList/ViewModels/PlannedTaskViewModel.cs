using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
    public class PlannedTaskViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime WriteStamp { get; set; }
    }
}
