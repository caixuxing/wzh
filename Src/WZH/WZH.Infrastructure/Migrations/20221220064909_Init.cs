using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WZH.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borrow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键ID"),
                    ApplyBorrowName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "借阅申请名称"),
                    BorrowUserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "借阅部门"),
                    BorrowTpye = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "借阅日期"),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "预归还日期"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "Timestamp", rowVersion: true, nullable: false, comment: "版本号"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: false),
                    LastModifyUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDel = table.Column<bool>(type: "bit", nullable: false),
                    DelDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrow", x => x.Id);
                },
                comment: "借阅表");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键ID"),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogMsg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: false),
                    LastModifyUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDel = table.Column<bool>(type: "bit", nullable: false),
                    DelDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                },
                comment: "系统日志表");

            migrationBuilder.CreateTable(
                name: "BorrowDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键Id"),
                    BorrowId = table.Column<long>(type: "bigint", nullable: false, comment: "借阅主键Id"),
                    ArchiveId = table.Column<long>(type: "bigint", nullable: false, comment: "文档主键Id"),
                    RowVersion = table.Column<byte[]>(type: "Timestamp", rowVersion: true, nullable: false, comment: "版本号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowDetails_Borrow_BorrowId",
                        column: x => x.BorrowId,
                        principalTable: "Borrow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "借阅明细表");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowDetails_BorrowId",
                table: "BorrowDetails",
                column: "BorrowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowDetails");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Borrow");
        }
    }
}
