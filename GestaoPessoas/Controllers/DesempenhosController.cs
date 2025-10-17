using GestaoPessoas.Data;
using GestaoPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestaoPessoas.Controllers
{
    public class DesempenhosController : Controller
    {
        private readonly GestaoPessoasContext _context;

        public DesempenhosController(GestaoPessoasContext context)
        {
            _context = context;
        }

        // GET: Desempenhos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var desempenhos = await _context.Desempenhos
                .Include(d => d.Funcionario)
                .Include(d => d.Meta)
                .ToListAsync();

            return View(desempenhos);
        }

        // GET: Desempenhos/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var desempenho = await _context.Desempenhos
                .Include(d => d.Funcionario)
                .Include(d => d.Meta)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (desempenho == null)
                return NotFound();

            return View(desempenho);
        }

        // GET: Desempenhos/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome");
            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Descricao");
            return View();
        }

        // POST: Desempenhos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Desempenho desempenho)
        {
            if (ModelState.IsValid)
            {
                // Validação opcional: pontuação não pode exceder a pontuação máxima da meta
                var meta = await _context.Metas.FindAsync(desempenho.MetaId);
                if (meta != null && desempenho.PontuacaoObtida > meta.PontuacaoMaxima)
                {
                    ModelState.AddModelError("PontuacaoObtida", "A pontuação não pode ser maior que a pontuação máxima da meta.");
                    ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", desempenho.FuncionarioId);
                    ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Descricao", desempenho.MetaId);
                    return View(desempenho);
                }

                _context.Add(desempenho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", desempenho.FuncionarioId);
            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Descricao", desempenho.MetaId);
            return View(desempenho);
        }

        // GET: Desempenhos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var desempenho = await _context.Desempenhos.FindAsync(id);
            if (desempenho == null)
                return NotFound();

            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", desempenho.FuncionarioId);
            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Descricao", desempenho.MetaId);
            return View(desempenho);
        }

        // POST: Desempenhos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Desempenho desempenho)
        {
            if (id != desempenho.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var meta = await _context.Metas.FindAsync(desempenho.MetaId);
                if (meta != null && desempenho.PontuacaoObtida > meta.PontuacaoMaxima)
                {
                    ModelState.AddModelError("PontuacaoObtida", "A pontuação não pode ser maior que a pontuação máxima da meta.");
                    ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", desempenho.FuncionarioId);
                    ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Descricao", desempenho.MetaId);
                    return View(desempenho);
                }

                try
                {
                    _context.Update(desempenho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Desempenhos.Any(e => e.Id == desempenho.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", desempenho.FuncionarioId);
            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Descricao", desempenho.MetaId);
            return View(desempenho);
        }

        // GET: /Desempenhos/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var desempenho = await _context.Desempenhos
                .Include(d => d.Funcionario)
                .Include(d => d.Meta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (desempenho == null) return NotFound();

            return View(desempenho);
        }

        // POST: /Desempenhos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desempenho = await _context.Desempenhos.FindAsync(id);
            if (desempenho == null) return NotFound();

            _context.Desempenhos.Remove(desempenho);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
