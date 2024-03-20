using CMCallbackMicroService.Data.Entities;
using CMCallbackMicroService.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CMCallbackMicroService.Data.Context
{
    public class CMCallbackEFContext : DbContext
    {
        public CMCallbackEFContext(DbContextOptions<CMCallbackEFContext> options)
       : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        //For Data Seedings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder
                .UseSnakeCaseNamingConvention();

        //For Tables Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            //Rename Identity tables to lowercase
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetDefaultTableName();
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToLower());
            }
            modelBuilder.UseSerialColumns();

        }
        public DbSet<callbackrecord>? callbackrecord { get; set; }
    }
}
