using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SessionTimeStamptoDateTimeandNavigationnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Start",
            table: "Sessions");

            migrationBuilder.AddColumn<DateTime>(
            name: "Start",
            table: "Sessions",
            type: "timestamp without time zone",
            nullable: false,
            defaultValueSql: "NOW()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Start",
            table: "Sessions");

            migrationBuilder.AddColumn<TimeSpan>( // если старый тип был interval
            name: "Start",
            table: "Sessions",
            type: "interval",
            nullable: false,
            defaultValue: TimeSpan.Zero);
        }
    }
}
