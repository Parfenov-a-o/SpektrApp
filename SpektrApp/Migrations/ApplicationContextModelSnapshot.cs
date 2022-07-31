﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpektrApp;

#nullable disable

namespace SpektrApp.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("CompletedProjectEmployee", b =>
                {
                    b.Property<int>("CompletedProjectsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CompletedProjectsId", "EmployeesId");

                    b.HasIndex("EmployeesId");

                    b.ToTable("CompletedProjectEmployee");
                });

            modelBuilder.Entity("CompletedProjectInstalledEquipment", b =>
                {
                    b.Property<int>("CompletedProjectsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstalledEquipmentsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CompletedProjectsId", "InstalledEquipmentsId");

                    b.HasIndex("InstalledEquipmentsId");

                    b.ToTable("CompletedProjectInstalledEquipment");
                });

            modelBuilder.Entity("InstalledEquipmentMaintainedObject", b =>
                {
                    b.Property<int>("InstalledEquipmentsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaintainedObjectsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InstalledEquipmentsId", "MaintainedObjectsId");

                    b.HasIndex("MaintainedObjectsId");

                    b.ToTable("InstalledEquipmentMaintainedObject");
                });

            modelBuilder.Entity("SpektrApp.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Contacts")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Высшая школа экономики"
                        });
                });

            modelBuilder.Entity("SpektrApp.Models.CompletedProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ObjectDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ObjectSchema")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ProjectCompletionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("CompletedProjects");
                });

            modelBuilder.Entity("SpektrApp.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmployeePositionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT")
                        .HasComputedColumnSql("Surname || ' ' || FirstName || ' ' || Patronymic");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeePositionId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeePositionId = 1,
                            FirstName = "Олег",
                            Gender = "Мужской",
                            Patronymic = "Алексеевич",
                            Surname = "Парфенов"
                        },
                        new
                        {
                            Id = 2,
                            EmployeePositionId = 2,
                            FirstName = "Светлана",
                            Gender = "Женский",
                            Patronymic = "Михайловна",
                            Surname = "Парфенова"
                        },
                        new
                        {
                            Id = 3,
                            EmployeePositionId = 3,
                            FirstName = "Антон",
                            Gender = "Мужской",
                            Patronymic = "Анатольевич",
                            Surname = "Баранов"
                        });
                });

            modelBuilder.Entity("SpektrApp.Models.EmployeePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmployeePositions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Директор"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Бухгалтер"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Сотрудник монтажной бригады"
                        });
                });

            modelBuilder.Entity("SpektrApp.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("EquipmentCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Units")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentCategoryId");

                    b.ToTable("Equipments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EquipmentCategoryId = 1,
                            Name = "ДИП-31",
                            Units = "Шт."
                        },
                        new
                        {
                            Id = 2,
                            EquipmentCategoryId = 2,
                            Name = "C2000-М",
                            Units = "Шт."
                        });
                });

            modelBuilder.Entity("SpektrApp.Models.EquipmentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EquipmentCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Датчики"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Приборы"
                        });
                });

            modelBuilder.Entity("SpektrApp.Models.InstalledEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Count")
                        .HasColumnType("REAL");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IndexNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.ToTable("InstalledEquipment");
                });

            modelBuilder.Entity("SpektrApp.Models.MaintainedObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ObjectDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ObjectSchema")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ServiceEndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ServiceStartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("MaintainedObjects");
                });

            modelBuilder.Entity("SpektrApp.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Login");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SpektrApp.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("CompletedProjectEmployee", b =>
                {
                    b.HasOne("SpektrApp.Models.CompletedProject", null)
                        .WithMany()
                        .HasForeignKey("CompletedProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpektrApp.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompletedProjectInstalledEquipment", b =>
                {
                    b.HasOne("SpektrApp.Models.CompletedProject", null)
                        .WithMany()
                        .HasForeignKey("CompletedProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpektrApp.Models.InstalledEquipment", null)
                        .WithMany()
                        .HasForeignKey("InstalledEquipmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InstalledEquipmentMaintainedObject", b =>
                {
                    b.HasOne("SpektrApp.Models.InstalledEquipment", null)
                        .WithMany()
                        .HasForeignKey("InstalledEquipmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpektrApp.Models.MaintainedObject", null)
                        .WithMany()
                        .HasForeignKey("MaintainedObjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpektrApp.Models.CompletedProject", b =>
                {
                    b.HasOne("SpektrApp.Models.Client", "Client")
                        .WithMany("CompletedProjects")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("SpektrApp.Models.Employee", b =>
                {
                    b.HasOne("SpektrApp.Models.EmployeePosition", "EmployeePosition")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeePositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeePosition");
                });

            modelBuilder.Entity("SpektrApp.Models.Equipment", b =>
                {
                    b.HasOne("SpektrApp.Models.EquipmentCategory", "EquipmentCategory")
                        .WithMany("Equipments")
                        .HasForeignKey("EquipmentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipmentCategory");
                });

            modelBuilder.Entity("SpektrApp.Models.InstalledEquipment", b =>
                {
                    b.HasOne("SpektrApp.Models.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("SpektrApp.Models.MaintainedObject", b =>
                {
                    b.HasOne("SpektrApp.Models.Client", "Client")
                        .WithMany("MaintainedObjects")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpektrApp.Models.Employee", "Employee")
                        .WithMany("MaintainedObjects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SpektrApp.Models.User", b =>
                {
                    b.HasOne("SpektrApp.Models.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("SpektrApp.Models.Client", b =>
                {
                    b.Navigation("CompletedProjects");

                    b.Navigation("MaintainedObjects");
                });

            modelBuilder.Entity("SpektrApp.Models.Employee", b =>
                {
                    b.Navigation("MaintainedObjects");
                });

            modelBuilder.Entity("SpektrApp.Models.EmployeePosition", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("SpektrApp.Models.EquipmentCategory", b =>
                {
                    b.Navigation("Equipments");
                });

            modelBuilder.Entity("SpektrApp.Models.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
