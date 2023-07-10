﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(ScadaDBContext))]
    partial class ScadaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("webapi.Model.TagValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TagBaseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TagBaseId");

                    b.ToTable("TagValues");
                });

            modelBuilder.Entity("webapi.model.Alarm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnalogInputId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Limit")
                        .HasColumnType("REAL");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AnalogInputId");

                    b.ToTable("Alarms");
                });

            modelBuilder.Entity("webapi.model.IOAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("webapi.model.RealTimeUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("HighLimit")
                        .HasColumnType("REAL");

                    b.Property<double>("LowLimit")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("RealTimeUnits");
                });

            modelBuilder.Entity("webapi.model.TagBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("TagBases");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TagBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("webapi.model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("webapi.model.AnalogInput", b =>
                {
                    b.HasBaseType("webapi.model.TagBase");

                    b.Property<double>("HighLimit")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsScanning")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("INTEGER");

                    b.Property<double>("LowLimit")
                        .HasColumnType("REAL");

                    b.Property<double>("ScanTime")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("REAL");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("TagBases", t =>
                        {
                            t.Property("HighLimit")
                                .HasColumnName("AnalogInput_HighLimit");

                            t.Property("LowLimit")
                                .HasColumnName("AnalogInput_LowLimit");

                            t.Property("Unit")
                                .HasColumnName("AnalogInput_Unit");
                        });

                    b.HasDiscriminator().HasValue("AnalogInput");
                });

            modelBuilder.Entity("webapi.model.AnalogOutput", b =>
                {
                    b.HasBaseType("webapi.model.TagBase");

                    b.Property<double>("HighLimit")
                        .HasColumnType("REAL");

                    b.Property<double>("LowLimit")
                        .HasColumnType("REAL");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("AnalogOutput");
                });

            modelBuilder.Entity("webapi.model.DigitalInput", b =>
                {
                    b.HasBaseType("webapi.model.TagBase");

                    b.Property<bool>("IsScanning")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("INTEGER");

                    b.Property<double>("ScanTime")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("DigitalInput");
                });

            modelBuilder.Entity("webapi.model.DigitalOutput", b =>
                {
                    b.HasBaseType("webapi.model.TagBase");

                    b.HasDiscriminator().HasValue("DigitalOutput");
                });

            modelBuilder.Entity("webapi.Model.TagValue", b =>
                {
                    b.HasOne("webapi.model.TagBase", null)
                        .WithMany("Values")
                        .HasForeignKey("TagBaseId");
                });

            modelBuilder.Entity("webapi.model.Alarm", b =>
                {
                    b.HasOne("webapi.model.AnalogInput", "AnalogInput")
                        .WithMany("Alarms")
                        .HasForeignKey("AnalogInputId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnalogInput");
                });

            modelBuilder.Entity("webapi.model.RealTimeUnit", b =>
                {
                    b.HasOne("webapi.model.IOAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("webapi.model.TagBase", b =>
                {
                    b.HasOne("webapi.model.IOAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("webapi.model.TagBase", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("webapi.model.AnalogInput", b =>
                {
                    b.Navigation("Alarms");
                });
#pragma warning restore 612, 618
        }
    }
}
