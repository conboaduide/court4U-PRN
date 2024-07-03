using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("ClubImage")]
    public class ClubImage : Entity
    {
        public string Name { get; set; }
        public string ClubImageUrl { get; set; }

        [ForeignKey("Clubs")]
        public string ClubId { get; set; }
        public Club Club { get; set; }
    }
}
