using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DindPoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HierarchyLevel",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportsTo",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HeadOfDepartmentId = table.Column<int>(type: "int", nullable: true),
                    ParentDepartmentId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Department_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_User_HeadOfDepartmentId",
                        column: x => x.HeadOfDepartmentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HierarchyLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OldReportsTo = table.Column<int>(type: "int", nullable: true),
                    NewReportsTo = table.Column<int>(type: "int", nullable: true),
                    OldHierarchyLevel = table.Column<int>(type: "int", nullable: false),
                    NewHierarchyLevel = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HierarchyLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HierarchyLog_User_ChangedBy",
                        column: x => x.ChangedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HierarchyLog_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TargetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeriodType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiTemplate_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: false),
                    AssignedBy = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiAssignment_KpiTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "KpiTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiAssignment_User_AssignedBy",
                        column: x => x.AssignedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiAssignment_User_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubmittedTo = table.Column<int>(type: "int", nullable: true),
                    ActualValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EvidenceUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiProgress_KpiAssignment_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "KpiAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiProgress_User_SubmittedTo",
                        column: x => x.SubmittedTo,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_KpiProgress_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiApproval_KpiProgress_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "KpiProgress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KpiApproval_User_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 9, 9, 29, 30, 67, DateTimeKind.Utc).AddTicks(4607), "Karyawan biasa", true, "Staff" },
                    { 2, new DateTime(2026, 4, 9, 9, 29, 30, 67, DateTimeKind.Utc).AddTicks(4610), "Manager level", true, "Superior" },
                    { 3, new DateTime(2026, 4, 9, 9, 29, 30, 67, DateTimeKind.Utc).AddTicks(4611), "Head of Department", true, "HOD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ReportsTo",
                table: "User",
                column: "ReportsTo");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_HeadOfDepartmentId",
                table: "Department",
                column: "HeadOfDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentDepartmentId",
                table: "Department",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HierarchyLog_ChangedBy",
                table: "HierarchyLog",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_HierarchyLog_UserId",
                table: "HierarchyLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiApproval_ApproverId",
                table: "KpiApproval",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiApproval_ProgressId",
                table: "KpiApproval",
                column: "ProgressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpiAssignment_AssignedBy",
                table: "KpiAssignment",
                column: "AssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_KpiAssignment_AssignedTo",
                table: "KpiAssignment",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_KpiAssignment_TemplateId",
                table: "KpiAssignment",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiProgress_AssignmentId",
                table: "KpiProgress",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiProgress_SubmittedTo",
                table: "KpiProgress",
                column: "SubmittedTo");

            migrationBuilder.CreateIndex(
                name: "IX_KpiProgress_UserId",
                table: "KpiProgress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiTemplate_CreatedBy",
                table: "KpiTemplate",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Department_DepartmentId",
                table: "User",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_ReportsTo",
                table: "User",
                column: "ReportsTo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Department_DepartmentId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_ReportsTo",
                table: "User");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "HierarchyLog");

            migrationBuilder.DropTable(
                name: "KpiApproval");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "KpiProgress");

            migrationBuilder.DropTable(
                name: "KpiAssignment");

            migrationBuilder.DropTable(
                name: "KpiTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DepartmentId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ReportsTo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HierarchyLevel",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ReportsTo",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
