#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCRUDMVCSQL.Models;
using WebCRUDMVCSQL.ViewModels;

namespace WebCRUDMVCSQL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Contexto _context;

        public UsuarioController(Contexto context)
        {
            _context = context;
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Senha")] UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Senha = model.Senha
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        // GET: Usuarios/Create
        public IActionResult Login()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Senha")] Usuario usuario)
        {
            ModelState.Remove("Nome");

            if (ModelState.IsValid)
            {
                var usuarioEncontrado = await _context.Usuario
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email
                                           && u.Senha == usuario.Senha);

                if (usuarioEncontrado == null)
                {
                    ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
                    return View(usuario);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var viewModel = new UsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha
            };

            return View(viewModel);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null) return NotFound();

                usuario.Nome = viewModel.Nome;
                usuario.Email = viewModel.Email;
                usuario.Senha = viewModel.Senha;

                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

    }
}
