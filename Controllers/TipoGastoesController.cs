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
    public class TipoGastoesController : Controller
    {
        private readonly finanzasContext _context;

        public TipoGastoesController(finanzasContext context)
        {
            _context = context;
        }

        // GET: TipoGastoes
        public async Task<IActionResult> Index()
        {
            var finanzasContext = _context.TipoGastos.Include(t => t.TipoNavigation);
            return View(await finanzasContext.ToListAsync());
        }

        // GET: TipoGastoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoGastos == null)
            {
                return NotFound();
            }

            var tipoGasto = await _context.TipoGastos
                .Include(t => t.TipoNavigation)
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoGasto == null)
            {
                return NotFound();
            }

            return View(tipoGasto);
        }

        // GET: TipoGastoes/Create
        public IActionResult Create()
        {
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto");
            return View();
        }

        // POST: TipoGastoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,Tipo")] TipoGasto tipoGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto", tipoGasto.Tipo);
            return View(tipoGasto);
        }

        // GET: TipoGastoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoGastos == null)
            {
                return NotFound();
            }

            var tipoGasto = await _context.TipoGastos.FindAsync(id);
            if (tipoGasto == null)
            {
                return NotFound();
            }
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto", tipoGasto.Tipo);
            return View(tipoGasto);
        }

        // POST: TipoGastoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,Tipo")] TipoGasto tipoGasto)
        {
            if (id != tipoGasto.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoGastoExists(tipoGasto.IdTipo))
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
            ViewData["Tipo"] = new SelectList(_context.Textos, "IdTexto", "IdTexto", tipoGasto.Tipo);
            return View(tipoGasto);
        }

        // GET: TipoGastoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoGastos == null)
            {
                return NotFound();
            }

            var tipoGasto = await _context.TipoGastos
                .Include(t => t.TipoNavigation)
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoGasto == null)
            {
                return NotFound();
            }

            return View(tipoGasto);
        }

        // POST: TipoGastoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoGastos == null)
            {
                return Problem("Entity set 'finanzasContext.TipoGastos'  is null.");
            }
            var tipoGasto = await _context.TipoGastos.FindAsync(id);
            if (tipoGasto != null)
            {
                _context.TipoGastos.Remove(tipoGasto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoGastoExists(int id)
        {
          return (_context.TipoGastos?.Any(e => e.IdTipo == id)).GetValueOrDefault();
        }
    }
}
