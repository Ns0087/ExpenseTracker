using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.DAL;
using Expense_Tracker.DAL.Entities;
using Expense_Tracker.Services.Interfaces;
using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Models.ResponseViewModels;
using Expense_Tracker.Services.Implementations;

namespace Expense_Tracker.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(IServiceProvider serviceProvider)
        {
            userService = serviceProvider.GetRequiredService<IUserService>();
        }

        // GET: Users
        public IActionResult Index()
        
        
        {
              return View();
        }

        // GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    //if (id == null || _context.Users == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //var user = await _context.Users
        //    //    .FirstOrDefaultAsync(m => m.UserId == id);
        //    //if (user == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return View(user);
        //}

        // GET: Users/AddOrEdit
        public IActionResult SignUpOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new SignUpModel());
            //else
            //{
            //    try
            //    {
            //        var category = await userService.logIn(id);
            //        return View(category.ToEntity<CategoryResponseModel, CategoryModel>());
            //    }
            //    catch (Exception ex)
            //    {
            //        return Problem(ex.Message);
            //    }
            //}
            return View(new SignUpModel());
        }

        // POST: Users/SignUpOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpOrEdit([Bind("UserId,FullName,UserName,Email,Phone,Password,Gender")] SignUpModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (user.UserId == 0)
                    {
                        await userService.SignUp(user);
                        return RedirectToAction(nameof(Index));
                    }

                    //else
                    //    await userService.UpdateUserAsync(user);
                    //return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("Email,Password")] LogInModel logIn)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     var user = await userService.logIn(logIn);
                    if(user != null)
                    {
                        return RedirectToAction(nameof(Index), "Dashboard", user.UserId);
                    }
                    //else
                    //    await userService.UpdateUserAsync(user);
                    //return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserId,FullName,UserName,Email,Phone,Password,Gender")] User user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.UserId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        //    }
        //    var user = await _context.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //  return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}
    }
}
