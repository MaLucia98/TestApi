using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerSubject> PartnerSubject { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<PartnerTypes> PartnerTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<PartnerSubject>(entity =>
            {

                entity.HasOne(d => d.Student)
                  .WithMany(p => p.PartnerSubject)
                  .HasForeignKey(d => d.StudentId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_PartnerSubject_Restrict_Student");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentsSubject)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PartnerSubject_Restrict_Subject");
            });

            modelBuilder.Entity<PartnerTypes>().HasData(
                 new PartnerTypes { Id = 1, Name = "Estudiante" },
                 new PartnerTypes { Id = 2, Name = "Profesor"});

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Física", Credits = 3, TeacherId = 1},
                new Subject { Id = 2, Name = "Matematicas", Credits = 3, TeacherId = 1 },
                new Subject { Id = 3, Name = "Filosofía", Credits = 3, TeacherId = 2 },
                new Subject { Id = 4, Name = "Psicología", Credits = 3, TeacherId = 2 },
                new Subject { Id = 5, Name = "Literatura", Credits = 3, TeacherId = 3 },
                new Subject { Id = 6, Name = "Idiomas Extranjeros", Credits = 3, TeacherId = 3 },
                new Subject { Id = 7, Name = "Biología", Credits = 3, TeacherId = 4 },
                new Subject { Id = 8, Name = "Química", Credits = 3, TeacherId = 4 },
                new Subject { Id = 9, Name = "Historia del Arte", Credits = 3, TeacherId = 5 },
                new Subject { Id = 10, Name = "Música", Credits = 3, TeacherId = 5 });

            modelBuilder.Entity<Partner>().HasData(
                new Partner { Id = 1, Name = "Profesor A", PartnerTypeId = (int)PartnerTypeEnum.Profesor },
                new Partner { Id = 2, Name = "Profesor B", PartnerTypeId = (int)PartnerTypeEnum.Profesor },
                new Partner { Id = 3, Name = "Profesor C", PartnerTypeId = (int)PartnerTypeEnum.Profesor },
                new Partner { Id = 4, Name = "Profesor D", PartnerTypeId = (int)PartnerTypeEnum.Profesor },
                new Partner { Id = 5, Name = "Profesor E", PartnerTypeId = (int)PartnerTypeEnum.Profesor });
     
        }
        
     }
}