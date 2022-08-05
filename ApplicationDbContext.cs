using System;
using System.Data.Entity;
using System.Linq;
using Diary.Models.Configurations;
using Diary.Models.Domains;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using Diary.Properties;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            :base($@"Server={Settings.Default.ServerAddress}\{Settings.Default.ServerName};Database={Settings.Default.DatabaseName};User Id={Settings.Default.UserName}; Password={Settings.Default.Password};")
        {
        }

        public ApplicationDbContext(string connection)
            :base(connection)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Raiting> Raitings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new RaitingConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
        }

    }

}