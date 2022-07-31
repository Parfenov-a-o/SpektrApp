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

        }

        public ApplicationContext(int id)
        {
            if (id == 0)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SpektrDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Client cl1 = new Client() { Id = 1, Name = "Высшая школа экономики" };
            Client cl2 = new Client() { Id = 2, Name = "Алтайский государственый университет" };
            Client cl3 = new Client() { Id = 3, Name = "Московский государственный университет" };
            Client cl4 = new Client() { Id = 4, Name = "ООО Ромашка" };
            Client cl5 = new Client() { Id = 5, Name = "ООО Семицветик" };
            Client cl6 = new Client() { Id = 6, Name = "ООО Радуга" };

            EquipmentCategory ct1 = new EquipmentCategory() { Id = 1, Name = "Датчики" };
            EquipmentCategory ct2 = new EquipmentCategory() { Id = 2, Name = "Приборы" };

            Equipment eq1 = new Equipment() { Id = 1, Name = "ДИП-31", Units = "Шт.", EquipmentCategoryId = ct1.Id };
            Equipment eq2 = new Equipment() { Id = 2, Name = "ДИП-32", Units = "Шт.", EquipmentCategoryId = ct1.Id };
            Equipment eq3 = new Equipment() { Id = 3, Name = "ДИП-33", Units = "Шт.", EquipmentCategoryId = ct1.Id };
            Equipment eq4 = new Equipment() { Id = 4, Name = "C2000-К", Units = "Шт.", EquipmentCategoryId = ct2.Id };
            Equipment eq5 = new Equipment() { Id = 5, Name = "C2000-М", Units = "Шт.", EquipmentCategoryId = ct2.Id };
            Equipment eq6 = new Equipment() { Id = 6, Name = "C2000-Н", Units = "Шт.", EquipmentCategoryId = ct2.Id };

            EmployeePosition position1 = new EmployeePosition() { Id = 1, Name = "Директор" };
            EmployeePosition position2 = new EmployeePosition() { Id = 2, Name = "Бухгалтер" };
            EmployeePosition position3 = new EmployeePosition() { Id = 3, Name = "Сотрудник монтажной бригады" };

            Employee empl1 = new Employee() { Id = 1, Surname = "Иванов", FirstName = "Иван", Patronymic = "Павлович", Gender = "Мужской", EmployeePositionId = position1.Id };
            Employee empl2 = new Employee() { Id = 2, Surname = "Сидорова", FirstName = "Татьяна", Patronymic = "Михайловна", Gender = "Женский", EmployeePositionId = position2.Id };
            Employee empl3 = new Employee() { Id = 3, Surname = "Живоглядов", FirstName = "Антон", Patronymic = "Эдуардович", Gender = "Мужской", EmployeePositionId = position3.Id };
            Employee empl4 = new Employee() { Id = 4, Surname = "Быстров", FirstName = "Петр", Patronymic = "Александрович", Gender = "Мужской", EmployeePositionId = position3.Id };

            UserRole usRol1 = new UserRole() {Id=1, Name = "Администратор" };
            UserRole usRol2 = new UserRole() { Id = 2, Name = "Рядовой пользователь" };

            User user1 = new User() { Login = "Aleksandr", Password = "Hero", UserRoleId = usRol1.Id};
            User user2 = new User() { Login = "Elena", Password = "Villain", UserRoleId = usRol2.Id };

            CompletedProject comprj1 = new CompletedProject() { Id = 1, ClientId = cl1.Id, ProjectCompletionDate = DateTime.Now.Date, Address = "Москва, ул. Ленина, д.22, кв.122" };
            //CompletedProject comprj2 = new CompletedProject() { Id = 2, ClientId = cl5.Id, Employees = new List<Employee>() { empl3, empl4 }, ProjectCompletionDate = DateTime.Now.Date, Address = "Барнаул, ул. Ленина, д.11, кв.111", InstalledEquipments = new List<InstalledEquipment>() { new InstalledEquipment() { Id = 2, IndexNumber = 1, EquipmentId = eq3.Id, Count = 50 } } };

            

            modelBuilder.Entity<Client>().HasData(cl1);
            modelBuilder.Entity<Client>().HasData(cl2);
            modelBuilder.Entity<Client>().HasData(cl3);
            modelBuilder.Entity<Client>().HasData(cl4);
            modelBuilder.Entity<Client>().HasData(cl5);
            modelBuilder.Entity<Client>().HasData(cl6);
            modelBuilder.Entity<EquipmentCategory>().HasData(ct1);
            modelBuilder.Entity<EquipmentCategory>().HasData(ct2);
            modelBuilder.Entity<Equipment>().HasData(eq1);
            modelBuilder.Entity<Equipment>().HasData(eq2);
            modelBuilder.Entity<Equipment>().HasData(eq3);
            modelBuilder.Entity<Equipment>().HasData(eq4);
            modelBuilder.Entity<Equipment>().HasData(eq5);
            modelBuilder.Entity<Equipment>().HasData(eq6);
            modelBuilder.Entity<EmployeePosition>().HasData(position1);
            modelBuilder.Entity<EmployeePosition>().HasData(position2);
            modelBuilder.Entity<EmployeePosition>().HasData(position3);
            modelBuilder.Entity<Employee>().HasData(empl1);
            modelBuilder.Entity<Employee>().HasData(empl2);
            modelBuilder.Entity<Employee>().HasData(empl3);
            modelBuilder.Entity<Employee>().HasData(empl4);
            modelBuilder.Entity<UserRole>().HasData(usRol1);
            modelBuilder.Entity<UserRole>().HasData(usRol2);
            modelBuilder.Entity<User>().HasData(user1);
            modelBuilder.Entity<User>().HasData(user2);
            modelBuilder.Entity<CompletedProject>().HasData(comprj1);
            //modelBuilder.Entity<CompletedProject>().HasData(comprj2);



            modelBuilder.Entity<Employee>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("Surname || ' ' || FirstName || ' ' || Patronymic");
        }
    }
}
