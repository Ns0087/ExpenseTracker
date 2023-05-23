using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models.ResponseViewModels
{
    public class UserResponseModel
    {
        [Key]
        [Required]
        public int UserId { get; set; }
        public string? FullName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Password { get; set; }

        public string Gender { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
