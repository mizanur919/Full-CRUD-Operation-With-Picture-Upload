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
    public class FeesController : Controller
    {
        private readonly mydbContext _context;

        public FeesController(mydbContext context)
        {
            _context = context;
        }

        // GET: Fees
        public async Task<IActionResult> Index()
        {
            var mydbContext = _context.Fee.Include(f => f.Student);
            return View(await mydbContext.ToListAsync());
        }

        // GET: Fees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fee = await _context.Fee
                .Include(f => f.Student)
                .SingleOrDefaultAsync(m => m.Voucherno == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // GET: Fees/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Voucherno,InputDate,StudentId,Headname,Amount")] Fee fee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(fee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", fee.StudentId);
                return View(fee);
            }
            catch (DbUpdateException dbx)
            {
                ViewData["err"] = "Inserted Voucher No. is already exists";
                return View();
            }
            catch (Exception ex)
            {
                ViewData["err"] = ex.Message.ToString();
                return View();
            }
        }

        // GET: Fees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fee = await _context.Fee.SingleOrDefaultAsync(m => m.Voucherno == id);
            if (fee == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", fee.StudentId);
            return View(fee);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Voucherno,InputDate,StudentId,Headname,Amount")] Fee fee)
        {
            if (id != fee.Voucherno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeExists(fee.Voucherno))
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
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", fee.StudentId);
            return View(fee);
        }

        // GET: Fees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fee = await _context.Fee
                .Include(f => f.Student)
                .SingleOrDefaultAsync(m => m.Voucherno == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var fee = await _context.Fee.SingleOrDefaultAsync(m => m.Voucherno == id);
            _context.Fee.Remove(fee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeExists(string id)
        {
            return _context.Fee.Any(e => e.Voucherno == id);
        }
    }
}
