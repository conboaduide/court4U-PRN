using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("Reviews")]
    public class Review : Entity
    {
        public string Content { get; set; }
        public int ParentId { get; set; }
        public string CommentLeft { get; set; }
        public string CommentRight { get; set; }

        [ForeignKey("Clubs")]
        public string ClubId { get; set; }
        public Club Club { get; set; }

        [ForeignKey("Users")]
        public string ReviewerId { get; set; }
        public User Reviewer { get; set; }
    }
}
