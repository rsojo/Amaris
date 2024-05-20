using Amaris.Models.DTO;
using Amaris.Models.General;
using Amaris.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Amaris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IEmployeeService _service;

        public HomeController(IEmployeeService service, ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(IFormCollection collection)
        {
            var Lst = new List<Employee>();
            var response = new Response<Employee>();
            if (collection["Search"][0] != "")
            {
                response = await _service.GetItem(int.Parse(collection["Search"][0]));
            }
            else
            {
                response = await _service.GetItems("");
            }
            if (response.Error)
            {
                _logger.LogError(response.Msg); // Log the error message
                return RedirectToAction("Error", "Home");
            }

            return View(response.Lst);
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
