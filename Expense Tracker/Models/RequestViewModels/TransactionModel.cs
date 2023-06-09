﻿using Expense_Tracker.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models.RequestViewModels
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public string? CategoryTitleWithIcon
        {
            set { }
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }

        public string? FormattedAmount
        {
            set { }
            get
            {
                return (Category == null || Category.Type == "Expense" ? "- " : "+ ") + Amount.ToString("C0");
            }
        }

    }
}
