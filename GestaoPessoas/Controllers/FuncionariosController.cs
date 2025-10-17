using GestaoPessoas.Data;
using GestaoPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestaoPessoas.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly GestaoPessoasContext _context;

        public FuncionariosController(GestaoPessoasContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Setor)
                .ToListAsync();
            return View(funcionarios);
        }

        
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Setor)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                var setor = await _context.Setores
                    .Include(s => s.Funcionarios)
                    .FirstOrDefaultAsync(s => s.Id == funcionario.SetorId);

                if (setor == null)
                {
                    ModelState.AddModelError("", "Setor inválido.");
                    ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", funcionario.SetorId);
                    return View(funcionario);
                }

                if (setor.Funcionarios.Count >= setor.CapacidadeMaximaPessoas)
                {
                    ModelState.AddModelError("SetorId", "Este setor atingiu sua capacidade máxima.");
                    ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", funcionario.SetorId);
                    return View(funcionario);
                }

                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", funcionario.SetorId);
            return View(funcionario);
        }


        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", funcionario.SetorId);
            return View(funcionario);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var setorDestino = await _context.Setores
                    .Include(s => s.Funcionarios)
                    .FirstOrDefaultAsync(s => s.Id == funcionario.SetorId);

                if (setorDestino == null)
                {
                    ModelState.AddModelError("", "Setor não encontrado.");
                    return View(funcionario);
                }

                // validação: capacidade máxima
                if (setorDestino.Funcionarios.Count >= setorDestino.CapacidadeMaximaPessoas)
                {
                    ModelState.AddModelError("SetorId", "Este setor já atingiu sua capacidade máxima.");
                    ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", funcionario.SetorId);
                    return View(funcionario);
                }

                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Funcionarios.Any(e => e.Id == funcionario.Id))
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

            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "Nome", funcionario.SetorId);
            return View(funcionario);
        }


        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var funcionario = await _context.Funcionarios
                .Include(f => f.Setor)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var funcionario = await _context.Funcionarios.FindAsync(id);
                if (funcionario == null)
                {
                    TempData["Erro"] = "Funcionário não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Funcionário excluído com sucesso!";
            }
            catch (DbUpdateException)
            {
                TempData["Erro"] = "Não é possível excluir este funcionário, pois há desempenhos vinculados.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
