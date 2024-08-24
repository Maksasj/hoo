﻿// <auto-generated />
using System;
using Hoo.Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hoo.Service.Migrations
{
    [DbContext(typeof(HooDbContext))]
    [Migration("20240824112039_AddGoogleFiles")]
    partial class AddGoogleFiles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Hoo.Service.Models.GoogleFileItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GoogleDriveFiles");
                });

            modelBuilder.Entity("Hoo.Service.Models.WebFileItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccessUri")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastModificationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WebFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
