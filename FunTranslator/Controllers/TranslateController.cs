using FunTranslator.Business;
using FunTranslator.Data;
using FunTranslator.Data.Models;
using FunTranslator.Data.Procedures;
using FunTranslator.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace FunTranslator.Controllers
{
    public class TranslateController : Controller
    {
        private readonly ILogger<TranslateController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TranslateController(ILogger<TranslateController> logger, ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _context = applicationDbContext;
            _configuration = configuration;
        }

        public IActionResult Translate()
        {
            ViewBag.data = new TranslateListModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TranslateText(TranslateViewModel textToTranslate)
        {
            var returnMessage = "";

            if (ModelState.IsValid)
            {
                var funTranslatorApi = new FunTranslatorApi();

                var apiUrl = _configuration.GetValue<string>("ApiUrl");
                var translationType = _configuration.GetValue<string>("TranslationType");

                var result = funTranslatorApi.Translate(textToTranslate.text!, apiUrl, translationType);

                if (result == null)
                {
                    returnMessage = "Technical Error";
                }
                else
                {
                    var translateLogDto = new TranslateLogDto();
                    translateLogDto.TranslationType = translationType;
                    translateLogDto.RequestData = JsonConvert.SerializeObject(textToTranslate);

                    var response = result.Content.ReadAsStringAsync().Result;

                    translateLogDto.ReturnCode = result.StatusCode.ToString();                   

                    if (result.IsSuccessStatusCode)
                    {
                        var translateResponse = JsonConvert.DeserializeObject<TranslateSuccessResponse>(response);

                        translateLogDto.ReturnMessage = "OK";
                        translateLogDto.ResponseData = JsonConvert.SerializeObject(translateResponse);

                        returnMessage = "Translated: " + translateResponse!.contents.translated;
                    }
                    else
                    {
                        var translateResponse = JsonConvert.DeserializeObject<TranslateErrorResponse>(response);

                        translateLogDto.ReturnMessage = translateResponse!.error.message;
                        translateLogDto.ResponseData = null;

                        returnMessage = "Error : " + translateResponse!.error.message;
                    }

                    var dataService = new DataService(_context);
                    dataService.LogTranslate(translateLogDto);
                }
            }

            TranslateListModel translateListModel = new TranslateListModel();
            translateListModel.message = returnMessage;

            ViewBag.data = translateListModel;

            return View("Translate");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}