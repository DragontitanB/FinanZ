using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFi.Models;

namespace MyFi.Controllers
{
    public class IngresosController : Controller
    {
        private readonly finanzasContext _context;

        public IngresosController(finanzasContext context)
        {
            _context = context;
        }

        // GET: Ingresos
        public async Task<IActionResult> Index()
        {
            var finanzasContext = _context.Ingresos.Include(i => i.IdNavigation);
            return View(await finanzasContext.ToListAsync());
        }

        // GET: Ingresos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ingresos == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingresos
                .Include(i => i.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id_Ingresos == id);
            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        // GET: Ingresos/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Ingresos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIngresos,Id,Monto,Tipo,Fecha,Notificacion,Automatico,Recurrencia")] Ingreso ingreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Id", ingreso.Id);
            return View(ingreso);
        }

        // GET: Ingresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ingresos == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Id", ingreso.Id);
            return View(ingreso);
        }

        // POST: Ingresos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIngresos,Id,Monto,Tipo,Fecha,Notificacion,Automatico,Recurrencia")] Ingreso ingreso)
        {
            if (id != ingreso.Id_Ingresos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoExists(ingreso.Id_Ingresos))
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
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Id", ingreso.Id);
            return View(ingreso);
        }

        // GET: Ingresos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ingresos == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingresos
                .Include(i => i.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id_Ingresos == id);
            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        // POST: Ingresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ingresos == null)
            {
                return Problem("Entity set 'finanzasContext.Ingresos'  is null.");
            }
            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso != null)
            {
                _context.Ingresos.Remove(ingreso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresoExists(int id)
        {
          return (_context.Ingresos?.Any(e => e.Id_Ingresos == id)).GetValueOrDefault();
        }
    }
}
