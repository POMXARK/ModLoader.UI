﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModLoader;

#nullable disable

namespace ModLoader.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20221127093829_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("ModLoader.Models.Mod", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isDelited")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Mods");

                    b.HasData(
                        new
                        {
                            Id = 1u,
                            Description = "Walk the dog",
                            Icon = "C:\\test.png",
                            Name = "Mod_1",
                            isDelited = false
                        },
                        new
                        {
                            Id = 2u,
                            Description = "Buy some milk",
                            Icon = "C:\\test.png",
                            Name = "Mod_2",
                            isDelited = false
                        },
                        new
                        {
                            Id = 3u,
                            Description = "Learn Avalonia",
                            Icon = "C:\\test.png",
                            Name = "Mod_3",
                            isDelited = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
