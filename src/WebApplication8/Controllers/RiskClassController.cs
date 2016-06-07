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
    public class RiskClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiskClassController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: RiskClass
        public async Task<IActionResult> Index()
        {
            return View(await _context.RiskClass.ToListAsync());
        }

        // GET: RiskClass/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskClass = await _context.RiskClass.SingleOrDefaultAsync(m => m.Id == id);
            if (riskClass == null)
            {
                return NotFound();
            }

            return View(riskClass);
        }

        // GET: RiskClass/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RiskClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Classification,Ordinal")] RiskClass riskClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riskClass);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(riskClass);
        }

        // GET: RiskClass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskClass = await _context.RiskClass.SingleOrDefaultAsync(m => m.Id == id);
            if (riskClass == null)
            {
                return NotFound();
            }
            return View(riskClass);
        }

        // POST: RiskClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Classification,Ordinal")] RiskClass riskClass)
        {
            if (id != riskClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riskClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskClassExists(riskClass.Id))
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
            return View(riskClass);
        }

        // GET: RiskClass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskClass = await _context.RiskClass.SingleOrDefaultAsync(m => m.Id == id);
            if (riskClass == null)
            {
                return NotFound();
            }

            return View(riskClass);
        }

        // POST: RiskClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riskClass = await _context.RiskClass.SingleOrDefaultAsync(m => m.Id == id);
            _context.RiskClass.Remove(riskClass);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RiskClassExists(int id)
        {
            return _context.RiskClass.Any(e => e.Id == id);
        }
    }
}
