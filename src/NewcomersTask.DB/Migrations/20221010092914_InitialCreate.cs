using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NewcomersTask.DB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderSaga",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNumber = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    CustomerSurname = table.Column<string>(type: "text", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CurrentState = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSaga", x => x.CorrelationId);
                });

            migrationBuilder.CreateTable(
                name: "OrderSagaItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sku = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderCorrelationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSagaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSagaItem_OrderSaga_OrderCorrelationId",
                        column: x => x.OrderCorrelationId,
                        principalTable: "OrderSaga",
                        principalColumn: "CorrelationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSagaItem_OrderCorrelationId",
                table: "OrderSagaItem",
                column: "OrderCorrelationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSagaItem");

            migrationBuilder.DropTable(
                name: "OrderSaga");
        }
    }
}