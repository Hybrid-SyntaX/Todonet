using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.net.Core.Models
{
    public class TodoTask
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime? DueDate {get;set;}
        public DateTime? CompletionDate {get;set;}
        public DateTime CreationDate {get;set;}
        public DateTime LastUpdated {get;set;}
    }
}
