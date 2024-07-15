using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "BookedSlot",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookedSlot_BookingId",
                table: "BookedSlot",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedSlot_Booking_BookingId",
                table: "BookedSlot",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedSlot_Booking_BookingId",
                table: "BookedSlot");

            migrationBuilder.DropIndex(
                name: "IX_BookedSlot_BookingId",
                table: "BookedSlot");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookedSlot");
        }
    }
}
