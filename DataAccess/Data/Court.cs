using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    [Table("Court")]
    public class Court
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Num { get; set; }
        public Enums.CourtStatus Status { get; set; }

        [ForeignKey("Clubs")]
        public string ClubId { get; set; }
        public Club Club { get; set; }
    }
}
