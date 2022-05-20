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



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SpektrDB.db");
        }
    }
}
