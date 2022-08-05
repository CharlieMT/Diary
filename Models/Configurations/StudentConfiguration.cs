using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Diary.Models.Domains;

namespace Diary.Models.Configurations
{
    class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");

            HasKey(x => x.Id);

            Property(x => x.FirstName).HasMaxLength(100).IsRequired();

            Property(x => x.LastName).HasMaxLength(100).IsRequired();
        }
    }
}
