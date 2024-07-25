using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Request
{
    public class UpdateStaffRequest
    {
        public string? FullName { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public Enums.Status Status { get; set; }
        public Enums.Roles Role { get; set; }
        public string? ClubId { get; set; }
    }
}
