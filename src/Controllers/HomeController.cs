using AgendaContatosMVC.Context;
using AgendaContatosMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatosMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AgendaContext _context;

        public HomeController(AgendaContext context)
        {
            _context = context;
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        // LISTA + FORM
        public IActionResult Index(int? id)
        {
            var contatos = _context.Contatos.AsNoTracking().ToList();

            Contato contato = id.HasValue
                ? _context.Contatos.AsNoTracking().FirstOrDefault(c => c.Id == id)
                : new Contato();

            ViewBag.Contatos = contatos;
            return View(contato);
        }


        // Buscar por Id
        [HttpGet]
        public IActionResult PesquisarPorId(int id)
        {
            var contato = _context.Contatos.FirstOrDefault(c => c.Id == id);

            if (contato == null)
            {
                ViewBag.Contatos = new List<Contato>(); // tabela fica vazia
                TempData["Mensagem"] = "Nenhum contato encontrado com esse Id.";
            }
            else
            {
                ViewBag.Contatos = new List<Contato> { contato };
            }

            return View("Index", new Contato());
        }

        // Buscar por Nome
        [HttpGet]
        public IActionResult PesquisarPorNome(string nome)
        {
            var contatos = _context.Contatos
                .Where(c => c.Nome.Contains(nome))
                .ToList();

            if (!contatos.Any())
            {
                TempData["Mensagem"] = "Nenhum contato encontrado com esse nome.";
            }

            ViewBag.Contatos = contatos;

            return View("Index", new Contato());
        }

        // SALVAR (Criação ou Edição)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Contato contato)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Contatos = _context.Contatos.AsNoTracking().ToList();
                return View("Index", contato);
            }

            if (contato.Id == 0)
                _context.Contatos.Add(contato);
            else
                _context.Contatos.Update(contato);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // EXCLUIR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato != null)
            {
                _context.Contatos.Remove(contato);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
