using GestaoPessoas.Data;
using GestaoPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoPessoas.Controllers
{
    public class SetoresController : Controller
    {
        private readonly GestaoPessoasContext _context;

        public SetoresController(GestaoPessoasContext context)
        {
            _context = context;
        }

        // GET: /Setores
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var setores = await _context.Setores
                .Include(s => s.Funcionarios)
                .ToListAsync();
            return View(setores);
        }

        // GET: /Setores/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var setor = await _context.Setores
                .Include(s => s.Funcionarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (setor == null) return NotFound();

            return View(setor);
        }

        // GET: /Setores/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Setores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setor setor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setor);
        }

        // GET: /Setores/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var setor = await _context.Setores.FindAsync(id);
            if (setor == null) return NotFound();

            return View(setor);
        }

        // POST: /Setores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Setor setor)
        {
            if (id != setor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var setorExistente = await _context.Setores
                    .Include(s => s.Funcionarios)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (setorExistente.Funcionarios.Count > setor.CapacidadeMaximaPessoas)
                {
                    ModelState.AddModelError("", "Não é possível reduzir a capacidade abaixo do número de funcionários existentes.");
                    return View(setor);
                }

                try
                {
                    setorExistente.Nome = setor.Nome;
                    setorExistente.CapacidadeMaximaPessoas = setor.CapacidadeMaximaPessoas;

                    _context.Update(setorExistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Setores.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(setor);
        }

        // GET: /Setores/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var setor = await _context.Setores
                .Include(s => s.Funcionarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (setor == null) return NotFound();

            return View(setor);
        }

        // POST: /Setores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setor = await _context.Setores
                .Include(s => s.Funcionarios)
                .Include(s => s.Metas)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (setor == null)
                return NotFound();

            // Verifica se há funcionários vinculados
            if (setor.Funcionarios.Any())
            {
                TempData["ErrorMessage"] = "Não é possível excluir este setor, pois ainda há funcionários vinculados a ele.";
                return RedirectToAction(nameof(Index));
            }

            // Verifica se há metas vinculadas
            if (setor.Metas.Any())
            {
                TempData["ErrorMessage"] = "Não é possível excluir este setor, pois ainda há metas vinculadas a ele.";
                return RedirectToAction(nameof(Index));
            }

            _context.Setores.Remove(setor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Setor excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }


    }
}