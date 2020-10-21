using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADETQ2_LarzAndFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADETQ2_LarzAndFriends.Controllers
{
    public class MembersController : Controller
    {

        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Members Member { get; set; }

        public MembersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Member = new Members();
            if (id == null)
            {
                //create
                return View(Member);
            }
            //update
            Member = _db.Members.FirstOrDefault(u => u.Id == id);
            if (Member == null)
            {
                return NotFound();
            }
            return View(Member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Member.Id == 0)
                {
                    //create
                    _db.Members.Add(Member);
                }
                else
                {
                    _db.Members.Update(Member);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Member);
        }

        
        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Members.ToListAsync() });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var memberFromDb = await _db.Members.FirstOrDefaultAsync(u => u.Id == id);
            if (memberFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
                
            }
            _db.Members.Remove(memberFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });

        }
        #endregion

    }
}
