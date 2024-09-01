using Microsoft.AspNetCore.Mvc;
using SeaBattleASP.Models;
using System.Reflection;

namespace SeaBattleASP.MControllers
{
    public class SeaBattleController : Controller
    {
        private SeaBattle _game;

        public SeaBattleController()
        {
            _game = new SeaBattle();
        }

        public IActionResult Index()
        {
            return View(_game);
        }

        [HttpPost]
        public IActionResult PlaceShip(string cell)
        {
            if (string.IsNullOrEmpty(cell))
            {
                // Обработка случая, когда cell равно null или пусто
                ModelState.AddModelError("", "Координаты не переданы.");
                return View("Index", _game);
            }

            var coords = cell.Split('-');
            int x = int.Parse(coords[0]);
            int y = int.Parse(coords[1]);

            _game.ConfigureShips(x, y);

            return View("Index", _game);
        }

        [HttpPost]
        public IActionResult PlayerShoot(int x, int y)
        {
            bool hit = _game.PlayerShoot(x, y);
            if (!_game.CheckIfMapIsNotEmpty())
            {
                _game.Init();
            }
            return RedirectToAction("Index");
        }

        public IActionResult StartGame()
        {
            _game.isPlaying = true;
            return RedirectToAction("Index");
        }

    }
}
