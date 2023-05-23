using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;
using Expense_Tracker.Services.Interfaces;
using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Extensions;
using Expense_Tracker.Models.ResponseViewModels;

namespace Expense_Tracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly ICategoryService categoryService;
        private readonly ICryptographicService cryptographicService;

        public TransactionController(IServiceProvider serviceProvider)
        {
            transactionService = serviceProvider.GetRequiredService<ITransactionService>();
            categoryService = serviceProvider.GetRequiredService<ICategoryService>();
            cryptographicService = serviceProvider.GetRequiredService<ICryptographicService>();
        }

        // GET: Transaction
        public async Task<IActionResult> Index(string? Id)
        {
            if (Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try
            {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }

            ViewBag.Id = Id;

            return View(await transactionService.GetAllTransactionAsync(userId));
        }

        // GET: Transaction/AddOrEdit
        public async Task<IActionResult> AddOrEdit(string? Id, int transactionId = 0)
        {
            if (Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try
            {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }

            ViewBag.Id = Id;

            await PopulateCategories(userId);

            if (transactionId == 0)
                return View(new TransactionModel());
            else
            {
                var transaction = await transactionService.GetTransactionByIdAsync(transactionId);
                return View(transaction.ToEntity<TransactionResponseModel, TransactionModel>());
            }
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] TransactionModel transaction, [Bind("Id")] string? Id)
        {
            if (Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try
            {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }

            ViewBag.Id = Id;

            if (ModelState.IsValid)
            {
                try
                {
                    transaction.UserId = userId;

                    if (transaction.TransactionId == 0)
                    {
                        await transactionService.AddTransactionAsync(transaction);
                        return RedirectToAction(nameof(Index), new { Id = ViewBag.Id });
                    }
                    else
                    {
                        await transactionService.UpdateTransactionAsync(transaction);
                        return RedirectToAction(nameof(Index), new { Id = ViewBag.Id });
                    }
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }
            await PopulateCategories(userId);
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? Id, int transactionId)
        {
            if (Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try
            {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }

            ViewBag.Id = Id;

            try
            {
                await transactionService.DeleteTransactionAsync(transactionId);
                return RedirectToAction(nameof(Index), new { Id = ViewBag.Id });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [NonAction]
        public async Task PopulateCategories(int userId)
        {
            var CategoryCollection = await categoryService.GetAllCategoriesAsync(userId);
            var DefaultCategory = new CategoryResponseModel() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory);

            var newCategoryCollection = new List<CategoryModel>();
            foreach (var category in CategoryCollection)
            {
                newCategoryCollection.Add(category.ToEntity<CategoryResponseModel, CategoryModel>());
            }

            ViewBag.Categories = newCategoryCollection;

        }
    }
}
