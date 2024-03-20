using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web66bit.Models;

namespace Web66bit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlayersContext _context;

        public HomeController(ILogger<HomeController> logger, PlayersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var players = _context.players.ToList();
            return View(players);
        }

        // Действие для отображения формы редактирования игрока
        public IActionResult Edit(int id)
        {
            var player = _context.players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(player).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.players.Any(p => p.Id == id))
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
            return View(player);
        }
        
        // Действие для добавления игрока
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                _context.players.Add(player);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
