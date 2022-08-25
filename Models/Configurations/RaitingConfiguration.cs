using Diary.Models.Domains;
using System.Data.Entity.ModelConfiguration;

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
