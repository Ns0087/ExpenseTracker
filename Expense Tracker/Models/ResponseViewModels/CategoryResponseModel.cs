using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models.ResponseViewModels
{
    public class CategoryResponseModel
    {
        [Key]
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; } = "";

        public string Type { get; set; } = "Expense";

        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public string? TitleWithIcon
        {
            set { }
            get
            {
                return Icon + " " + Title;
            }
        }
    }
}
