using Microsoft.EntityFrameworkCore.Migrations;

namespace WZH.Infrastructure.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowDetails_Borrow_BorrowId",
                table: "BorrowDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowDetails_Borrow_BorrowId",
                table: "BorrowDetails",
                column: "BorrowId",
                principalTable: "Borrow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowDetails_Borrow_BorrowId",
                table: "BorrowDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowDetails_Borrow_BorrowId",
                table: "BorrowDetails",
                column: "BorrowId",
                principalTable: "Borrow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
