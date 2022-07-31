using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpektrApp.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Employees",
                type: "TEXT",
                nullable: true,
                computedColumnSql: "Surname || ' ' || FirstName || ' ' || Patronymic");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Contacts", "Email", "Name", "PhoneNumber" },
                values: new object[] { 1, null, null, null, "Высшая школа экономики", null });

            migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Директор" });

            migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Бухгалтер" });

            migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Сотрудник монтажной бригады" });

            migrationBuilder.InsertData(
                table: "EquipmentCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Датчики" });

            migrationBuilder.InsertData(
                table: "EquipmentCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Приборы" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Email", "EmployeePositionId", "FirstName", "Gender", "Patronymic", "PhoneNumber", "Surname" },
                values: new object[] { 1, null, null, 1, "Олег", "Мужской", "Алексеевич", null, "Парфенов" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Email", "EmployeePositionId", "FirstName", "Gender", "Patronymic", "PhoneNumber", "Surname" },
                values: new object[] { 2, null, null, 2, "Светлана", "Женский", "Михайловна", null, "Парфенова" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Email", "EmployeePositionId", "FirstName", "Gender", "Patronymic", "PhoneNumber", "Surname" },
                values: new object[] { 3, null, null, 3, "Антон", "Мужской", "Анатольевич", null, "Баранов" });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Description", "EquipmentCategoryId", "Name", "Units" },
                values: new object[] { 1, null, 1, "ДИП-31", "Шт." });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Description", "EquipmentCategoryId", "Name", "Units" },
                values: new object[] { 2, null, 2, "C2000-М", "Шт." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeePositions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeePositions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeePositions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquipmentCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquipmentCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Employees");
        }
    }
}
