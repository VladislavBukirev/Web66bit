using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web66bit.Models;

namespace Web66bit.Controllers
{
    public class HomeController : Controller
    {
        private readonly PlayersContext _context;
        public HomeController(PlayersContext context)
        {
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
                return NotFound();

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
                    var existingPlayer = _context.players.Find(id);
                    if (existingPlayer == null)
                    {
                        return NotFound();
                    }
                    
                    existingPlayer.Name = player.Name;
                    existingPlayer.Surname = player.Surname;
                    existingPlayer.Gender = player.Gender;
                    existingPlayer.BirthDate = player.BirthDate;
                    existingPlayer.TeamName = player.TeamName;
                    existingPlayer.Country = player.Country; 

                    _context.Entry(existingPlayer).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.players.Any(p => p.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // Действие для добавления игрока
        public IActionResult Create()
        {
            var teams = _context.players.Select(t => t.TeamName).Distinct().ToList();
            ViewBag.Teams = teams;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
