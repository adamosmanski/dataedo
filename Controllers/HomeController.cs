using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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


        [HttpPost("delete/{id}")]
        public IActionResult Delete(uint id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.Id == id);

                if (user == null)
                {
                    
                    _logger.LogWarning($"User with Id={id} not found.");
                    return NotFound($"User with Id={id} not found.");
                }

                _context.Users.Remove(user);
                _context.SaveChanges();

                // Zaloguj usuniêcie u¿ytkownika
                _logger.LogInformation($"The user with Login={user.Login} has been deleted.");
                return Ok($"The user with Login={user.Login} has been deleted.");
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, $"An error occurred while deleting user with Id={id}.");

                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}
