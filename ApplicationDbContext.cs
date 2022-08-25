using System.Data.Entity;
using Diary.Models.Configurations;
using Diary.Models.Domains;
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

        public bool CheckConnectionToDatabase(string serverAdress, string serverName, string databaseName, string userName, string password)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext($@"Server={serverAdress}\{serverName};Database={databaseName};User Id={userName}; Password={password};"))
            {
                return dbContext.Database.Exists();
                
            }

        }

    }

}