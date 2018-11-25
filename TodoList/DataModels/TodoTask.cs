using System;
using System.ComponentModel.DataAnnotations;
using TodoList.Enums;

namespace TodoList.DataModels
{
    public class TodoTask
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime WriteStamp { get; set; }

        public DateTime DeletionStamp { get; set; }

        public DateTime CompletionStamp { get; set; }

        public TodoTaskStatus Status { get; set; }
    }
}
