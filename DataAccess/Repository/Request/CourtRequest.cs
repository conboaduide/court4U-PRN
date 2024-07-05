using DataAccess.Entity;

namespace DataAccess.Repository.Request
{
    public class CourtRequest
    {
        public int Num { get; set; }
        public Enums.CourtStatus Status { get; set; }
        public string ClubId { get; set; }
    }
}
