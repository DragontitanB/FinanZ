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
    public class ConfiguracionsController : Controller
    {
        private readonly finanzasContext _context;

        public ConfiguracionsController(finanzasContext context)
        {
            _context = context;
        }

        // GET: Configuracions
        public async Task<IActionResult> Index()
        {
            var finanzasContext = _context.Configuracions.Include(c => c.IdConfiguracionNavigation);
            return View(await finanzasContext.ToListAsync());
        }

        // GET: Configuracions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Configuracions == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracions
                .Include(c => c.IdConfiguracionNavigation)
                .FirstOrDefaultAsync(m => m.IdConfiguracion == id);
            if (configuracion == null)
            {
                return NotFound();
            }

            return View(configuracion);
        }

        // GET: Configuracions/Create
        public IActionResult Create()
        {
            ViewData["IdConfiguracion"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Configuracions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConfiguracion,Tema,Notificaciones")] Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConfiguracion"] = new SelectList(_context.Usuarios, "Id", "Id", configuracion.IdConfiguracion);
            return View(configuracion);
        }

        // GET: Configuracions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Configuracions == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracions.FindAsync(id);
            if (configuracion == null)
            {
                return NotFound();
            }
            ViewData["IdConfiguracion"] = new SelectList(_context.Usuarios, "Id", "Id", configuracion.IdConfiguracion);
            return View(configuracion);
        }

        // POST: Configuracions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConfiguracion,Tema,Notificaciones")] Configuracion configuracion)
        {
            if (id != configuracion.IdConfiguracion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracionExists(configuracion.IdConfiguracion))
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
            ViewData["IdConfiguracion"] = new SelectList(_context.Usuarios, "Id", "Id", configuracion.IdConfiguracion);
            return View(configuracion);
        }

        // GET: Configuracions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Configuracions == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracions
                .Include(c => c.IdConfiguracionNavigation)
                .FirstOrDefaultAsync(m => m.IdConfiguracion == id);
            if (configuracion == null)
            {
                return NotFound();
            }

            return View(configuracion);
        }

        // POST: Configuracions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Configuracions == null)
            {
                return Problem("Entity set 'finanzasContext.Configuracions'  is null.");
            }
            var configuracion = await _context.Configuracions.FindAsync(id);
            if (configuracion != null)
            {
                _context.Configuracions.Remove(configuracion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracionExists(int id)
        {
          return (_context.Configuracions?.Any(e => e.IdConfiguracion == id)).GetValueOrDefault();
        }
    }
}
