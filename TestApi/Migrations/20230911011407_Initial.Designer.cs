﻿// <auto-generated />
using Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TestApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230911011407_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Entities.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartnerTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartnerTypeId");

                    b.ToTable("Partners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Profesor A",
                            PartnerTypeId = 2
                        },
                        new
                        {
                            Id = 2,
                            Name = "Profesor B",
                            PartnerTypeId = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Profesor C",
                            PartnerTypeId = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "Profesor D",
                            PartnerTypeId = 2
                        },
                        new
                        {
                            Id = 5,
                            Name = "Profesor E",
                            PartnerTypeId = 2
                        });
                });

            modelBuilder.Entity("Entities.Entities.PartnerSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("PartnerSubject");
                });

            modelBuilder.Entity("Entities.Entities.PartnerTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PartnerTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Estudiante"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Profesor"
                        });
                });

            modelBuilder.Entity("Entities.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Credits = 3,
                            Name = "Física",
                            TeacherId = 1
                        },
                        new
                        {
                            Id = 2,
                            Credits = 3,
                            Name = "Matematicas",
                            TeacherId = 1
                        },
                        new
                        {
                            Id = 3,
                            Credits = 3,
                            Name = "Filosofía",
                            TeacherId = 2
                        },
                        new
                        {
                            Id = 4,
                            Credits = 3,
                            Name = "Psicología",
                            TeacherId = 2
                        },
                        new
                        {
                            Id = 5,
                            Credits = 3,
                            Name = "Literatura",
                            TeacherId = 3
                        },
                        new
                        {
                            Id = 6,
                            Credits = 3,
                            Name = "Idiomas Extranjeros",
                            TeacherId = 3
                        },
                        new
                        {
                            Id = 7,
                            Credits = 3,
                            Name = "Biología",
                            TeacherId = 4
                        },
                        new
                        {
                            Id = 8,
                            Credits = 3,
                            Name = "Química",
                            TeacherId = 4
                        },
                        new
                        {
                            Id = 9,
                            Credits = 3,
                            Name = "Historia del Arte",
                            TeacherId = 5
                        },
                        new
                        {
                            Id = 10,
                            Credits = 3,
                            Name = "Música",
                            TeacherId = 5
                        });
                });

            modelBuilder.Entity("Entities.Entities.Partner", b =>
                {
                    b.HasOne("Entities.Entities.PartnerTypes", "PartnerType")
                        .WithMany()
                        .HasForeignKey("PartnerTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartnerType");
                });

            modelBuilder.Entity("Entities.Entities.PartnerSubject", b =>
                {
                    b.HasOne("Entities.Entities.Partner", "Student")
                        .WithMany("PartnerSubject")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_PartnerSubject_Restrict_Student");

                    b.HasOne("Entities.Entities.Subject", "Subject")
                        .WithMany("StudentsSubject")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_PartnerSubject_Restrict_Subject");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Entities.Entities.Subject", b =>
                {
                    b.HasOne("Entities.Entities.Partner", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Entities.Entities.Partner", b =>
                {
                    b.Navigation("PartnerSubject");
                });

            modelBuilder.Entity("Entities.Entities.Subject", b =>
                {
                    b.Navigation("StudentsSubject");
                });
#pragma warning restore 612, 618
        }
    }
}