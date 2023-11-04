using ASP_Final.Dao.IRepository;
using ASP_Final.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace ASP_Final.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller 
    // GET: Admin/Product
    
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork,ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index(int? page)
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
    }
}