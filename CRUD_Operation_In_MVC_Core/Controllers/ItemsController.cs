using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Operation_In_MVC_Core.Model.Db;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CRUD_Operation_In_MVC_Core.Controllers
{
    public class ItemsController : Controller
    {
        private readonly mydbContext _context;

        public ItemsController(mydbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .SingleOrDefaultAsync(m => m.Itemcode == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Itemcode,Itemname,Picture")] Items items)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(items);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(items);
            }

            catch (DbUpdateException dbx)
            {
                ViewData["err"] = "Inserted Item Code No. Is Already Exists";
                return View();
            }
            catch (Exception ex)
            {
                ViewData["err"] = ex.Message.ToString();
                return View();
            }
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items.SingleOrDefaultAsync(m => m.Itemcode == id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Itemcode,Itemname,Picture")] Items items)
        {
            if (id != items.Itemcode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(items);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(items.Itemcode))
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
            return View(items);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .SingleOrDefaultAsync(m => m.Itemcode == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var items = await _context.Items.SingleOrDefaultAsync(m => m.Itemcode == id);
            _context.Items.Remove(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsExists(string id)
        {
            return _context.Items.Any(e => e.Itemcode == id);
        }

        // Uplode Files

        public ActionResult UploadFile(string id)
        {
            return View(_context.Items.Find(id));
        }
        [HttpPost, ActionName("UploadFile")]
        public ActionResult UploadFile(IFormFile file, string id)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");
                if (file.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/" + file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Items st = (from s in _context.Items where s.Itemcode == id select s).First();
                    st.Picture = file.FileName;
                    _context.SaveChanges();
                }
                ViewBag.Message = "File Uploaded Successfully!!" + ":" + id;
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

    }
}
