using System.ComponentModel.DataAnnotations;

namespace DataAccess.Repository.Request
{
    public class ClubRequest
    {
        [Required(ErrorMessage = "Club name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Club address is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City/Province is required.")]
        public string? CityOfProvince { get; set; }

        [Required(ErrorMessage = "District is required.")]
        public string? District { get; set; }

        [Required(ErrorMessage = "Logo is required.")]
        public string? LogoUrl { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string? UserId { get; set; }
    }
}
