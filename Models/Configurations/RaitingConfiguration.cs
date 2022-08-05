using Diary.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Configurations
{
    class RaitingConfiguration : EntityTypeConfiguration<Raiting>
    {

        public RaitingConfiguration()
        {
            ToTable("dbo.Ratings");

            HasKey(x => x.Id);
        }
    }
}
