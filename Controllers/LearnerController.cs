using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lap3.Data;
using Lap3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lap3.Controllers
{
    [Route("[controller]")]
    public class LearnerController : Controller
    {
        private SchoolContext db;
        public LearnerController(SchoolContext context)
        {
            db = context;
        }
        private int pageSize = 3;
        [HttpGet("List")]
        public IActionResult Index(int? mid)
        {
            var learners = (IQueryable<Learner>)db.Learners.Include(m => m.Major);
            if (mid != null)
            {
                learners = (IQueryable<Learner>)db.Learners.Where(l => l.MajorID == mid).Include(m => m.Major);
            }
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = learners.Take(pageSize).ToList();
            return View(result);
        }

        [HttpGet("LearnerByMajorID")]
        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners.Where(l => l.MajorID == mid).Include(m => m.Major).ToList();
            return PartialView("LearnerTable", learners);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.Majors = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(learner);
        }

        [HttpGet("Update")]
        public IActionResult Edit(int id)
        {
            if (id == 0 || db.Learners == null)
            {
                return NotFound();
            }
            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(learner);
        }

        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int id)
        {
            if (id == 0 || db.Learners == null)
            {
                return NotFound();
            }
            var learner = db.Learners.Include(l => l.Major).Include(e => e.Enrollments).FirstOrDefault(m => m.LearnerID == id);
            if (learner == null)
            {
                return NotFound();
            }
            if (learner.Enrollments.Any())
            {
                return Content("This learner has some enrolments, can't delete!");
            }
            return View(learner);
        }

        [HttpPost("DeleteConfirmed/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }

            db.Learners.Remove(learner);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("LearnerFilter")]
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            var learners = (IQueryable<Learner>)db.Learners;
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid);
                ViewBag.mid = mid;
            }
            if (keyword != null)
            {
                learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;
            }
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = learners.Skip(pageSize * (page - 1)).Take(pageSize).Include(m => m.Major);
            return PartialView("LearnerTable", result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}