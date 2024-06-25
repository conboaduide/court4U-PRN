using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    public class Pricing
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public float Price { get; set; }

        [ForeignKey("Clubs")]
        public string ClubId { get; set; }
        public Club Club { get; set; }
    }
}
