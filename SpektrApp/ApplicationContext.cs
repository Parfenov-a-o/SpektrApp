using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpektrApp.Models;

namespace SpektrApp
{
    internal class ApplicationContext:DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<EmployeePosition> EmployeePositions { get; set; } = null!;
        public DbSet<CompletedProject> CompletedProjects { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; } = null!;
        public DbSet<Equipment> Equipments { get; set; } = null!;
        public DbSet<MaintainedObject> MaintainedObjects { get; set; } = null!;


        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SpektrDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Client cl = new Client() { Id = 1, Name = "Высшая школа экономики" };

            EquipmentCategory ct1 = new EquipmentCategory() { Id = 1, Name = "Датчики" };
            EquipmentCategory ct2 = new EquipmentCategory() { Id = 2, Name = "Приборы" };

            Equipment eq1 = new Equipment() { Id = 1, Name = "ДИП-31", Units = "Шт.", EquipmentCategoryId = ct1.Id };
            Equipment eq2 = new Equipment() { Id = 2, Name = "C2000-М", Units = "Шт.", EquipmentCategoryId = ct2.Id };

            EmployeePosition position1 = new EmployeePosition() { Id = 1, Name = "Директор" };
            EmployeePosition position2 = new EmployeePosition() { Id = 2, Name = "Бухгалтер" };
            EmployeePosition position3 = new EmployeePosition() { Id = 3, Name = "Сотрудник монтажной бригады" };

            Employee empl1 = new Employee() { Id = 1, Surname = "Парфенов", FirstName = "Олег", Patronymic = "Алексеевич", Gender = "Мужской", EmployeePositionId = position1.Id };
            Employee empl2 = new Employee() { Id = 2, Surname = "Парфенова", FirstName = "Светлана", Patronymic = "Михайловна", Gender = "Женский", EmployeePositionId = position2.Id };
            Employee empl3 = new Employee() { Id = 3, Surname = "Баранов", FirstName = "Антон", Patronymic = "Анатольевич", Gender = "Мужской", EmployeePositionId = position3.Id };

            modelBuilder.Entity<Client>().HasData(cl);
            modelBuilder.Entity<EquipmentCategory>().HasData(ct1);
            modelBuilder.Entity<EquipmentCategory>().HasData(ct2);
            modelBuilder.Entity<Equipment>().HasData(eq1);
            modelBuilder.Entity<Equipment>().HasData(eq2);
            modelBuilder.Entity<EmployeePosition>().HasData(position1);
            modelBuilder.Entity<EmployeePosition>().HasData(position2);
            modelBuilder.Entity<EmployeePosition>().HasData(position3);
            modelBuilder.Entity<Employee>().HasData(empl1);
            modelBuilder.Entity<Employee>().HasData(empl2);
            modelBuilder.Entity<Employee>().HasData(empl3);

            modelBuilder.Entity<Employee>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("Surname || ' ' || FirstName || ' ' || Patronymic");
        }
    }
}
