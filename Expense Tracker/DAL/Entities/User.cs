using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.DAL.Entities
{
    public class User
    {
        [Key] 
        public int UserId { get; }

        [Required(ErrorMessage = "Name is required.")]
        public string? FullName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Username is required.")]
        public string? UserName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        public string? Gender { get; set; } = string.Empty;
    }
}
