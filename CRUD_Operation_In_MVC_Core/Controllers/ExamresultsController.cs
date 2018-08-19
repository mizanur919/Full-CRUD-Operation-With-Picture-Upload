using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Operation_In_MVC_Core.Model.Db;

namespace CRUD_Operation_In_MVC_Core.Controllers
{
    public class ExamresultsController : Controller
    {
        private readonly mydbContext _context;

        public ExamresultsController(mydbContext context)
        {
            _context = context;
        }

        // GET: Examresults
        public async Task<IActionResult> Index()
        {
            var mydbContext = _context.Examresult.Include(e => e.Student);
            return View(await mydbContext.ToListAsync());
        }

        // GET: Examresults/Details/5
        public async Task<IActionResult> Details(int? examsl, int? studentid)
        {
            if (examsl == null || studentid == null)
            {
                return NotFound();
            }

            var examresult = await _context.Examresult
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.Examsl == examsl && m.Studentid == studentid);
            if (examresult == null)
            {
                return NotFound();
            }

            return View(examresult);
        }

        // GET: Examresults/Create
        public IActionResult Create()
        {
            ViewData["Studentid"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: Examresults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Examsl,Studentid,Examname,Institution,Board,Result")] Examresult examresult)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(examresult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Studentid"] = new SelectList(_context.Student, "Id", "Id", examresult.Studentid);
                return View(examresult);
            }
            catch (DbUpdateException dbx)
            {
                ViewData["err"] = "Inserted Exam Serial No. is already exists";
                return View();
            }
            catch (Exception ex)
            {
                ViewData["err"] = ex.Message.ToString();
                return View();
            }
        }

        // GET: Examresults/Edit/5
        public async Task<IActionResult> Edit(int? examsl, int? studentid)
        {
            if (examsl == null || studentid == null)
            {
                return NotFound();
            }

            var examresult = await _context.Examresult.SingleOrDefaultAsync(m => m.Examsl == examsl && m.Studentid == studentid);
            if (examresult == null)
            {
                return NotFound();
            }
            ViewData["Studentid"] = new SelectList(_context.Student, "Id", "Id", examresult.Studentid);
            return View(examresult);
        }

        // POST: Examresults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Examsl,Studentid,Examname,Institution,Board,Result")] Examresult examresult)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examresult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamresultExists(examresult.Examsl, examresult.Studentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Studentid"] = new SelectList(_context.Student, "Id", "Id", examresult.Studentid);
            return View(examresult);
        }

        // GET: Examresults/Delete/5
        public async Task<IActionResult> Delete(int? examsl, int? studentid)
        {
            if (examsl == null || studentid == null)
            {
                return NotFound();
            }

            var examresult = await _context.Examresult
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.Examsl == examsl && m.Studentid == studentid);
            if (examresult == null)
            {
                return NotFound();
            }

            return View(examresult);
        }

        // POST: Examresults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? examsl, int? studentid)
        {
            var examresult = await _context.Examresult.SingleOrDefaultAsync(m => m.Examsl == examsl && m.Studentid == studentid);
            _context.Examresult.Remove(examresult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamresultExists(int examsl, int studentid)
        {
            return _context.Examresult.Any(e => e.Examsl == examsl && e.Studentid == studentid);
        }
    }
}
