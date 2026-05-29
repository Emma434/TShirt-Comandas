using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TShirt.Comandas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PivotToGenericSaaS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NombreCliente = table.Column<string>(type: "text", nullable: false),
                    Origen = table.Column<string>(type: "text", nullable: false),
                    ResponsableEjecucion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsComanda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SkuProducto = table.Column<string>(type: "text", nullable: false),
                    DescripcionProducto = table.Column<string>(type: "text", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    NotasConfiguracion = table.Column<string>(type: "jsonb", nullable: true),
                    ComandaId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsComanda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsComanda_Comandas_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsComanda_ComandaId",
                table: "ItemsComanda",
                column: "ComandaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsComanda");

            migrationBuilder.DropTable(
                name: "Comandas");
        }
    }
}
