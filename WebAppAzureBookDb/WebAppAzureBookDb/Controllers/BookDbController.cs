using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppAzureBookDb.Models;

namespace WebAppAzureBookDb.Controllers
{
    public class BookDbController : Controller
    {
        private readonly BookStoreDbContext _context;

        public BookDbController(BookStoreDbContext context)
        {
            _context = context;
        }

        // GET: BookDb
        public async Task<IActionResult> Index()
        {
              return _context.BookDbs != null ? 
                          View(await _context.BookDbs.ToListAsync()) :
                          Problem("Entity set 'BookStoreDbContext.BookDbs'  is null.");
        }

        // GET: BookDb/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookDbs == null)
            {
                return NotFound();
            }

            var bookDb = await _context.BookDbs
                .FirstOrDefaultAsync(m => m.Bid == id);
            if (bookDb == null)
            {
                return NotFound();
            }

            return View(bookDb);
        }

        // GET: BookDb/Create
        public IActionResult Create()
        {
            return View(new BookDb());
        }

        // POST: BookDb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bid,Author,Publisher,Category,Price")] BookDb bookDb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookDb);
        }

        // GET: BookDb/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookDbs == null)
            {
                return NotFound();
            }

            var bookDb = await _context.BookDbs.FindAsync(id);
            if (bookDb == null)
            {
                return NotFound();
            }
            return View(bookDb);
        }

        // POST: BookDb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Bid,Author,Publisher,Category,Price")] BookDb bookDb)
        {
            if (id != bookDb.Bid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookDbExists(bookDb.Bid))
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
            return View(bookDb);
        }

        // GET: BookDb/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookDbs == null)
            {
                return NotFound();
            }

            var bookDb = await _context.BookDbs
                .FirstOrDefaultAsync(m => m.Bid == id);
            if (bookDb == null)
            {
                return NotFound();
            }

            return View(bookDb);
        }

        // POST: BookDb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookDbs == null)
            {
                return Problem("Entity set 'BookStoreDbContext.BookDbs'  is null.");
            }
            var bookDb = await _context.BookDbs.FindAsync(id);
            if (bookDb != null)
            {
                _context.BookDbs.Remove(bookDb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookDbExists(int id)
        {
          return (_context.BookDbs?.Any(e => e.Bid == id)).GetValueOrDefault();
        }
    }
}
