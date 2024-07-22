using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class KeyID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedSlot_Booking_BookingId",
                table: "BookedSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedSlot_Slot_SlotId",
                table: "BookedSlot");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "MemberSubscription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Booking",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "BookingId",
                table: "BookedSlot",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedSlot_Booking_BookingId",
                table: "BookedSlot",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedSlot_Slot_SlotId",
                table: "BookedSlot",
                column: "SlotId",
                principalTable: "Slot",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedSlot_Booking_BookingId",
                table: "BookedSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedSlot_Slot_SlotId",
                table: "BookedSlot");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "MemberSubscription");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "BookingId",
                table: "BookedSlot",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedSlot_Booking_BookingId",
                table: "BookedSlot",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedSlot_Slot_SlotId",
                table: "BookedSlot",
                column: "SlotId",
                principalTable: "Slot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
