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
    public class TipoIngresoesController : Controller
    {
        private readonly finanzasContext _context;

        public TipoIngresoesController(finanzasContext context)
        {
            _context = context;
        }

        // GET: TipoIngresoes
        public async Task<IActionResult> Index()
        {
            var finanzasContext = _context.TipoIngresos.Include(t => t.IdIngresoNavigation).Include(t => t.TipoNavigation);
            return View(await finanzasContext.ToListAsync());
        }

        // GET: TipoIngresoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoIngresos == null)
            {
                return NotFound();
            }

            var tipoIngreso = await _context.TipoIngresos
                .Include(t => t.IdIngresoNavigation)
                .Include(t => t.TipoNavigation)
                .FirstOrDefaultAsync(m => m.IdIngreso == id);
            if (tipoIngreso == null)
            {
                return NotFound();
            }

            return View(tipoIngreso);
        }

        // GET: TipoIngresoes/Create
        public IActionResult Create()
        {
            ViewData["IdIngreso"] = new SelectList(_context.Ingresos, "IdIngresos", "IdIngresos");
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto");
            return View();
        }

        // POST: TipoIngresoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIngreso,Tipo")] TipoIngreso tipoIngreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoIngreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIngreso"] = new SelectList(_context.Ingresos, "IdIngresos", "IdIngresos", tipoIngreso.IdIngreso);
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto", tipoIngreso.Tipo);
            return View(tipoIngreso);
        }

        // GET: TipoIngresoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoIngresos == null)
            {
                return NotFound();
            }

            var tipoIngreso = await _context.TipoIngresos.FindAsync(id);
            if (tipoIngreso == null)
            {
                return NotFound();
            }
            ViewData["IdIngreso"] = new SelectList(_context.Ingresos, "IdIngresos", "IdIngresos", tipoIngreso.IdIngreso);
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto", tipoIngreso.Tipo);
            return View(tipoIngreso);
        }

        // POST: TipoIngresoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIngreso,Tipo")] TipoIngreso tipoIngreso)
        {
            if (id != tipoIngreso.IdIngreso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoIngreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoIngresoExists(tipoIngreso.IdIngreso))
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
            ViewData["IdIngreso"] = new SelectList(_context.Ingresos, "IdIngresos", "IdIngresos", tipoIngreso.IdIngreso);
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto", tipoIngreso.Tipo);
            return View(tipoIngreso);
        }

        // GET: TipoIngresoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoIngresos == null)
            {
                return NotFound();
            }

            var tipoIngreso = await _context.TipoIngresos
                .Include(t => t.IdIngresoNavigation)
                .Include(t => t.TipoNavigation)
                .FirstOrDefaultAsync(m => m.IdIngreso == id);
            if (tipoIngreso == null)
            {
                return NotFound();
            }

            return View(tipoIngreso);
        }

        // POST: TipoIngresoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoIngresos == null)
            {
                return Problem("Entity set 'finanzasContext.TipoIngresos'  is null.");
            }
            var tipoIngreso = await _context.TipoIngresos.FindAsync(id);
            if (tipoIngreso != null)
            {
                _context.TipoIngresos.Remove(tipoIngreso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoIngresoExists(int id)
        {
          return (_context.TipoIngresos?.Any(e => e.IdIngreso == id)).GetValueOrDefault();
        }
    }
}
