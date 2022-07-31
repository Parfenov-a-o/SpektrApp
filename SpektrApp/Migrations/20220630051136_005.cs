using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpektrApp.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstalledEquipment_CompletedProjects_CompletedProjectId",
                table: "InstalledEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_InstalledEquipment_MaintainedObjects_MaintainedObjectId",
                table: "InstalledEquipment");

            migrationBuilder.DropIndex(
                name: "IX_InstalledEquipment_CompletedProjectId",
                table: "InstalledEquipment");

            migrationBuilder.DropIndex(
                name: "IX_InstalledEquipment_MaintainedObjectId",
                table: "InstalledEquipment");

            migrationBuilder.DropColumn(
                name: "CompletedProjectId",
                table: "InstalledEquipment");

            migrationBuilder.DropColumn(
                name: "MaintainedObjectId",
                table: "InstalledEquipment");

            migrationBuilder.CreateTable(
                name: "CompletedProjectInstalledEquipment",
                columns: table => new
                {
                    CompletedProjectsId = table.Column<int>(type: "INTEGER", nullable: false),
                    InstalledEquipmentsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedProjectInstalledEquipment", x => new { x.CompletedProjectsId, x.InstalledEquipmentsId });
                    table.ForeignKey(
                        name: "FK_CompletedProjectInstalledEquipment_CompletedProjects_CompletedProjectsId",
                        column: x => x.CompletedProjectsId,
                        principalTable: "CompletedProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedProjectInstalledEquipment_InstalledEquipment_InstalledEquipmentsId",
                        column: x => x.InstalledEquipmentsId,
                        principalTable: "InstalledEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstalledEquipmentMaintainedObject",
                columns: table => new
                {
                    InstalledEquipmentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaintainedObjectsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalledEquipmentMaintainedObject", x => new { x.InstalledEquipmentsId, x.MaintainedObjectsId });
                    table.ForeignKey(
                        name: "FK_InstalledEquipmentMaintainedObject_InstalledEquipment_InstalledEquipmentsId",
                        column: x => x.InstalledEquipmentsId,
                        principalTable: "InstalledEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstalledEquipmentMaintainedObject_MaintainedObjects_MaintainedObjectsId",
                        column: x => x.MaintainedObjectsId,
                        principalTable: "MaintainedObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedProjectInstalledEquipment_InstalledEquipmentsId",
                table: "CompletedProjectInstalledEquipment",
                column: "InstalledEquipmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalledEquipmentMaintainedObject_MaintainedObjectsId",
                table: "InstalledEquipmentMaintainedObject",
                column: "MaintainedObjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedProjectInstalledEquipment");

            migrationBuilder.DropTable(
                name: "InstalledEquipmentMaintainedObject");

            migrationBuilder.AddColumn<int>(
                name: "CompletedProjectId",
                table: "InstalledEquipment",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaintainedObjectId",
                table: "InstalledEquipment",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstalledEquipment_CompletedProjectId",
                table: "InstalledEquipment",
                column: "CompletedProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalledEquipment_MaintainedObjectId",
                table: "InstalledEquipment",
                column: "MaintainedObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalledEquipment_CompletedProjects_CompletedProjectId",
                table: "InstalledEquipment",
                column: "CompletedProjectId",
                principalTable: "CompletedProjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalledEquipment_MaintainedObjects_MaintainedObjectId",
                table: "InstalledEquipment",
                column: "MaintainedObjectId",
                principalTable: "MaintainedObjects",
                principalColumn: "Id");
        }
    }
}
