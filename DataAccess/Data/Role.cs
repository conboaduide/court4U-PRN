using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    [Table("Role")]
    public class Role
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
