using GestaoPessoas.Data;
using GestaoPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoPessoas.Controllers
{
    public class MetasController : Controller
    {
        private readonly GestaoPessoasContext _context;

        public MetasController(GestaoPessoasContext context)
        {
            _context = context;
        }

        // GET: Metas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var metas = await _context.Metas
                .Include(m => m.Setor)
                .ToListAsync();
            return View(metas);
        }

        // GET: Metas/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var meta = await _context.Metas
                .Include(m => m.Setor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meta == null)
                return NotFound();

            return View(meta);
        }

        // GET: Metas/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome");
            return View();
        }

        // POST: Metas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meta meta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", meta.SetorId);
            return View(meta);
        }

        // GET: Metas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
                return NotFound();

            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", meta.SetorId);
            return View(meta);
        }

        // POST: Metas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Meta meta)
        {
            if (id != meta.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Metas.Any(e => e.Id == meta.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", meta.SetorId);
            return View(meta);
        }

        // GET: Metas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meta = await _context.Metas
                .Include(m => m.Setor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
        }

        // POST: Metas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
            {
                return NotFound();
            }

            try
            {
                _context.Metas.Remove(meta);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Meta excluída com sucesso!";
            }
            catch (DbUpdateException)
            {
                TempData["Erro"] = "Não é possível excluir esta meta pois ela está associada a registros de desempenho ou outros dados.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
