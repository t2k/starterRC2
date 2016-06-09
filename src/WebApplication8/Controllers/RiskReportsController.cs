using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.Models;
using WebApplication8.Models.RiskReportViewModels;

namespace WebApplication8.Controllers
{
    public class RiskReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiskReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RiskReports
        public async Task<IActionResult> Index()
        {
            return View(await _context.RiskReport.ToListAsync());
        }

        // GET: RiskReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.RiskReport.Include(i => i.RRRIs).SingleOrDefaultAsync(m => m.Id == id);

            DetailsViewModel vm = new DetailsViewModel
            {
                Report = report,
                RiskItems = await _context.RiskItem.Where(i => report.RiskItemIds.Contains(i.Id)).Include(k => k.RiskCategory).Include(l => l.RiskClass).ToListAsync()
            };

            return View(vm);
        }


        // GET: RiskReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RiskReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] RiskReport riskReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riskReport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(riskReport);
        }

        // GET: RiskReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskReport = await _context.RiskReport.SingleOrDefaultAsync(m => m.Id == id);
            if (riskReport == null)
            {
                return NotFound();
            }
            return View(riskReport);
        }

        // POST: RiskReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] RiskReport riskReport)
        {
            if (id != riskReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riskReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskReportExists(riskReport.Id))
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
            return View(riskReport);
        }

        // GET: RiskReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riskReport = await _context.RiskReport.SingleOrDefaultAsync(m => m.Id == id);
            if (riskReport == null)
            {
                return NotFound();
            }

            return View(riskReport);
        }

        // POST: RiskReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riskReport = await _context.RiskReport.SingleOrDefaultAsync(m => m.Id == id);
            _context.RiskReport.Remove(riskReport);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RiskReportExists(int id)
        {
            return _context.RiskReport.Any(e => e.Id == id);
        }
    }
}
