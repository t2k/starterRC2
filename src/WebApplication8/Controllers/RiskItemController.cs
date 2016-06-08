using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class RiskItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiskItemController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: RiskItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RiskItem.Include(r => r.RiskCategory).Include(r => r.RiskClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RiskItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskItem = await _context.RiskItem.SingleOrDefaultAsync(m => m.Id == id);
            if (riskItem == null)
            {
                return NotFound();
            }

            return View(riskItem);
        }

        // GET: RiskItem/Create
        public IActionResult Create()
        {
            ViewData["RiskCategoryId"] = new SelectList(_context.Set<RiskCategory>(), "Id", "CategoryName");
            ViewData["RiskClassId"] = new SelectList(_context.Set<RiskClass>(), "Id", "Classification");
            return View();
        }

        // POST: RiskItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,RiskCategoryId,RiskClassId,Score")] RiskItem riskItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RiskCategoryId"] = new SelectList(_context.Set<RiskCategory>(), "Id", "CategoryName");
            ViewData["RiskClassId"] = new SelectList(_context.Set<RiskClass>(), "Id", "Classification");
            return View(riskItem);
        }

        // GET: RiskItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskItem = await _context.RiskItem.SingleOrDefaultAsync(m => m.Id == id);
            if (riskItem == null)
            {
                return NotFound();
            }
            ViewData["RiskCategoryId"] = new SelectList(_context.Set<RiskCategory>(), "Id", "CategoryName");
            ViewData["RiskClassId"] = new SelectList(_context.Set<RiskClass>(), "Id", "Classification");
            return View(riskItem);
        }

        // POST: RiskItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,RiskCategoryId,RiskClassId,Score")] RiskItem riskItem)
        {
            if (id != riskItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskItemExists(riskItem.Id))
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
            ViewData["RiskCategoryId"] = new SelectList(_context.Set<RiskCategory>(), "Id", "CategoryName");
            ViewData["RiskClassId"] = new SelectList(_context.Set<RiskClass>(), "Id", "Classification");
            return View(riskItem);
        }

        // GET: RiskItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskItem = await _context.RiskItem.SingleOrDefaultAsync(m => m.Id == id);
            if (riskItem == null)
            {
                return NotFound();
            }

            return View(riskItem);
        }

        // POST: RiskItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riskItem = await _context.RiskItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.RiskItem.Remove(riskItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RiskItemExists(int id)
        {
            return _context.RiskItem.Any(e => e.Id == id);
        }
    }
}
