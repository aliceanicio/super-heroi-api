﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using super_heroi_api.Data;

#nullable disable

namespace super_heroi_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("super_heroi_api.Models.Herois", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Altura")
                        .HasColumnType("real");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2(7)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("NomeHeroi")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<float>("Peso")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Herois");
                });

            modelBuilder.Entity("super_heroi_api.Models.HeroisSuperpoderes", b =>
                {
                    b.Property<int>("HeroiId")
                        .HasColumnType("int");

                    b.Property<int>("SuperpoderId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("HeroiId", "SuperpoderId");

                    b.HasIndex("SuperpoderId");

                    b.ToTable("HeroisSuperpoderes");
                });

            modelBuilder.Entity("super_heroi_api.Models.Superpoderes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Superpoder")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Superpoderes");
                });

            modelBuilder.Entity("super_heroi_api.Models.HeroisSuperpoderes", b =>
                {
                    b.HasOne("super_heroi_api.Models.Herois", "Herois")
                        .WithMany("HeroisSuperpoderes")
                        .HasForeignKey("HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("super_heroi_api.Models.Superpoderes", "Superpoder")
                        .WithMany()
                        .HasForeignKey("SuperpoderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Herois");

                    b.Navigation("Superpoder");
                });

            modelBuilder.Entity("super_heroi_api.Models.Herois", b =>
                {
                    b.Navigation("HeroisSuperpoderes");
                });
#pragma warning restore 612, 618
        }
    }
}
