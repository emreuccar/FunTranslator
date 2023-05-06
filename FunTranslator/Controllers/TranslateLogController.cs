using FunTranslator.Business;
using FunTranslator.Data;
using FunTranslator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FunTranslator.Controllers
{
    public class TranslateLogController : Controller
    {
        private readonly ILogger<TranslateLogController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TranslateLogController(ILogger<TranslateLogController> logger, ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _context = applicationDbContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.data = new TranslateListModel();

            TranslateSearchModel translateSearchModel = new TranslateSearchModel();
            translateSearchModel.Text = "";
            translateSearchModel.StartDate = DateTime.Today.AddMonths(-3);
            translateSearchModel.EndDate = DateTime.Today;


            return View("TranslateLog", translateSearchModel);
        }

        public IActionResult Search(TranslateSearchModel translateSearchModel)
        {
            var dataService = new DataService(_context);
            var list = dataService.SelectLogs(translateSearchModel);

            TranslateListModel translateListModel = new TranslateListModel();
            translateListModel.Logs = list;

            ViewBag.data = translateListModel;

            return View("TranslateLog");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}