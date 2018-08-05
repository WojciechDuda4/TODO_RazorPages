using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Enums;

namespace TodoList.DataModels
{
    public class TodoTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime WriteStamp { get; set; }

        public DateTime DeletionStamp { get; set; }

        public DateTime CompletionStamp { get; set; }

        public TodoTaskStatus Status { get;set; }

    }
}
