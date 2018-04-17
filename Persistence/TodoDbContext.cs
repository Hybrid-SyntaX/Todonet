using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.net.Core.Models;

namespace Todo.net.Persistence
{
    public class TodoDbContext:DbContext
    {
        public DbSet<TodoTask> TodoTasks { set; get; }

        public TodoDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
