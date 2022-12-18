using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WZH.Infrastructure.Migrations
{
    public partial class _20221215Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borrow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键ID"),
                    ArchiveId = table.Column<long>(type: "bigint", nullable: false, comment: "文档ID"),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrow");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
