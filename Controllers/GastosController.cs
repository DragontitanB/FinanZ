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
    public class GastosController : Controller
    {
        private readonly finanzasContext _context;

        public GastosController(finanzasContext context)
        {
            _context = context;
        }

        // GET: Gastos
        public async Task<IActionResult> Index()
        {
            var finanzasContext = _context.Gastos.Include(g => g.IdTipoNavigation).Include(g => g.IdUsuarioNavigation);
            return View(await finanzasContext.ToListAsync());
        }

        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gastos == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .Include(g => g.IdTipoNavigation)
                .Include(g => g.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdGastos == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            ViewData["IdTipo"] = new SelectList(_context.TipoGastos, "IdTipo", "IdTipo");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGastos,IdUsuario,Monto,IdTipo,Fecha,Recurrencia,Notificacion,Automatico")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipo"] = new SelectList(_context.TipoGastos, "IdTipo", "IdTipo", gasto.IdTipo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", gasto.IdUsuario);
            return View(gasto);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gastos == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }
            ViewData["IdTipo"] = new SelectList(_context.TipoGastos, "IdTipo", "IdTipo", gasto.IdTipo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", gasto.IdUsuario);
            return View(gasto);
        }

        // POST: Gastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGastos,IdUsuario,Monto,IdTipo,Fecha,Recurrencia,Notificacion,Automatico")] Gasto gasto)
        {
            if (id != gasto.IdGastos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.IdGastos))
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
            ViewData["IdTipo"] = new SelectList(_context.TipoGastos, "IdTipo", "IdTipo", gasto.IdTipo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", gasto.IdUsuario);
            return View(gasto);
        }

        // GET: Gastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gastos == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .Include(g => g.IdTipoNavigation)
                .Include(g => g.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdGastos == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // POST: Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gastos == null)
            {
                return Problem("Entity set 'finanzasContext.Gastos'  is null.");
            }
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoExists(int id)
        {
          return (_context.Gastos?.Any(e => e.IdGastos == id)).GetValueOrDefault();
        }
    }
}
