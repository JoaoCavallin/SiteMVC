#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCRUDMVCSQL.Models;

namespace WebCRUDMVCSQL.Controllers
{
    public class PedidosController : Controller
    {
        private readonly Contexto _context;

        public PedidosController(Contexto context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pedido.Include(p => p.Cliente).ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.Produtos = _context.Produto.ToList();
            ViewBag.Clientes = _context.Cliente.ToList(); // ← adicione
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int clienteId, DateTime data, List<int> produtoIds, List<int> quantidades)
        {
            if (produtoIds == null || produtoIds.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Adicione ao menos um produto.");
                ViewBag.Produtos = _context.Produto.ToList();
                ViewBag.Clientes = _context.Cliente.ToList(); // ← adicione
                return View();
            }

            var pedido = new Pedido
            {
                ClienteId = clienteId, // ← adicione
                Data = data,
                Itens = new List<PedidoItem>()
            };

            for (int i = 0; i < produtoIds.Count; i++)
            {
                pedido.Itens.Add(new PedidoItem
                {
                    ProdutoId = produtoIds[i],
                    Quantidade = quantidades[i]
                });
            }

            _context.Add(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewBag.Produtos = _context.Produto.ToList();
            ViewBag.Clientes = _context.Cliente.ToList();

            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int clienteId, DateTime data, List<int> produtoIds, List<int> quantidades)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            // Atualiza os dados do pedido
            pedido.ClienteId = clienteId;
            pedido.Data = data;

            // Remove os itens antigos e adiciona os novos
            _context.PedidoItem.RemoveRange(pedido.Itens);
            pedido.Itens = new List<PedidoItem>();

            for (int i = 0; i < produtoIds.Count; i++)
            {
                pedido.Itens.Add(new PedidoItem
                {
                    ProdutoId = produtoIds[i],
                    Quantidade = quantidades[i]
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido    
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.Id == id);
        }

        public string SexoCliente(bool sexo)
        {
            if (sexo)
            {
                return "s";
            }
            else
            {
                return "n";
            }
        }
    }
}
