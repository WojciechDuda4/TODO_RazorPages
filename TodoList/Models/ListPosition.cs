using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class ListPosition
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Task { get; set; }
        public DateTime WriteStamp { get; set; }
    }
}
