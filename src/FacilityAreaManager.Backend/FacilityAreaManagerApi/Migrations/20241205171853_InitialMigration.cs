using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityAreaManagerApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessEquipmentTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Area = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEquipmentTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ProductionFacilities",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StandardAreaForEquipment = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFacilities", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPlacementContracts",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductionFacilityCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcessEquipmentTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPlacementContracts", x => x.Code);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContracts_ProcessEquipmentTypes_ProcessEquipmentTypeCode",
                        column: x => x.ProcessEquipmentTypeCode,
                        principalTable: "ProcessEquipmentTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentPlacementContracts_ProductionFacilities_ProductionFacilityCode",
                        column: x => x.ProductionFacilityCode,
                        principalTable: "ProductionFacilities",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContracts_ProcessEquipmentTypeCode",
                table: "EquipmentPlacementContracts",
                column: "ProcessEquipmentTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPlacementContracts_ProductionFacilityCode",
                table: "EquipmentPlacementContracts",
                column: "ProductionFacilityCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentPlacementContracts");

            migrationBuilder.DropTable(
                name: "ProcessEquipmentTypes");

            migrationBuilder.DropTable(
                name: "ProductionFacilities");
        }
    }
}
