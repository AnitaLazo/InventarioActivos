using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace INVENTARIO.Services.ActivosCatalogApi.Data.Migrations
{
    public partial class ActivosDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "CatalogActivos_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "TipoActivos_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "TipoActivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Residual = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 100, nullable: false),
                    VidaUtil = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogActivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Acta_entrega = table.Column<string>(nullable: false),
                    Cod_institucion = table.Column<string>(nullable: false),
                    Detalle = table.Column<string>(maxLength: 100, nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Fecha_adquisicion = table.Column<DateTime>(nullable: false),
                    Marca = table.Column<string>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Precio_actual = table.Column<decimal>(nullable: false),
                    Precio_compra = table.Column<decimal>(nullable: false),
                    Serie_modelo = table.Column<string>(nullable: false),
                    TipoActivosID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogActivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogActivos_TipoActivos_TipoActivosID",
                        column: x => x.TipoActivosID,
                        principalTable: "TipoActivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogActivos_TipoActivosID",
                table: "CatalogActivos",
                column: "TipoActivosID");

           /* migrationBuilder.CreateIndex(
                name: "IX_TipoActivos_CatalogActivosId",
                table: "TipoActivos",
                column: "CatalogActivosId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogActivos_TipoActivos_TipoActivosID",
                table: "CatalogActivos");

            migrationBuilder.DropTable(
                name: "TipoActivos");

            migrationBuilder.DropTable(
                name: "CatalogActivos");

            migrationBuilder.DropSequence(
                name: "CatalogActivos_hilo");

            migrationBuilder.DropSequence(
                name: "TipoActivos_hilo");
        }
    }
}
