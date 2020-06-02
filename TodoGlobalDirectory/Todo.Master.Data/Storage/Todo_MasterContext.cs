using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Master.Data.Entities;

namespace Todo.Master.Data.Storage
{
    public partial class Todo_MasterContext : DbContext
    {
        public Todo_MasterContext(DbContextOptions<Todo_MasterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Task>()
                .HasIndex(u => u.Title)
                .IsUnique();
        }
    }
}
