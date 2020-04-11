﻿// <auto-generated />
using System;
using Beeffective.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Beeffective.Migrations
{
    [DbContext(typeof(CellContext))]
    [Migration("20200411071553_Add_TimeSpent")]
    partial class Add_TimeSpent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("Beeffective.Data.Entities.CellEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Goal")
                        .HasColumnType("TEXT");

                    b.Property<double>("Importance")
                        .HasColumnType("REAL");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("TimeSpent")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<double>("Urgency")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Cells");
                });
#pragma warning restore 612, 618
        }
    }
}
