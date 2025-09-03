using System.ComponentModel.DataAnnotations;

namespace OnlineBank.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        public DateOnly CreatedAt { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
