using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    [Table("Permission")]
    public class Permission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ICollection<ClubRole> ClubRoles { get; set; }

    }
}
